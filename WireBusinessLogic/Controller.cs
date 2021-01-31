// ***********************************************************************
// Assembly         : WireBusinessLogic
// Author           : Jia Li
// Created          : 11-27-2020
//
// Last Modified By : Jia Li
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="Controller.cs" company="Red Clay">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using WireAPI;
using WireCommon;
using WireConfig;

namespace WireBusinessLogic
{
    /// <summary>
    ///     Class Controller.
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    /// </summary>
    public class Controller : IWireCommunicator
    {
        /// <summary>
        ///     The timer
        /// </summary>
        private static Timer _timer;

        /// <summary>
        ///     The configuration
        /// </summary>
        private readonly Configuration _configuration;

        /// <summary>
        ///     The email API
        /// </summary>
        private readonly IEmailApi _emailAPI;

        /// <summary>
        ///     The VSO API
        /// </summary>
        private readonly IVSOApi _vsoApi;

        /// <summary>
        ///     Ticks per minute = 1000 milliseconds in one second, times 60 seconds
        ///     in one minute.
        /// </summary>
        private readonly int TICKS_PER_MINUTE = 1000 * 60;

        /// <summary>
        ///     The handle error
        /// </summary>
        private Action<Exception> _handleError;

        /// <summary>
        ///     The handle message
        /// </summary>
        private Action<string> _handleMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Controller" /> class.
        /// </summary>
        /// <param name="vsoApi">The vso API.</param>
        /// <param name="emailApi">The email API.</param>
        /// <param name="configuration">The configuration.</param>
        public Controller(IVSOApi vsoApi, IEmailApi emailApi, Configuration configuration)
        {
            _vsoApi = vsoApi;
            _emailAPI = emailApi;
            _configuration = configuration;
            State = new WorkStatus();

            State.Status = StateEnum.Stopped;
            _timer = new Timer(TimerCallback, State, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        ///     Gets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        public WorkStatus State { get; }

        /// <summary>
        ///     Gets or sets the error handler.
        /// </summary>
        /// <value>The error handler.</value>
        public Action<Exception> HandleError
        {
            get => _handleError;
            set
            {
                _handleError = value;
                _vsoApi.HandleError += _handleError;
                _emailAPI.HandleError += _handleError;
            }
        }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The handle message.</value>
        public Action<string> HandleMessage
        {
            get => _handleMessage;
            set
            {
                _handleMessage = value;
                _vsoApi.HandleMessage += _handleMessage;
                _emailAPI.HandleMessage += _handleMessage;
            }
        }

        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>
        ///     The handle log.
        /// </value>
        public Action<LogEntryType, string> HandleLog { get; set; }

        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>
        ///     The handle report.
        /// </value>
        public Action<string> HandleReport { get; set; }

        /// <summary>
        ///     Calls the error handler with the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            if (HandleError != null)
                HandleError.Invoke(exception);
            else
                throw exception;
        }

        /// <summary>
        ///     Calls the message handler with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Message(string message)
        {
            HandleMessage?.Invoke(message);
        }

        /// <summary>
        ///     Logs the specified message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        public void Log(LogEntryType entryType, string message)
        {
            HandleLog?.Invoke(entryType, message);
        }

        /// <summary>
        ///     Reports the specified message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        public void Report(string message)
        {
            HandleReport?.Invoke(message);
        }

        /// <summary>
        ///     Tests report functionality by gathering report info, but only sending
        ///     the results to the report email address.
        /// </summary>
        public void Test()
        {
            CheckServer(false);
        }

        /// <summary>
        ///     Runs the report functionality immediately.
        /// </summary>
        public void Run()
        {
            CheckServer(true);
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            if (State.Status == StateEnum.Stopped)
                _timer.Change(1000, _configuration.ControllerConfig.PollingInterval * TICKS_PER_MINUTE);
            State.Status = StateEnum.Started;
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            State.Status = StateEnum.Stopped;
        }

        /// <summary>
        ///     Checks the server.
        /// </summary>
        public void CheckServer(bool sendReminders)
        {
            var errorDictionary =
                new Dictionary<string, Dictionary<int, TaskItemErrors>>();
            var reportEmailStrings = new StringBuilder();

            var msg = $"Checking server {_configuration.VsoConfig.BaseUri}...";
            Log(LogEntryType.INFO, msg);
            Message(msg);

            _vsoApi.Connect();
            var projectList = _configuration.VsoConfig.ConfigItems.Select(
                s => s.Value.AreaPath).Distinct().ToList();
            var assignedList = _configuration.EMailConfig.Recipients.Select(item => item.Key).ToList();
            var records =
                _vsoApi.SelectItems(DateTime.UtcNow.AddMinutes(_configuration.ControllerConfig.ReportingInterval * -1),
                    projectList, assignedList);
            errorDictionary = ParseRecords(records);

            var reportText = GenerateReportAndCacheEmails(errorDictionary);
            if (_emailAPI.CachedEmailCount > 0)
            {
                if (sendReminders)
                    _emailAPI.SendCachedEmails();
                else
                    _emailAPI.ClearCache();

                SendReportEmailAndSaveReport(reportText);
            }
            else
            {
                msg = "No errors found...";
                Log(LogEntryType.INFO, msg);
                Message(msg);
            }
        }

        /// <summary>
        ///     Parses the work item records and returns a dictionary of errors for processing into
        ///     an error report and emails.
        /// </summary>
        /// <param name="workItems">The work items to parse.</param>
        /// <returns>Dictionary of errors</returns>
        private Dictionary<string, Dictionary<int, TaskItemErrors>> ParseRecords(List<WorkItem> workItems)
        {
            var result = new Dictionary<string, Dictionary<int, TaskItemErrors>>();

            if (workItems != null)
            {
                var displayName = string.Empty;
                var workItemName = string.Empty;
                var workItemUrl = string.Empty;
                IdentityRef assignedTo;

                // foreach record in workitems
                foreach (var workItem in workItems)
                {
                    if (workItem.Id == null)
                        continue;

                    if (workItem.Fields.ContainsKey(Constants.WORK_ITEM_ASSIGNED_TO))
                    {
                        assignedTo = (IdentityRef) workItem.Fields[Constants.WORK_ITEM_ASSIGNED_TO];
                        displayName = assignedTo.DisplayName;
                        workItemName = workItem.Fields[Constants.WORK_ITEM_TITLE].ToString();
                        workItemUrl = workItem.Url.Replace("apis/wit/workItems", "workitems/edit");

                        var msg = $"Checking work item {workItemName}...";
                        Log(LogEntryType.INFO, msg);
                        Message(msg);

                        if (_configuration.EMailConfig.Recipients.ContainsKey(displayName))
                        {
                            msg = $"Work item assigned to {displayName}";
                            Log(LogEntryType.INFO, msg);
                            Message(msg);

                            foreach (var configItem in _configuration.VsoConfig.ConfigItems)
                            {
                                var taskItem = configItem.Value;
                                var searchName = taskItem.FieldPrefix + taskItem.FieldName.Trim();

                                // if it's present...
                                if (workItem.Fields.ContainsKey(searchName))
                                {
                                    // if it fails regex test
                                    if (!configItem.Value.IsValid(workItem.Fields[searchName]))
                                        // Add error as BAD VALUE
                                        AddError(result, true, displayName, workItem.Id.GetValueOrDefault(),
                                            workItemName, workItemUrl, taskItem);
                                }
                                else
                                {
                                    // Add error as NO VALUE
                                    AddError(result, false, displayName, workItem.Id.GetValueOrDefault(),
                                        workItemName, workItemUrl, taskItem);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Adds an error line to the error dictionary passed in as a parameter.
        /// </summary>
        /// <param name="errorDictionary">The error dictionary.</param>
        /// <param name="itemPresent">if set to <c>true</c> [item present].</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="workItemId">The work item identifier.</param>
        /// <param name="workItemTitle">The work item title.</param>
        /// <param name="workItemURL">The work item URL.</param>
        /// <param name="itemConfig">The item configuration.</param>
        private void AddError(Dictionary<string, Dictionary<int, TaskItemErrors>> errorDictionary, bool itemPresent,
            string userName, int workItemId, string workItemTitle, string workItemURL, TaskItemConfig itemConfig)
        {
            Dictionary<int, TaskItemErrors> taskItemDictionary;
            var logEntry = string.Format(Constants.LOG_REMINDER_FIELD_LINE, itemConfig.FieldName,
                itemConfig.Description);

            if (errorDictionary.ContainsKey(userName))
            {
                taskItemDictionary = errorDictionary[userName];
            }
            else
            {
                taskItemDictionary = new Dictionary<int, TaskItemErrors>();
                errorDictionary.Add(userName, taskItemDictionary);
            }

            TaskItemErrors taskItemErrors;
            if (taskItemDictionary.ContainsKey(workItemId))
            {
                taskItemErrors = taskItemDictionary[workItemId];
            }
            else
            {
                taskItemErrors = new TaskItemErrors(userName, workItemId, workItemTitle, workItemURL);
                taskItemDictionary.Add(workItemId, taskItemErrors);
            }

            taskItemErrors.AddErrorItem(itemConfig, itemPresent);

            // log the error to the text log
            Log(LogEntryType.ERROR, string.Format(itemPresent ? Constants.BAD_VALUE_FORMAT : Constants.NO_VALUE_FORMAT,
                logEntry));
        }

        /// <summary>
        ///     Generates the report and caches reminder emails for sending to users. Returns the
        ///     consolidated error report as a string.
        /// </summary>
        /// <param name="errorDictionary">The error dictionary.</param>
        /// <returns>The consolidated error report from the error dictionary.</returns>
        private string GenerateReportAndCacheEmails(Dictionary<string, Dictionary<int, TaskItemErrors>> errorDictionary)
        {
            var reportEmailBodyStringBuilder = new StringBuilder();

            foreach (var taskItemDictKvp in errorDictionary)
            {
                var displayName = taskItemDictKvp.Key;
                var emailAddress = _configuration.EMailConfig.Recipients[displayName];
                var userEmailBodyStringBuilder = new StringBuilder();
                foreach (var workItemKvp in taskItemDictKvp.Value)
                {
                    var taskItemErrors = workItemKvp.Value;
                    userEmailBodyStringBuilder.AppendLine(string.Format(Constants.USER_EMAIL_TASK_HEADER,
                        taskItemErrors.WorkItemURL,
                        taskItemErrors.WorkItemTitle));

                    foreach (var taskItemConfigTuple in taskItemErrors.ErrorItems)
                    {
                        var taskItemConfig = taskItemConfigTuple.Item1;
                        var errorKind = taskItemConfigTuple.Item2 ? "BAD VALUE" : "NO VALUE";
                        userEmailBodyStringBuilder.AppendLine(string.Format(Constants.USER_EMAIL_TASK_LINE_ITEM,
                            errorKind,
                            taskItemConfig.FieldName, taskItemConfig.Description, taskItemConfig.HelpMessage));
                    }
                }

                var userEmailBody = string.Format(Constants.USER_EMAIL_BODY, userEmailBodyStringBuilder);
                _emailAPI.CacheEmail(displayName, emailAddress, Constants.USER_EMAIL_SUBJECT, userEmailBody);

                reportEmailBodyStringBuilder.AppendLine(userEmailBodyStringBuilder.ToString());
            }

            return reportEmailBodyStringBuilder.ToString();
        }

        /// <summary>
        ///     Sends the report to the configured report email address, and writes it to disk.
        /// </summary>
        public void SendReportEmailAndSaveReport(string reportText)
        {
            var msg = "Sending error report...";
            Log(LogEntryType.INFO, msg);
            Message(msg);

            var reportEmailBody = string.Format(Constants.REPORT_HTML_FORMAT, reportText);
            Report(reportEmailBody);
            _emailAPI.SendEmail(_configuration.ControllerConfig.ReportEmail,
                _configuration.ControllerConfig.ReportEmail,
                string.Format(Constants.REPORT_EMAIL_SUBJECT_FORMAT, DateTime.Now), reportEmailBody);

            msg = $"Error report sent to {_configuration.ControllerConfig.ReportEmail}";
            Log(LogEntryType.INFO, msg);
            Message(msg);
        }

        /// <summary>
        ///     Gets the contents of the most recent log file as a string.
        /// </summary>
        /// <returns>Contents of most recent log file</returns>
        public string GetLogContents()
        {
            var result = new StringBuilder();
            FileInfo logFile;

            var directory = new DirectoryInfo(_configuration.ControllerConfig.LogFolder);
            var files = directory.GetFiles(FileWriter.LOG_FILE_PATTERN);
            if (files.Length > 0)
            {
                logFile = (from f in files
                    orderby f.LastWriteTime descending
                    select f).First();

                // need to make sure the linefeeds are intact
                var logStrings = File.ReadAllLines(logFile.FullName);
                foreach (var reportString in logStrings)
                    result.AppendLine(reportString);
            }

            return result.ToString();
        }

        /// <summary>
        ///     Gets the contents of the most recent report file as a string.
        /// </summary>
        /// <returns>Contents of the most recent report file</returns>
        public string GetReportContents()
        {
            var result = new StringBuilder();
            FileInfo reportFile;

            var directory = new DirectoryInfo(_configuration.ControllerConfig.ReportFolder);
            var files = directory.GetFiles(FileWriter.REPORT_FILE_PATTERN);
            if (files.Length > 0)
            {
                reportFile = (from f in files
                    orderby f.LastWriteTime descending
                    select f).First();

                // need to make sure the linefeeds are intact
                var reportStrings = File.ReadAllLines(reportFile.FullName);
                foreach (var reportString in reportStrings)
                    result.AppendLine(reportString);
            }

            return result.ToString();
        }

        /// <summary>
        ///     Callback for checking wok items on a schedule.
        /// </summary>
        /// <param name="state">The state.</param>
        private void TimerCallback(object state)
        {
            var currentState = (WorkStatus) state;

            switch (currentState.Status)
            {
                case StateEnum.Started:
                    CheckServer(true);
                    break;
                case StateEnum.Stopped:
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    break;
                case StateEnum.Paused:
                    break;
            }
        }

        /// <summary>
        ///     Pauses this instance.
        /// </summary>
        public void Pause()
        {
            State.Status = StateEnum.Paused;
        }

        /// <summary>
        ///     Sends the most recent log.
        /// </summary>
        public void SendMostRecentLog()
        {
            Message("Checking most recent log file...");

            var logContents = GetLogContents();
            if (string.IsNullOrEmpty(logContents))
            {
                Message("No log file found...");
                return;
            }

            Message("Sending log...");
            _emailAPI.SendEmail(_configuration.ControllerConfig.ReportEmail,
                _configuration.ControllerConfig.ReportEmail,
                string.Format(Constants.LOG_EMAIL_SUBJECT_FORMAT, DateTime.Now), logContents, false);

            Message($"Log sent to {_configuration.ControllerConfig.ReportEmail}");
        }

        /// <summary>
        ///     Sends the most recent report.
        /// </summary>
        public void SendMostRecentReport()
        {
            Message("Checking most recent report file...");

            var reportContents = GetReportContents();
            if (string.IsNullOrEmpty(reportContents))
            {
                Message("No report file found...");
                return;
            }

            Message("Sending report...");

            _emailAPI.SendEmail(_configuration.ControllerConfig.ReportEmail,
                _configuration.ControllerConfig.ReportEmail,
                string.Format(Constants.REPORT_EMAIL_SUBJECT_FORMAT, DateTime.Now), reportContents);

            Message($"Report sent to {_configuration.ControllerConfig.ReportEmail}");
        }
    }
}
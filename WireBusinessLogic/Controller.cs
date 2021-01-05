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
using System.Runtime.Serialization;
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
        public WorkStatus State { get; private set; }

        /// <summary>
        /// Ticks per minute = 1000 milliseconds in one second, times 60 seconds
        /// in one minute.
        /// </summary>
        private readonly int TICKS_PER_MINUTE = 1000 * 60;

        /// <summary>
        ///     The timer
        /// </summary>
        private static Timer _timer;

        /// <summary>
        ///     The email API
        /// </summary>
        private readonly IEmailApi _emailAPI;

        /// <summary>
        ///     The VSO API
        /// </summary>
        private readonly IVSOApi _vsoApi;

        /// <summary>
        ///     The configuration
        /// </summary>
        private readonly Configuration _configuration;

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
        /// Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>
        /// The handle log.
        /// </value>
        public Action<LogEntryType, string> HandleLog { get; set; }

        /// <summary>
        /// Gets or sets the report operations handler.
        /// </summary>
        /// <value>
        /// The handle report.
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
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Log(LogEntryType entryType, string message)
        {
            HandleLog?.Invoke(entryType, message);
        }

        /// <summary>
        /// Reports the specified message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Report(string message)
        {
            HandleReport?.Invoke(message);
        }

        public void run()
        {
            CheckServer();
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
        public void CheckServer()
        {
            _vsoApi.Connect();
            var projectList = _configuration.VsoConfig.ConfigItems.Select(
                s => s.Value.AreaPath).Distinct().ToList();
            var assignedList = _configuration.EMailConfig.Recipients.Select(item => item.Key).ToList();
            var records = 
                _vsoApi.SelectItems(DateTime.UtcNow.AddMinutes(_configuration.ControllerConfig.ReportingInterval * -1),
                projectList, assignedList);
            ParseRecords(records);
        }

        /// <summary>
        ///     Parses the records.
        /// </summary>
        /// <param name="workItems">The work items.</param>
        private void ParseRecords(List<WorkItem> workItems)
        {
            if (workItems == null) return;

            var reportLines = new StringBuilder();
            try
            {
                foreach (var record in workItems)
                {
                    if (record.Fields.ContainsKey(Constants.WORK_ITEM_ASSIGNED_TO))
                    {
                        var assignedTo = (IdentityRef) record.Fields[Constants.WORK_ITEM_ASSIGNED_TO];
                        var displayName = assignedTo.DisplayName;
                        var workItemUrl = record.Url.Replace("apis/wit/workItems", "workitems/edit");

                        if (_configuration.EMailConfig.Recipients.ContainsKey(displayName))
                        {
                            var logLines = new StringBuilder();
                            var errorList = new StringBuilder();

                            var emailAddress = _configuration.EMailConfig.Recipients[displayName];
                            var emailSubject = string.Format(Constants.EMAIL_SUBJECT_FORMAT,
                                record.Id, record.Fields[Constants.WORK_ITEM_TITLE]);
                            var logReminderText =
                                string.Format(Constants.LOG_REMINDER_HEADER, displayName, emailSubject);
                            logLines.AppendLine(logReminderText);
                            reportLines.AppendLine(logReminderText);

                            foreach (var item in _configuration.VsoConfig.ConfigItems)
                            {
                                var taskItem = item.Value;
                                var searchName = (taskItem.FieldType == 0 ? "System." : "Custom.") +
                                                 taskItem.FieldName.Trim();

                                var logEntry = string.Format(Constants.LOG_REMINDER_FIELD_LINE, searchName, taskItem.Description);

                                if (record.Fields.ContainsKey(searchName))
                                {
                                    if (!item.Value.IsValid(record.Fields[searchName]))
                                    {
                                        var errorLine = string.Format(
                                            Constants.EMAIL_TABLE_ROW_FORMAT, "BAD VALUE", taskItem.FieldName,
                                            taskItem.Description, taskItem.HelpMessage
                                        );

                                        errorList.AppendLine(errorLine);
                                        logLines.AppendLine(string.Format(Constants.BAD_VALUE_FORMAT, logEntry));
                                        reportLines.AppendLine(string.Format(Constants.BAD_VALUE_FORMAT, logEntry));
                                    }
                                }
                                else
                                {
                                    var errorLine = string.Format(
                                        Constants.EMAIL_TABLE_ROW_FORMAT, "NO VALUE", taskItem.FieldName,
                                        taskItem.Description, taskItem.HelpMessage
                                    );

                                    errorList.AppendLine(errorLine);
                                    logLines.AppendLine(string.Format(Constants.NO_VALUE_FORMAT, logEntry));
                                    reportLines.AppendLine(string.Format(Constants.NO_VALUE_FORMAT, logEntry));
                                }
                            }

                            if (logLines.Length > 0)
                                Log(LogEntryType.ERROR, logLines.ToString());

                            if (errorList.Length > 0)
                            {
                                var emailBody = string.Format(Constants.EMAIL_BODY_FORMAT,
                                    workItemUrl, record.Id, record.Fields[Constants.WORK_ITEM_TITLE], errorList
                                );

                                var sendMsg = $"Sending reminder to {displayName}...";
                                Message(sendMsg);
                                Log(LogEntryType.INFO, sendMsg);
                                //_emailAPI.SendEmail(displayName, emailAddress, emailSubject, emailBody);
                            }
                        }
                    }
                }

                if (reportLines.Length > 0)
                {
                    Report(reportLines.ToString());
                    SendReport();
                }
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        /// <summary>
        ///     Sends the report.
        /// </summary>
        public void SendReport()
        {
            Message($"Sending log...");

            var reportContent = GetReportContents();
            _emailAPI.SendEmail(_configuration.ControllerConfig.ReportEmail, _configuration.ControllerConfig.ReportEmail,
                $"WIRE report {DateTime.Now}", reportContent, false);

            Message($"Log sent to {_configuration.ControllerConfig.ReportEmail}.");
        }

        public string GetReportContents()
        {
            var directory = new DirectoryInfo(_configuration.ControllerConfig.ReportFolder);
            var logFile = (from f in directory.GetFiles(FileWriter.REPORT_FILE_PATTERN)
                orderby f.LastWriteTime descending
                select f).First();

            // need to make sure the linefeeds are intact
            var reportStrings = File.ReadAllLines(logFile.FullName);
            var result = new StringBuilder();

            foreach (var reportString in reportStrings)
                result.AppendLine(reportString);

            return result.ToString();
        }

        /// <summary>
        ///     Checks the work items.
        /// </summary>
        private void CheckWorkItems()
        {
        }

        private void TimerCallback(object state)
        {
            var currentState = (WorkStatus) state;

            switch (currentState.Status)
            {
                case StateEnum.Started:
                    CheckServer();
                    break;
                case StateEnum.Stopped:
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    break;
                case StateEnum.Paused:
                    break;
            }
        }

        public void Pause()
        {
            State.Status = StateEnum.Paused;
        }
    }
}
// ***********************************************************************
// Assembly         : WireCli
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-10-2020
// ***********************************************************************
// <copyright file="CommandProcessor.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using WireAPI;
using WireBusinessLogic;
using WireCommon;
using WireConfig;

namespace WireCLI
{
    /// <summary>
    ///     Class CommandProcessor.
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    /// </summary>
    /// <seealso cref="WireCommon.IWireCommunicator" />
    public class CommandProcessor : IWireCommunicator
    {
        /// <summary>
        ///     The configuration file
        /// </summary>
        private readonly IConfigFile _configFile;

        /// <summary>
        ///     The controller
        /// </summary>
        private readonly Controller _controller;

        /// <summary>
        ///     The email API
        /// </summary>
        private readonly IEmailApi _emailAPI;

        /// <summary>
        ///     The help command.
        /// </summary>
        private readonly Command _helpCommand = new Command("help");

        /// <summary>
        ///     Backing field for the error handler action.
        /// </summary>
        private Action<Exception> _handleError;

        /// <summary>
        ///     Backing field for the log handler action.
        /// </summary>
        private Action<LogEntryType, string> _handleLog;

        /// <summary>
        ///     Backing field for the message handler action.
        /// </summary>
        private Action<string> _handleMessage;

        /// <summary>
        ///     Backing field for the report handler action.
        /// </summary>
        private Action<string> _handleReport;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="configFile">The configuration file.</param>
        public CommandProcessor(IConfigFile configFile)
        {
            _configFile = configFile;
            _controller = new Controller(new VSOApi(configFile.Configuration.VsoConfig),
                new EmailApi(configFile.Configuration.EMailConfig),
                configFile.Configuration);
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
                _configFile.HandleError += _handleError;
                _controller.HandleError += _handleError;
            }
        }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The message handler.</value>
        public Action<string> HandleMessage
        {
            get => _handleMessage;

            set
            {
                _handleMessage = value;
                _configFile.HandleMessage += _handleMessage;
                _controller.HandleMessage += _handleMessage;
            }
        }

        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>
        ///     The handle log.
        /// </value>
        public Action<LogEntryType, string> HandleLog
        {
            get => _handleLog;
            set
            {
                _handleLog = value;
                _configFile.HandleLog += _handleLog;
                _controller.HandleLog += _handleLog;
            }
        }

        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>
        ///     The handle report.
        /// </value>
        public Action<string> HandleReport
        {
            get => _handleReport;
            set
            {
                _handleReport = value;
                _configFile.HandleReport += _handleReport;
                _controller.HandleReport += _handleReport;
            }
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
        ///     Processes the help command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ProcessHelpCommand(Command command)
        {
            if (command.Values.Count == 1)
            {
                Message(Constants.GENERAL_HELP_TEXT);
            }
            else
            {
                Message(string.Empty);
                Message(_configFile.Configuration.GetHelp(command.Values[1]));
                Message(string.Empty);
            }
        }

        /// <summary>
        ///     Processes the command.
        /// </summary>
        /// <param name="commandString">The command string.</param>
        public void ProcessCommand(string commandString)
        {
            var command = new Command(commandString);

            switch (command.Values[0])
            {
                case "help":
                    ProcessHelpCommand(command);
                    break;

                case "config":
                    ProcessConfigCommand(command);
                    break;

                case "test":
                    ProcessTestCommand();
                    break;

                case "run":
                    ProcessRunCommand();
                    break;

                case "start":
                    ProcessStartCommand();
                    break;

                case "stop":
                    ProcessStopCommand();
                    break;

                case "pause":
                    ProcessPauseCommand();
                    break;

                case "print":
                    ProcessPrintCommand();
                    break;

                case "clear":
                    ProcessClearCommand(command);
                    break;

                case "send":
                    ProcessSendCommand(command);
                    break;

                case "status":
                    ProcessStatusCommand();
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        ///     Processes the send command.
        /// </summary>
        /// <param name="command">The send command.</param>
        private void ProcessSendCommand(Command command)
        {
            if (command.Values.Count == 1)
            {
                Error(new Exception("'send' must be followed by 'log' or 'report'"));
            }
            else
            {
                var sendParameter = command.Values[1].Trim().ToLower();

                if (sendParameter == "log")
                    ProcessSendLogCommand();
                else if (sendParameter == "report")
                    ProcessSendReportCommand();
                else
                    Error(new Exception($"send: unrecognized parameter '{sendParameter}'"));
            }
        }

        /// <summary>
        ///     Processes the test command.
        /// </summary>
        private void ProcessTestCommand()
        {
            _controller.Test();
        }

        /// <summary>
        ///     Processes the pause command.
        /// </summary>
        private void ProcessPauseCommand()
        {
            _controller.Pause();
        }

        /// <summary>
        ///     Processes the status command.
        /// </summary>
        private void ProcessStatusCommand()
        {
            switch (_controller.State.Status)
            {
                case StateEnum.Started:
                    Message("Running");
                    break;
                case StateEnum.Paused:
                    Message("Paused");
                    break;
                case StateEnum.Stopped:
                    Message("Stopped");
                    break;
            }

            Message($"Polling Interval: {_configFile.Configuration.ControllerConfig.PollingInterval} minutes");
            Message($"Reporting Interval: {_configFile.Configuration.ControllerConfig.ReportingInterval} minutes");
        }

        /// <summary>
        ///     Processes the send log command.
        /// </summary>
        private void ProcessSendLogCommand()
        {
            _controller.SendMostRecentLog();
        }

        /// <summary>
        ///     Processes the send report command.
        /// </summary>
        private void ProcessSendReportCommand()
        {
            _controller.SendMostRecentReport();
        }

        /// <summary>
        ///     Processes the print command.
        /// </summary>
        private void ProcessPrintCommand()
        {
            Message(_controller.GetLogContents());
        }

        /// <summary>
        ///     Processes the stop command.
        /// </summary>
        private void ProcessStopCommand()
        {
            Message("Stopping...");
            _controller.Stop();
            Message("Stopped");
        }

        /// <summary>
        ///     Processes the start command.
        /// </summary>
        private void ProcessStartCommand()
        {
            Message("Starting...");
            _controller.Start();
            Message("Started");
        }

        /// <summary>
        ///     Processes the run command.
        /// </summary>
        private void ProcessRunCommand()
        {
            _controller.Run();
        }

        /// <summary>
        ///     Processes the configuration command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ProcessConfigCommand(Command command)
        {
            switch (command.Values[1])
            {
                case "load":
                    if (command.Values.Count > 2)
                        _configFile.Load(command.Values[2]);
                    else
                        _configFile.Load();

                    break;

                case "save":
                    if (command.Values.Count > 2)
                        _configFile.Save(command.Values[2]);
                    else
                        _configFile.Save();

                    break;

                case "set":
                    ProcessSetCommand(command);
                    break;

                case "get":
                    ProcessGetCommand(command);
                    break;

                case "clear":
                    ProcessClearCommand(command);
                    break;

                case "delete":
                    ProcessDeleteCommand(command);
                    break;
            }
        }

        /// <summary>
        ///     Processes the delete command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ProcessDeleteCommand(Command command)
        {
            var commandCount = command.Values.Count;

            if (commandCount < 4)
                return;

            for (var i = 3; i < commandCount; i++)
                _configFile.Configuration.DeleteValue(command.Values[2], command.Values[i]);
        }

        /// <summary>
        ///     Processes the clear command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ProcessClearCommand(Command command)
        {
            _configFile.Configuration.ClearValue(command.Values[2]);
        }

        /// <summary>
        ///     Processes the get command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ProcessGetCommand(Command command)
        {
            int lowerBound;
            var commandCount = command.Values.Count;

            if (commandCount < 2)
                return;

            if (commandCount > 3 && command.Values[3].Contains("="))
                lowerBound = 3;
            else
                lowerBound = 2;

            for (var i = lowerBound; i < commandCount; i++)
            {
                var item = command.Values[i];
                dynamic result;

                if (item.Contains("="))
                {
                    var keyValuePair = command.Values[i].Split('=');
                    result = _configFile.Configuration.GetValue(keyValuePair[ConfigSettings.KEY],
                        keyValuePair[ConfigSettings.VALUE]);
                    this.Message(DisplayValue(command.Values[lowerBound], item, result));
                }
                else
                {
                    result = _configFile.Configuration.GetValue(item);
                    Message(DisplayValue(command.Values[i], item, result));
                }
            }
        }

        /// <summary>
        ///     Displays the value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string DisplayValue(string command, string key, dynamic value)
        {
            var displayName = _configFile.Configuration.GetDisplayName(command);
            return command.Equals(key) ? $"{displayName}: {value}" : $"{displayName}: {key}={value}";
        }

        /// <summary>
        ///     Processes the set command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void ProcessSetCommand(Command command)
        {
            var commandCount = command.Values.Count;

            if (commandCount < 3)
                return;

            if (command.Values[2].Contains("="))
            {
                for (var i = 2; i < commandCount; i++)
                    _configFile.Configuration.SetValue(command.Values[i]);
            }
            else if (commandCount > 3)
            {
                var category = command.Values[2];
                for (var i = 3; i < commandCount; i++)
                    _configFile.Configuration.SetValue(category, command.Values[i]);
            }
        }
    }
}
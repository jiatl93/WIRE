// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-28-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="ControllerConfig.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Newtonsoft.Json;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Class ControllerConfig.
    ///     Implements the <see cref="WireConfig.ConfigBase" />
    ///     Implements the <see cref="WireConfig.IControllerConfig" />
    /// </summary>
    /// <seealso cref="WireConfig.ConfigBase" />
    /// <seealso cref="WireConfig.IControllerConfig" />
    public class ControllerConfig : ConfigBase, IControllerConfig
    {
        /// <summary>
        ///     The pollinginterval key
        /// </summary>
        public const string POLLINGINTERVAL_KEY = "pollinginterval";

        /// <summary>
        ///     The reportinginterval key
        /// </summary>
        public const string REPORTINGINTERVAL_KEY = "reportinginterval";

        /// <summary>
        ///     The reportemail key
        /// </summary>
        public const string REPORTEMAIL_KEY = "reportemail";

        /// <summary>
        ///     The report folder key
        /// </summary>
        public const string REPORT_FOLDER_KEY = "reportfolder";

        /// <summary>
        ///     The log folder key
        /// </summary>
        public const string LOG_FOLDER_KEY = "logfolder";

        /// <summary>
        ///     The pollinginterval display
        /// </summary>
        public const string POLLINGINTERVAL_DISPLAY = "Polling Interval";

        /// <summary>
        ///     The reportinginterval display
        /// </summary>
        public const string REPORTINGINTERVAL_DISPLAY = "Reporting Interval";

        /// <summary>
        ///     The reportemail display
        /// </summary>
        public const string REPORTEMAIL_DISPLAY = "Report EMail";

        /// <summary>
        ///     The report folder display
        /// </summary>
        public const string REPORT_FOLDER_DISPLAY = "Report Folder";

        /// <summary>
        ///     The log folder display
        /// </summary>
        public const string LOG_FOLDER_DISPLAY = "Log Folder";

        /// <summary>
        ///     The pollinginterval help
        /// </summary>
        public static readonly string POLLINGINTERVAL_HELP =
            "SETTING NAME: " + POLLINGINTERVAL_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + POLLINGINTERVAL_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: The number of minutes between queries of the VSO server when WIRE is running in timed mode." +
            Environment.NewLine + Environment.NewLine +
            "To get " + POLLINGINTERVAL_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + POLLINGINTERVAL_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + POLLINGINTERVAL_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + POLLINGINTERVAL_KEY + "=60";

        /// <summary>
        ///     The reportinginterval help
        /// </summary>
        public static readonly string REPORTINGINTERVAL_HELP =
            "SETTING NAME: " + REPORTINGINTERVAL_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + REPORTINGINTERVAL_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: The maximum age of VSO items to query in minutes." +
            Environment.NewLine + Environment.NewLine +
            "To get " + REPORTINGINTERVAL_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + REPORTINGINTERVAL_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + REPORTINGINTERVAL_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + REPORTINGINTERVAL_KEY + "=1440";

        /// <summary>
        ///     The reportemail help
        /// </summary>
        public static readonly string REPORTEMAIL_HELP =
            "SETTING NAME: " + REPORTEMAIL_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + REPORTEMAIL_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: The email to which reports are sent." + Environment.NewLine + Environment.NewLine +
            "To get " + REPORTEMAIL_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + REPORTEMAIL_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + REPORTEMAIL_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + REPORTEMAIL_KEY + "=manager@contoso.com";

        /// <summary>
        ///     The report folder help
        /// </summary>
        public static readonly string REPORT_FOLDER_HELP =
            "SETTING NAME: " + REPORT_FOLDER_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + REPORT_FOLDER_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: The directory/folder in which reports are saved." +
            Environment.NewLine + Environment.NewLine +
            "To get " + REPORT_FOLDER_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + REPORT_FOLDER_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + REPORT_FOLDER_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + REPORT_FOLDER_KEY + "=c:\\WIRE-Reports";

        /// <summary>
        ///     The log folder help
        /// </summary>
        public static readonly string LOG_FOLDER_HELP =
            "SETTING NAME: " + LOG_FOLDER_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + LOG_FOLDER_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: The directory/folder in which logs are saved." +
            Environment.NewLine + Environment.NewLine +
            "To get " + LOG_FOLDER_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + LOG_FOLDER_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + LOG_FOLDER_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + LOG_FOLDER_KEY + "=c:\\WIRE-Logs";

        /// <summary>
        ///     Backing field for the error handler action.
        /// </summary>
        private Action<Exception> _handleError;

        /// <summary>
        ///     The handle log
        /// </summary>
        private Action<LogEntryType, string> _handleLog;

        /// <summary>
        ///     Backing field for the message handler action.
        /// </summary>
        private Action<string> _handleMessage;

        /// <summary>
        ///     The handle report
        /// </summary>
        private Action<string> _handleReport;

        /// <summary>
        ///     Gets or sets the interval in seconds at which the VSO data is polled for changes.
        /// </summary>
        /// <value>The polling interval.</value>
        public int PollingInterval { get; set; }

        /// <summary>
        ///     Gets or sets the interval in seconds at which compliance reports are sent to the
        ///     report email.
        /// </summary>
        /// <value>The reporting interval.</value>
        public int ReportingInterval { get; set; }

        /// <summary>
        ///     Gets or sets the email address to which compliance reports are sent.
        /// </summary>
        /// <value>The report email address.</value>
        public string ReportEmail { get; set; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public override void Init()
        {
            Settings.Add(POLLINGINTERVAL_KEY, new ConfigSettings(
                POLLINGINTERVAL_KEY, POLLINGINTERVAL_DISPLAY, POLLINGINTERVAL_HELP, SettingType.Single,
                x => PollingInterval = Convert.ToInt32(x), () => PollingInterval));

            Settings.Add(REPORTINGINTERVAL_KEY, new ConfigSettings(
                REPORTINGINTERVAL_KEY, REPORTINGINTERVAL_DISPLAY, REPORTINGINTERVAL_HELP, SettingType.Single,
                x => ReportingInterval = Convert.ToInt32(x), () => ReportingInterval));

            Settings.Add(REPORTEMAIL_KEY, new ConfigSettings(
                REPORTEMAIL_KEY, REPORTEMAIL_DISPLAY, REPORTEMAIL_HELP, SettingType.Single,
                x => ReportEmail = x, () => ReportEmail));

            Settings.Add(REPORT_FOLDER_KEY, new ConfigSettings(
                REPORT_FOLDER_KEY, REPORT_FOLDER_DISPLAY, REPORT_FOLDER_HELP, SettingType.Single,
                x => ReportFolder = x, () => ReportFolder));

            Settings.Add(LOG_FOLDER_KEY, new ConfigSettings(
                LOG_FOLDER_KEY, LOG_FOLDER_DISPLAY, LOG_FOLDER_HELP, SettingType.Single,
                x => LogFolder = x, () => LogFolder));
        }

        /// <summary>
        ///     Gets or sets the log folder.
        /// </summary>
        /// <value>The log folder.</value>
        public string LogFolder { get; set; }

        /// <summary>
        ///     Gets or sets the report folder.
        /// </summary>
        /// <value>The report folder.</value>
        public string ReportFolder { get; set; }

        /// <summary>
        ///     Gets or sets the error handler.
        /// </summary>
        /// <value>The error handler.</value>
        [JsonIgnore]
        public Action<Exception> HandleError
        {
            get => _handleError;
            set => _handleError = value;
        }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The handle message.</value>
        [JsonIgnore]
        public Action<string> HandleMessage
        {
            get => _handleMessage;
            set => _handleMessage = value;
        }

        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>The handle log.</value>
        public Action<LogEntryType, string> HandleLog
        {
            get => _handleLog;
            set => _handleLog = value;
        }

        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>The handle report.</value>
        public Action<string> HandleReport
        {
            get => _handleReport;
            set => _handleReport = value;
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
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="message">The message.</param>
        public void Log(LogEntryType entryType, string message)
        {
            HandleLog?.Invoke(entryType, message);
        }

        /// <summary>
        ///     Reports the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Report(string message)
        {
            HandleReport?.Invoke(message);
        }
    }
}
// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-23-2020
// ***********************************************************************
// <copyright file="TaskItemConfig.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Container for task item configuration instances.
    /// </summary>
    public class TaskItemConfig : IWireCommunicator
    {
        /// <summary>
        ///     The area path name key
        /// </summary>
        public const string AREA_PATH_NAME_KEY = "path";

        /// <summary>
        ///     The field name key
        /// </summary>
        public const string FIELD_NAME_KEY = "filedname";

        /// <summary>
        ///     The description key
        /// </summary>
        public const string DESCRIPTION_KEY = "description";

        /// <summary>
        ///     The validation regex key
        /// </summary>
        public const string VALIDATION_REGEX_KEY = "validationregex";

        /// <summary>
        ///     The help message key
        /// </summary>
        public const string HELP_MESSAGE_KEY = "helpmessage";

        /// <summary>
        ///     The grace period key
        /// </summary>
        public const string GRACE_PERIOD_KEY = "graceperiodinhours";

        /// <summary>
        ///     Regular Expression used for comparison.
        /// </summary>
        private Regex _compareRegex;

        /// <summary>
        ///     Regular Expression used for validation.
        /// </summary>
        private string _validationRegex;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemConfig" /> class.
        /// </summary>
        /// <param name="areaPath">The area path.</param>
        /// <param name="fieldPrefix">The field prefix.</param>
        /// <param name="fieldName">The Name.</param>
        /// <param name="description">The description.</param>
        /// <param name="validationRegex">The validation regex.</param>
        /// <param name="helpMessage">The help message.</param>
        /// <param name="gracePeriodInHours">The grace period in hours.</param>
        public TaskItemConfig(string areaPath, string fieldPrefix, string fieldName, string description, string validationRegex,
            string helpMessage, int gracePeriodInHours)
        {
            AreaPath = areaPath;
            FieldPrefix = fieldPrefix;
            FieldName = fieldName;
            Description = description;
            ValidationRegex = validationRegex;
            HelpMessage = helpMessage;
            GracePeriodInHours = gracePeriodInHours;
        }

        public string FieldPrefix { get; set; }

        /// <summary>
        ///     Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project the field belongs to.</value>
        public string AreaPath { get; set; }

        /// <summary>
        ///     Gets or sets the name of the field that is configured for compliance
        ///     testing.
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

        /// <summary>
        ///     Gets or sets the description of the field configured for compliance
        ///     testing.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the regular expression used for validation.
        /// </summary>
        /// <value>The regular expression.</value>
        public string ValidationRegex
        {
            get => _validationRegex;
            set
            {
                try
                {
                    _validationRegex = value;
                    _compareRegex = null;
                    _compareRegex = new Regex(_validationRegex);
                }
                catch (Exception e)
                {
                    Error(e);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the message shown when validation fails.
        /// </summary>
        /// <value>The help message.</value>
        public string HelpMessage { get; set; }

        /// <summary>
        ///     Gets or sets the grace period in hours, i.e. the window of time to pass before
        ///     that particular item is flagged for follow-up.
        /// </summary>
        /// <value>The grace period in hours.</value>
        public int GracePeriodInHours { get; set; }

        /// <summary>
        ///     Gets or sets the error handler.
        /// </summary>
        /// <value>The error handler.</value>
        [JsonIgnore]
        public Action<Exception> HandleError { get; set; }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The handle message.</value>
        [JsonIgnore]
        public Action<string> HandleMessage { get; set; }

        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>The handle log.</value>
        public Action<LogEntryType, string> HandleLog { get; set; }

        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>The handle report.</value>
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

        /// <summary>
        ///     Returns true if comparison of the supplied value with the regular
        ///     validation regex is valid.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid(dynamic value)
        {
            try
            {
                if (_compareRegex != null)
                    return _compareRegex.IsMatch(Convert.ToString(value));
            }
            catch (Exception e)
            {
                Error(e);
            }

            return false;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"    Area Path: {AreaPath}");
            result.AppendLine($"    Field Name: {FieldName}");
            result.AppendLine($"    Description: {Description}");
            result.AppendLine($"    Validation RegEx: {ValidationRegex}");
            result.AppendLine($"    Help Text: {HelpMessage}");
            result.AppendLine($"    Grace Period in Hours: {GracePeriodInHours}");

            return result.ToString();
        }
    }
}
// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="VSOConfig.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Class that encapsulates information for configuring access to Visual Studio Online.
    /// </summary>
    public class VSOConfig : ConfigBase, IVSOConfig
    {
        // Constants defining keys
        /// <summary>
        ///     The baseuri key
        /// </summary>
        public const string BASEURI_KEY = "baseuri";

        /// <summary>
        ///     The prompt for login key
        /// </summary>
        public const string PROMPT_FOR_LOGIN_KEY = "prompt";

        /// <summary>
        ///     The token key
        /// </summary>
        public const string TOKEN_KEY = "token";

        /// <summary>
        ///     The configitems key
        /// </summary>
        public const string CONFIGITEMS_KEY = "configitems";

        // Constants defining display names
        /// <summary>
        ///     The baseuri display
        /// </summary>
        public const string BASEURI_DISPLAY = "Base URI";

        /// <summary>
        ///     The prompt for login display
        /// </summary>
        public const string PROMPT_FOR_LOGIN_DISPLAY = "Login Prompt";

        /// <summary>
        ///     The token display
        /// </summary>
        public const string TOKEN_DISPLAY = "Personal Authentication Token";

        /// <summary>
        ///     The configitems display
        /// </summary>
        public const string CONFIGITEMS_DISPLAY = "Config Items";

        /// <summary>
        ///     The baseuri help
        /// </summary>
        public static readonly string BASEURI_HELP =
            "SETTING NAME: " + BASEURI_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + BASEURI_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: URI of the VSO server and base path containing the tasks WIRE checks for compliance." +
            Environment.NewLine + Environment.NewLine +
            "To get " + BASEURI_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + BASEURI_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + BASEURI_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + BASEURI_KEY + "=https://visualstdio.contoso.com/projects";

        /// <summary>
        ///     The prompt for login help
        /// </summary>
        public static readonly string PROMPT_FOR_LOGIN_HELP =
            "SETTING NAME: " + PROMPT_FOR_LOGIN_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + PROMPT_FOR_LOGIN_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: True or False value indicating whether WIRE should prompt for login. " +
            "If True, the program will prompt for an interactive login the first time it connects " +
            "to the server. Otherwise, it will use the configured " + TOKEN_DISPLAY + "." +
            Environment.NewLine + Environment.NewLine +
            "To get " + PROMPT_FOR_LOGIN_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + PROMPT_FOR_LOGIN_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + PROMPT_FOR_LOGIN_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + PROMPT_FOR_LOGIN_KEY + "=false";

        /// <summary>
        ///     The token help
        /// </summary>
        public static readonly string TOKEN_HELP =
            "SETTING NAME: " + TOKEN_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + TOKEN_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: Value of the " + TOKEN_DISPLAY + " used to connect to the VSO server. " +
            "For information on how to get a " + TOKEN_DISPLAY +
            ", see https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate." +
            Environment.NewLine + Environment.NewLine +
            "To get " + TOKEN_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + TOKEN_KEY + Environment.NewLine + Environment.NewLine +
            "To set " + TOKEN_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + TOKEN_KEY + "=1234567890123456789012345678901234567890123456789012";

        /// <summary>
        ///     The configitems help
        /// </summary>
        public static readonly string CONFIGITEMS_HELP =
            "SETTING NAME: " + CONFIGITEMS_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + CONFIGITEMS_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: List of items containing information on fields for which WIRE enforces rules. " +
            "In this version of WIRE, new " + CONFIGITEMS_DISPLAY +
            " must be added directly to the configuration file, " +
            "however exiting items can be queried and edited as described below." + Environment.NewLine +
            Environment.NewLine +
            "To get " + CONFIGITEMS_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config get " + CONFIGITEMS_KEY + " <field name>" + Environment.NewLine + Environment.NewLine +
            "To set a specific value for " + CONFIGITEMS_DISPLAY + ": " + Environment.NewLine + Environment.NewLine +
            "    config set " + CONFIGITEMS_KEY + " \"<field name>." + TaskItemConfig.AREA_PATH_NAME_KEY +
            "=product-build_tier-1\"" + Environment.NewLine + Environment.NewLine +
            "    config set " + CONFIGITEMS_KEY + " \"<field name>." + TaskItemConfig.DESCRIPTION_KEY +
            "=Description of the field\"" + Environment.NewLine + Environment.NewLine +
            "    config set " + CONFIGITEMS_KEY + " \"<field name>." + TaskItemConfig.VALIDATION_REGEX_KEY +
            "=^[\\w\\d]+$\"" + Environment.NewLine + Environment.NewLine +
            "    config set " + CONFIGITEMS_KEY + " \"<field name>." + TaskItemConfig.HELP_MESSAGE_KEY +
            "=Help for correctly using field\"" + Environment.NewLine + Environment.NewLine +
            "    config set " + CONFIGITEMS_KEY + " \"<field name>." + TaskItemConfig.GRACE_PERIOD_KEY + "=4\"";

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

        private string _token;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VSOConfig" /> class.
        /// </summary>
        public VSOConfig()
        {
            ConfigItems = new Dictionary<string, TaskItemConfig>();
        }

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
        ///     Gets or sets the base URI for the VSO instance being monitored.
        /// </summary>
        /// <value>The base URI.</value>
        public string BaseUri { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [prompt for login].
        /// </summary>
        /// <value><c>true</c> if [prompt for login]; otherwise, <c>false</c>.</value>
        public bool PromptForLogin { get; set; }

        /// <summary>
        ///     Gets or sets the personal authentication token used to authenticate to the VSO server.
        /// </summary>
        /// <value>The personal authentication token.</value>
        [JsonEncrypt]
        public string Token { get; set; }

        /// <summary>
        ///     Gets the dictionary configuration items stored in this instance.
        /// </summary>
        /// <value>The configuration items.</value>
        public Dictionary<string, TaskItemConfig> ConfigItems { get; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public override void Init()
        {
            Settings.Add(BASEURI_KEY, new ConfigSettings(
                BASEURI_KEY, BASEURI_DISPLAY, BASEURI_HELP, SettingType.Single,
                x => BaseUri = x, () => BaseUri));

            Settings.Add(PROMPT_FOR_LOGIN_KEY, new ConfigSettings(
                PROMPT_FOR_LOGIN_KEY, PROMPT_FOR_LOGIN_DISPLAY, PROMPT_FOR_LOGIN_HELP, SettingType.Single,
                x => PromptForLogin = Convert.ToBoolean(x), () => PromptForLogin));

            Settings.Add(TOKEN_KEY, new ConfigSettings(
                TOKEN_KEY, TOKEN_DISPLAY, TOKEN_HELP, SettingType.Single,
                x => Token = x, () => Token));

            Settings.Add(CONFIGITEMS_KEY, new ConfigSettings(
                CONFIGITEMS_KEY, CONFIGITEMS_DISPLAY, CONFIGITEMS_HELP, SettingType.Dictionary,
                null, () =>
                {
                    var result = new StringBuilder();
                    result.AppendLine();

                    foreach (var item in ConfigItems) result.AppendLine(item.Value.ToString());

                    return result.ToString();
                },
                s =>
                {
                    if (ConfigItems.ContainsKey(s))
                        return ConfigItems[s];
                    throw new ArgumentException(string.Format(Constants.PROPERTY_ERROR_FMT, s));
                },
                (s, d) =>
                {
                    var keyValue = s.Split('.');

                    if (ConfigItems.ContainsKey(keyValue[Constants.KEY]))
                    {
                        var item = ConfigItems[keyValue[Constants.KEY]];

                        switch (keyValue[Constants.VALUE].ToLower())
                        {
                            case TaskItemConfig.FIELD_NAME_KEY:
                                throw new Exception("Property 'Field' cannot be changed after initially created");
                                break;

                            case TaskItemConfig.AREA_PATH_NAME_KEY:
                                item.AreaPath = d;
                                break;

                            case TaskItemConfig.DESCRIPTION_KEY:
                                item.Description = d;
                                break;

                            case TaskItemConfig.VALIDATION_REGEX_KEY:
                                item.ValidationRegex = d;
                                break;

                            case TaskItemConfig.HELP_MESSAGE_KEY:
                                item.HelpMessage = d;
                                break;

                            case TaskItemConfig.GRACE_PERIOD_KEY:
                                item.GracePeriodInHours = Convert.ToInt32(d);
                                break;

                            default:
                                throw new ArgumentException(string.Format(Constants.PROPERTY_ERROR_FMT,
                                    keyValue[Constants.KEY]));
                                break;
                        }
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(Constants.PROPERTY_ERROR_FMT,
                            keyValue[Constants.KEY]));
                    }
                },
                s =>
                {
                    if (ConfigItems.ContainsKey(s))
                    {
                        ConfigItems[s] = null;
                        ConfigItems.Remove(s);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(Constants.PROPERTY_ERROR_FMT, s));
                    }
                },
                s => { ConfigItems.Clear(); }
            ));
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
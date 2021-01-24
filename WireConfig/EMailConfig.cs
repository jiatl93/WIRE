// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="EMailConfig.cs" company="Red Clay">
//     ${AuthorCopyright}
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
    ///     Class that encapsulates configuration information for sending emails.
    /// </summary>
    public class EMailConfig : ConfigBase, IEMailConfig
    {
        // Constants defining keys
        /// <summary>
        ///     The host key
        /// </summary>
        public const string HOST_KEY = "host";

        /// <summary>
        ///     The port key
        /// </summary>
        public const string PORT_KEY = "port";

        /// <summary>
        ///     The SSL key
        /// </summary>
        public const string SSL_KEY = "ssl";

        /// <summary>
        ///     The fromemail key
        /// </summary>
        public const string FROMEMAIL_KEY = "fromemail";

        /// <summary>
        ///     The username key
        /// </summary>
        public const string USERNAME_KEY = "username";

        /// <summary>
        ///     The password key
        /// </summary>
        public const string PASSWORD_KEY = "password";

        /// <summary>
        ///     The recipients key
        /// </summary>
        public const string RECIPIENTS_KEY = "recipients";

        // Constants defining display names
        /// <summary>
        ///     The host display
        /// </summary>
        public const string HOST_DISPLAY = "Host";

        /// <summary>
        ///     The port display
        /// </summary>
        public const string PORT_DISPLAY = "Port";

        /// <summary>
        ///     The SSL display
        /// </summary>
        public const string SSL_DISPLAY = "SSL";

        /// <summary>
        ///     The fromemail display
        /// </summary>
        public const string FROMEMAIL_DISPLAY = "From Email";

        /// <summary>
        ///     The username display
        /// </summary>
        public const string USERNAME_DISPLAY = "User Name";

        /// <summary>
        ///     The password display
        /// </summary>
        public const string PASSWORD_DISPLAY = "Password";

        /// <summary>
        ///     The recipients display
        /// </summary>
        public const string RECIPIENTS_DISPLAY = "Recipients";

        // Constants defining help strings
        /// <summary>
        ///     The host help
        /// </summary>
        public static readonly string HOST_HELP =
            "SETTING NAME: " + HOST_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + HOST_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: Name of the SMTP server used to send emails." + Environment.NewLine + Environment.NewLine +
            "To get the host name:" + Environment.NewLine + Environment.NewLine +
            "    config get host" + Environment.NewLine + Environment.NewLine +
            "To set the host name:" + Environment.NewLine + Environment.NewLine +
            "    config set host=smtp.contoso.com";

        /// <summary>
        ///     The port help
        /// </summary>
        public static readonly string PORT_HELP =
            "SETTING NAME: " + PORT_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + PORT_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: Integer representing the port over which the mail server communicates. " +
            Environment.NewLine +
            "            Values can be one of the following: " + Environment.NewLine + Environment.NewLine +
            "            25 - used for SMTP mail forwarding  " + Environment.NewLine +
            "            465 - secure port (SSL); deprecated " + Environment.NewLine +
            "            587 - secure (SSL and TLS); currently used " + Environment.NewLine +
            "            2525 - unofficial secure alternative; used by some providers" + Environment.NewLine +
            Environment.NewLine +
            "Please check with your ISP or IT department if you are unsure which applies." + Environment.NewLine +
            Environment.NewLine +
            "To get the port number:" + Environment.NewLine + Environment.NewLine +
            "    config get port" + Environment.NewLine + Environment.NewLine +
            "To set the port number:" + Environment.NewLine + Environment.NewLine +
            "    config set port=587";

        /// <summary>
        ///     The fromemail help
        /// </summary>
        public static readonly string FROMEMAIL_HELP =
            "SETTING NAME: " + FROMEMAIL_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + FROMEMAIL_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: EMail address from which reminders are sent." + Environment.NewLine + Environment.NewLine +
            "To get the from email address:" + Environment.NewLine + Environment.NewLine +
            "    config get fromemail" + Environment.NewLine + Environment.NewLine +
            "To set the from email address:" + Environment.NewLine + Environment.NewLine +
            "    config set fromemail=wire.account@constoso.com";

        /// <summary>
        ///     The recipients help
        /// </summary>
        public static readonly string RECIPIENTS_HELP =
            "SETTING NAME: " + RECIPIENTS_KEY + Environment.NewLine + Environment.NewLine +
            "DISPLAY NAME: " + RECIPIENTS_DISPLAY + Environment.NewLine + Environment.NewLine +
            "DEFINITION: List of potential recipients for reminders. Recipients will receive " +
            "reminders only for their own assigned work items. Entries are in the form of" + Environment.NewLine +
            Environment.NewLine +
            "    <user name=email address>, for example \"gsama=gregor.samsa@contoso.com\"" + Environment.NewLine +
            Environment.NewLine +
            "The user name must be the same as what appears in an assigned work item." + Environment.NewLine +
            Environment.NewLine +
            "To get all values in the recipients list:" + Environment.NewLine + Environment.NewLine +
            "    config get recipients" + Environment.NewLine + Environment.NewLine +
            "To set the email for John Doe, or create a new entry for John Doe if it does not exist: " +
            Environment.NewLine + Environment.NewLine +
            "    config set recipients \"John Doe=john.doe@contoso.com\"" + Environment.NewLine + Environment.NewLine +
            "To delete an entry:" + Environment.NewLine + Environment.NewLine +
            "    config delete recipients \"John Doe\"" + Environment.NewLine + Environment.NewLine +
            "To clear all recipients:" + Environment.NewLine + Environment.NewLine +
            "    config clear recipients";

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

        private string _fromEmail;
        private string _userName;
        private string _password;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EMailConfig" /> class.
        /// </summary>
        public EMailConfig()
        {
            Recipients = new Dictionary<string, string>();
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
        ///     Gets or sets the email host.
        /// </summary>
        /// <value>The email host.</value>
        public string Host { get; set; }

        /// <summary>
        ///     Gets or sets the port for the email host.
        /// </summary>
        /// <value>The host port.</value>
        public int Port { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="EMailConfig" /> uses SSL.
        /// </summary>
        /// <value><c>true</c> if SSL; otherwise, <c>false</c>.</value>
        public bool Ssl { get; set; }

        /// <summary>
        ///     Gets or sets the email address that emails will be sent with.
        /// </summary>
        /// <value>From email address.</value>
        public string FromEmail { get; set; }

        /// <summary>
        ///     Gets or sets the user name for the email server.
        /// </summary>
        /// <value>The user name for the email server.</value>
        [JsonEncrypt]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password to use with the email server.
        /// </summary>
        /// <value>The password for the email server.</value>
        [JsonEncrypt]
        public string Password { get; set; }
        
        /// <summary>
        ///     Email recipients keyed by user name.
        /// </summary>
        /// <value>The recipients list.</value>
        public Dictionary<string, string> Recipients { get; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public override void Init()
        {
            Settings.Add(HOST_KEY, new ConfigSettings(
                HOST_KEY, HOST_DISPLAY, HOST_HELP, SettingType.Single,
                x => Host = x, () => Host));

            Settings.Add(SSL_KEY, new ConfigSettings(
                SSL_KEY, SSL_DISPLAY, "", SettingType.Single,
                x => Ssl = Convert.ToBoolean(x), () => Ssl));

            Settings.Add(PORT_KEY, new ConfigSettings(
                PORT_KEY, PORT_DISPLAY, PORT_HELP, SettingType.Single,
                x => Port = Convert.ToInt32(x), () => Port));

            Settings.Add(FROMEMAIL_KEY, new ConfigSettings(
                FROMEMAIL_KEY, FROMEMAIL_DISPLAY, FROMEMAIL_HELP, SettingType.Single,
                x => FromEmail = x, () => FromEmail));

            Settings.Add(USERNAME_KEY, new ConfigSettings(
                USERNAME_KEY, USERNAME_DISPLAY, "", SettingType.Single,
                x => UserName = x, () => UserName));

            Settings.Add(PASSWORD_KEY, new ConfigSettings(
                PASSWORD_KEY, PASSWORD_DISPLAY, "", SettingType.Single,
                x => Password = x, () => Password));

            Settings.Add(RECIPIENTS_KEY, new ConfigSettings(
                RECIPIENTS_KEY, RECIPIENTS_DISPLAY, RECIPIENTS_HELP,
                SettingType.Dictionary,
                null, () =>
                {
                    var result = new StringBuilder();
                    result.AppendLine();

                    foreach (var item in Recipients) result.AppendLine($"  {item.Key} = {item.Value}");

                    return result.ToString();
                },
                s =>
                {
                    if (Recipients.ContainsKey(s))
                        return Recipients[s];
                    throw new ArgumentException(string.Format(Constants.PROPERTY_ERROR_FMT, s));
                },
                (s, d) =>
                {
                    if (Recipients.ContainsKey(s))
                        Recipients[s] = d;
                    else
                        Recipients.Add(s, d);
                },
                s =>
                {
                    if (Recipients.ContainsKey(s))
                    {
                        Recipients[s] = null;
                        Recipients.Remove(s);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(Constants.PROPERTY_ERROR_FMT, s));
                    }
                },
                s => { Recipients.Clear(); }
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
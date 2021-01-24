// ***********************************************************************
// Assembly         : WireAPI
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="EmailAPI.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MailKit.Net.Smtp;
using MimeKit;
using WireCommon;
using WireConfig;

namespace WireAPI
{
    /// <summary>
    ///     Wrapper class to simplify creating and sending emails through the System.Net.Mail
    ///     classes.
    /// </summary>
    public class EmailApi : IEmailApi
    {
        /// <summary>
        ///     The email configuration
        /// </summary>
        private readonly IEMailConfig _emailConfig;

        /// <summary>
        ///     The mail client
        /// </summary>
        private readonly SmtpClient _smtpMailClient;

        /// <summary>
        ///     The is disposed
        /// </summary>
        private bool isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailApi" /> class.
        /// </summary>
        /// <param name="emailConfig">The email configuration.</param>
        public EmailApi(IEMailConfig emailConfig)
        {
            _emailConfig = emailConfig;
            _smtpMailClient = new SmtpClient();
        }

        /// <summary>
        ///     Sends the reminder email to the specified party.
        /// </summary>
        /// <param name="recipientName">Name of the recipient.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        public void SendEmail(string recipientName, string emailAddress, string subject, string body,
            bool isHtml = true)
        {
            try
            {
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("WIRE client", "no_reply@contoso.com"));
                mailMessage.To.Add(new MailboxAddress(recipientName, emailAddress));
                mailMessage.Subject = subject;
                mailMessage.Body = new TextPart(isHtml ? "html" : "plain")
                {
                    Text = body
                };

                _smtpMailClient.Connect(_emailConfig.Host, _emailConfig.Port, _emailConfig.Ssl);
                _smtpMailClient.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                _smtpMailClient.Send(mailMessage);
                _smtpMailClient.Disconnect(true);
            }
            catch (Exception e)
            {
                Error(e);
            }
        }
        
        /// <summary>
        ///     Gets or sets the error handler.
        /// </summary>
        /// <value>The error handler.</value>
        public Action<Exception> HandleError { get; set; }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The handle message.</value>
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
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Sends the report to the administrator.
        /// </summary>
        public void SendReport()
        {
            // TODO: make this work
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="EmailApi" /> class.
        /// </summary>
        ~EmailApi()
        {
            if (_smtpMailClient.IsConnected)
                _smtpMailClient.Disconnect(true);
            Dispose(false);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
                // free managed resources
                _smtpMailClient.Dispose();

            isDisposed = true;
        }
    }
}
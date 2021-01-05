// ***********************************************************************
// Assembly         : WireCommon
// Author           : jiatli93
// Created          : 12-02-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-06-2020
// ***********************************************************************
// <copyright file="IWireCommunicator.cs" company="WireCommon">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Newtonsoft.Json;

namespace WireCommon
{
    /// <summary>
    ///     Interface for implementing a lightweight communication system.
    /// </summary>
    public interface IWireCommunicator
    {
        /// <summary>
        ///     Gets or sets the error handler.
        /// </summary>
        /// <value>The error handler.</value>
        [JsonIgnore]
        Action<Exception> HandleError { get; set; }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The handle message.</value>
        [JsonIgnore]
        Action<string> HandleMessage { get; set; }


        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>
        ///     The handle log.
        /// </value>
        [JsonIgnore]
        Action<LogEntryType, string> HandleLog { get; set; }


        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>
        ///     The handle report.
        /// </value>
        [JsonIgnore]
        Action<string> HandleReport { get; set; }

        /// <summary>
        ///     Calls the error handler with the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Error(Exception exception);

        /// <summary>
        ///     Calls the message handler with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Message(string message);

        /// <summary>
        ///     Logs the specified message tagged with the specified LogEntryType.
        /// </summary>
        /// <param name="entryType"></param>
        /// <param name="message">
        ///     The message.
        /// </param>
        void Log(LogEntryType entryType, string message);

        /// <summary>
        ///     Reports the specified message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        void Report(string message);
    }
}
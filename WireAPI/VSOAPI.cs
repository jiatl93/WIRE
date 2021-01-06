// ***********************************************************************
// Assembly         : WireAPI
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="VSOAPI.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using WireCommon;
using WireConfig;

namespace WireAPI
{
    /// <summary>
    ///     Class VSOApi.
    ///     Implements the <see cref="WireAPI.IVSOApi" />
    /// </summary>
    /// <seealso cref="WireAPI.IVSOApi" />
    public class VSOApi : IVSOApi
    {
        /// <summary>
        ///     The query FMT
        /// </summary>
        private const string QUERY_FMT =
            "SELECT * FROM [WorkItems] WHERE [Work Item Type] = 'Task' " +
            "AND [Changed Date] >= '{0}' " +
            "AND [Area Path] in ({1}) " +
            "AND [Assigned To] in ({2})";

        /// <summary>
        ///     The vso configuration
        /// </summary>
        private readonly IVSOConfig _vsoConfig;

        /// <summary>
        ///     The connection
        /// </summary>
        private VssConnection _connection;

        /// <summary>
        ///     The credentials
        /// </summary>
        private VssCredentials _credentials;

        /// <summary>
        ///     The is disposed
        /// </summary>
        private bool isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VSOApi" /> class.
        /// </summary>
        /// <param name="vsoConfig">The VSO configuration.</param>
        public VSOApi(IVSOConfig vsoConfig)
        {
            _vsoConfig = vsoConfig;
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
        ///     Connects this instance.
        /// </summary>
        public void Connect()
        {
            try
            {
                if (_credentials == null)
                    _credentials = _vsoConfig.PromptForLogin
                        ? new VssClientCredentials()
                        : new VssBasicCredential(string.Empty, _vsoConfig.Token);

                if (_connection == null)
                    _connection = new VssConnection(new Uri(_vsoConfig.BaseUri), _credentials);

                _connection.ConnectAsync();
            }
            catch (Exception e)
            {
                Error(e);
            }

            if (_connection != null) Message($"Connected to {_vsoConfig.BaseUri}");
        }

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _connection.Disconnect();
            }
            catch (Exception e)
            {
                Error(e);
            }

            if (_connection != null) Message($"Disconnected from {_vsoConfig.BaseUri}");
        }

        /// <summary>
        ///     Selects the items.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="teamProjects">The team projects.</param>
        /// <param name="assignedTo">The assigned to.</param>
        /// <returns>List&lt;WorkItem&gt;.</returns>
        public List<WorkItem> SelectItems(DateTime dateTime, IList<string> teamProjects, IList<string> assignedTo)
        {
            List<WorkItem> result = null;

            var projectList = StringListToSQLList(teamProjects);
            var assignedList = StringListToSQLList(assignedTo);
            var sqlDate = dateTime.ToString(Constants.SQL_DATE_FORMAT);
            var queryString = string.Format(QUERY_FMT, sqlDate, projectList, assignedList);
            var infoMessage = string.Format(Constants.LOG_CHECK_HEADER, _vsoConfig.BaseUri);

            Message(infoMessage);
            Log(LogEntryType.INFO, infoMessage);
            Report(infoMessage);

            try
            {
                var witClient = _connection.GetClient<WorkItemTrackingHttpClient>();
                var query = new Wiql {Query = queryString};
                var queryResults = witClient.QueryByWiqlAsync(query, true).Result;
                if (queryResults.WorkItems.Any())
                    result = witClient.GetWorkItemsAsync(queryResults.WorkItems.Select(wir => wir.Id)).Result;
            }
            catch (Exception e)
            {
                Error(e);
            }

            if (result != null)
                Message($"{result.Count()} work items returned");
            else
                Message("No work items returned");

            return result;
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
            Log(LogEntryType.ERROR, exception.Message);
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
            Log(LogEntryType.INFO, message);
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
        ///     Strings the list to SQL list.
        /// </summary>
        /// <param name="stringList">The string list.</param>
        /// <returns>System.String.</returns>
        private string StringListToSQLList(IList<string> stringList)
        {
            var result = new StringBuilder();

            for (var i = 0; i < stringList.Count; i++)
            {
                result.Append($"'{stringList[i]}'");
                if (i < stringList.Count - 1)
                    result.Append(",");
            }

            return result.ToString();
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="VSOApi" /> class.
        /// </summary>
        ~VSOApi()
        {
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
                _connection.Dispose();

            isDisposed = true;
        }
    }
}
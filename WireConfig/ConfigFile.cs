// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 10-02-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-23-2020
// ***********************************************************************
// <copyright file="ConfigFile.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using Newtonsoft.Json;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Class that encapsulates a configuration object and the means to
    ///     load and save it as a file.
    /// </summary>
    public class ConfigFile : IConfigFile, IWireCommunicator
    {
        /// <summary>
        ///     The default file name
        /// </summary>
        private const string DEFAULT_FILENAME = "wire-config.json";

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
        ///     The json settings
        /// </summary>
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
            {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigFile" /> class.
        /// </summary>
        public ConfigFile()
        {
            FileName = DEFAULT_FILENAME;
            Configuration = new Configuration();
        }

        /// <summary>
        ///     Delegate to handle errors in the caller.
        /// </summary>
        /// <value>The error handler.</value>
        [JsonIgnore]
        public Action<Exception> HandleError
        {
            get => _handleError;
            set
            {
                _handleError = value;
                Configuration.HandleError += _handleError;
            }
        }

        /// <summary>
        ///     Delegate to handle messages in the caller.
        /// </summary>
        /// <value>The handle message.</value>
        [JsonIgnore]
        public Action<string> HandleMessage
        {
            get => _handleMessage;
            set
            {
                _handleMessage = value;
                Configuration.HandleMessage += _handleMessage;
            }
        }

        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>The handle log.</value>
        public Action<LogEntryType, string> HandleLog
        {
            get => _handleLog;
            set
            {
                _handleLog = value;
                Configuration.HandleLog += _handleLog;
            }
        }

        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>The handle report.</value>
        public Action<string> HandleReport
        {
            get => _handleReport;
            set
            {
                _handleReport = value;
                Configuration.HandleReport += _handleReport;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the configuration file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets the configuration object which contains all the others.
        /// </summary>
        /// <value>The configuration.</value>
        public Configuration Configuration { get; private set; }

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        public void Load()
        {
            Load(FileName);
        }

        /// <summary>
        ///     Loads configuration from the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Load(string fileName)
        {
            try
            {
                var configString = File.ReadAllText(fileName);
                Configuration = JsonConvert.DeserializeObject<Configuration>(configString);
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        /// <summary>
        ///     Saves this instance.
        /// </summary>
        public void Save()
        {
            Save(FileName);
        }

        /// <summary>
        ///     Saves the configuration using the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName)
        {
            try
            {
                var configString = JsonConvert.SerializeObject(Configuration, Formatting.Indented, _jsonSettings);
                File.WriteAllText(fileName, configString);
            }
            catch (Exception exception)
            {
                Error(exception);
            }
        }

        /// <summary>
        ///     Calls the HandleError delegate, if it has been set.
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
        ///     Calls the MessageError delegate, if it has been set.
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
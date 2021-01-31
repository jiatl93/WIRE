// ***********************************************************************
// Assembly         : WireCommon
// Author           : wingf
// Created          : 12-16-2020
//
// Last Modified By : wingf
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="FileWriter.cs" company="WireCommon">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;

namespace WireCommon
{
    /// <summary>
    ///     Class FileWriter.
    /// </summary>
    public class FileWriter
    {
        /// <summary>
        ///     The log file name format
        /// </summary>
        private const string LOG_FILE_NAME_FORMAT = "wire_{0:yyyy-MM-dd_HH-mm-ss}.log";

        /// <summary>
        ///     The report file name format
        /// </summary>
        private const string REPORT_FILE_NAME_FORMAT = "wire-report_{0:yyyy-MM-dd_HH-mm-ss}.html";

        /// <summary>
        ///     The log entry format
        /// </summary>
        private const string LOG_ENTRY_FORMAT = "[{0:yyyy-MM-dd HH:mm:ss}] - [{1}]: {2}\n";

        /// <summary>
        ///     The report entry format
        /// </summary>
        private const string REPORT_ENTRY_FORMAT = "[{0:yyyy-MM-dd HH:mm:ss}]: {1}\n";

        /// <summary>
        ///     The log file pattern
        /// </summary>
        public const string LOG_FILE_PATTERN = "wire_*.log";

        /// <summary>
        ///     The report file pattern
        /// </summary>
        public const string REPORT_FILE_PATTERN = "wire-report_*.html";

        /// <summary>
        ///     The log folder
        /// </summary>
        private readonly string _logFolder;

        /// <summary>
        ///     The report folder
        /// </summary>
        private readonly string _reportFolder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileWriter" /> class.
        /// </summary>
        /// <param name="reportFolder">The report folder.</param>
        /// <param name="logFolder">The log folder.</param>
        public FileWriter(string reportFolder, string logFolder)
        {
            _reportFolder = reportFolder;
            _logFolder = logFolder;

            if (!Directory.Exists(_reportFolder))
                Directory.CreateDirectory(_reportFolder);

            if (!Directory.Exists(_logFolder))
                Directory.CreateDirectory(_logFolder);

            CurrentLogFileName =
                Path.Combine(_logFolder, string.Format(LOG_FILE_NAME_FORMAT, DateTime.Now));
        }

        /// <summary>
        ///     Gets the name of the current log file.
        /// </summary>
        /// <value>The name of the current log file.</value>
        public string CurrentLogFileName { get; }

        /// <summary>
        ///     Gets the name of the current report file.
        /// </summary>
        /// <value>The name of the current report file.</value>
        public string CurrentReportFileName { get; private set; }

        /// <summary>
        ///     Writes the log entry.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="s">The s.</param>
        public void WriteLogEntry(LogEntryType entryType, string s)
        {
            File.AppendAllText(CurrentLogFileName, string.Format(LOG_ENTRY_FORMAT, DateTime.Now,
                Enum.GetName(typeof(LogEntryType), entryType), s));
        }

        /// <summary>
        ///     Writes the report entry.
        /// </summary>
        /// <param name="s">The s.</param>
        public void WriteReportEntry(string s)
        {
            CurrentReportFileName =
                Path.Combine(_reportFolder, string.Format(REPORT_FILE_NAME_FORMAT, DateTime.Now));

            File.AppendAllText(CurrentReportFileName, string.Format(REPORT_ENTRY_FORMAT, DateTime.Now, s));
        }
    }
}
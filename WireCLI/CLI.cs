// ***********************************************************************
// Assembly         : WireCli
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-10-2020
// ***********************************************************************
// <copyright file="CLI.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using WireCli;
using WireCommon;
using WireConfig;

namespace WireCLI
{
    /// <summary>
    ///     Class Cli.
    /// </summary>
    internal static class Cli
    {
        /// <summary>
        ///     The exit
        /// </summary>
        private static bool _exit;

        /// <summary>
        ///     The configuration file
        /// </summary>
        private static ConfigFile _configFile;

        /// <summary>
        ///     The commands
        /// </summary>
        private static CommandProcessor _commands;

        private static FileWriter _fileWriter;

        /// <summary>
        ///     The prompt
        /// </summary>
        private static readonly string Prompt = "WIRE>";

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            _configFile = new ConfigFile();
            _configFile.Load();

            _fileWriter = new FileWriter(
                _configFile.Configuration.ControllerConfig.ReportFolder,
                _configFile.Configuration.ControllerConfig.LogFolder);

            _commands = new CommandProcessor(_configFile);
            _commands.HandleError += exception =>
            {
                ConsoleWriterAsync.Write(exception.Message + Environment.NewLine);
            };
            _commands.HandleMessage += s => { ConsoleWriterAsync.Write(s + Environment.NewLine); };
            _commands.HandleLog += (t, s) => { _fileWriter.WriteLogEntry(t, s); };
            _commands.HandleReport += s => { _fileWriter.WriteReportEntry(s); };

            while (!_exit)
            {
                ConsoleWriterAsync.Write(Prompt);
                var command = Console.ReadLine();
                ProcessCommand(command);
            }
        }

        /// <summary>
        ///     Processes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        private static void ProcessCommand(string command)
        {
            if (command == "exit" || command == "quit")
                _exit = true;
            else
                _commands.ProcessCommand(command);
        }
    }
}
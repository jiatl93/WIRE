// ***********************************************************************
// Assembly         : WireCli
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 11-15-2020
// ***********************************************************************
// <copyright file="Command.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using System.Text;
using NotVisualBasic.FileIO;

namespace WireCLI
{
    /// <summary>
    ///     Class that encapsulates a command object consisting of an action (Verb), a direct object
    ///     (CommandName), and a list of 0 or more values to operate on (Values).
    /// </summary>
    public class Command
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public Command()
        {
            Values = new List<string>();
        }

        /// <summary>
        ///     Parameterized constructor. The command text parameter is split on the space
        ///     character and used to populate the values in the object.
        /// </summary>
        /// <param name="commandText">String to be parsed into a command object</param>
        public Command(string commandText) : this()
        {
            // convert commandText to stream
            var byteArray = Encoding.Default.GetBytes(commandText);
            var commandStream = new MemoryStream(byteArray);
            // we want to make sure that values in quotes preserve spaces
            var parser = new CsvTextFieldParser(commandStream);

            parser.SetDelimiter(' '); // delimiters are spaces not embedded in quoted strings
            parser.TrimWhiteSpace = true;
            var commandArray = parser.ReadFields();

            if (commandArray.Length > 0)
            {
                for (var i = 0; i < commandArray.Length; i++)
                {
                    var s = commandArray[i].Trim();

                    if (!string.IsNullOrEmpty(s)) Values.Add(s);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the command help.
        /// </summary>
        /// <value>The command help.</value>
        public string CommandHelp { get; set; }

        /// <summary>
        ///     Gets the value list .
        /// </summary>
        /// <value>The values.</value>
        public List<string> Values { get; }
    }
}
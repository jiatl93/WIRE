// ***********************************************************************
// Assembly         : WireCli
// Author           : jiatli93
// Created          : 12-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-19-2020
// ***********************************************************************
// <copyright file="ConsoleWriterAsync.cs" company="">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Threading;
using WireCommon;

namespace WireCli
{
    /// <summary>
    ///     Class ConsoleWriterAsync.
    /// </summary>
    internal class ConsoleWriterAsync
    {
        /// <summary>
        ///     The string last written to the console.
        /// </summary>
        private static string lastWritten;

        /// <summary>
        ///     The queue
        /// </summary>
        private static readonly BlockingCollection<string> _queue = new BlockingCollection<string>();

        /// <summary>
        ///     Initializes static members of the <see cref="ConsoleWriterAsync" /> class.
        /// </summary>
        static ConsoleWriterAsync()
        {
            var thread = new Thread(
                () =>
                {
                    // HACK: this is NOT the best way to ensure the prompt is restored
                    // after asynchronous writing to the console, but it worked and I 
                    // need to learn more about async stuff to get at the "proper" way
                    var timer = new Timer(o =>
                    {
                        if (lastWritten != Constants.PROMPT) Write(Constants.PROMPT);
                    }, lastWritten, 1000, 5000);

                    while (true)
                    {
                        lastWritten = _queue.Take();
                        Console.Write(lastWritten);
                    }
                });
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        ///     Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public static void Write(string value)
        {
            _queue.Add(value);
        }
    }
}
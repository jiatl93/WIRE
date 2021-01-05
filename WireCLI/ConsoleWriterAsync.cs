// ***********************************************************************
// Assembly         : WireCli
// Author           : wingf
// Created          : 12-15-2020
//
// Last Modified By : wingf
// Last Modified On : 12-19-2020
// ***********************************************************************
// <copyright file="ConsoleWriterAsync.cs" company="">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace WireCli
{
    /// <summary>
    ///     Class ConsoleWriterAsync.
    /// </summary>
    internal class ConsoleWriterAsync
    {
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
                    while (true) Console.Write(_queue.Take());
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
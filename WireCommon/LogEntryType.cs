// ***********************************************************************
// Assembly         : WireCommon
// Author           : wingf
// Created          : 12-23-2020
//
// Last Modified By : wingf
// Last Modified On : 12-23-2020
// ***********************************************************************
// <copyright file="LogEntryType.cs" company="WireCommon">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WireCommon
{
    /// <summary>
    ///     Enum LogEntryType
    /// </summary>
    public enum LogEntryType
    {
        /// <summary>
        ///     The debug
        /// </summary>
        DEBUG,

        /// <summary>
        ///     The information
        /// </summary>
        INFO,

        /// <summary>
        ///     The warning
        /// </summary>
        WARNING,

        /// <summary>
        ///     The error
        /// </summary>
        ERROR,

        /// <summary>
        ///     The fatal
        /// </summary>
        FATAL
    }
}
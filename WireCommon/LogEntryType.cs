// ***********************************************************************
// Assembly         : WireCommon
// Author           : jiatli93
// Created          : 12-23-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-23-2020
// ***********************************************************************
// <copyright file="LogEntryType.cs" company="WireCommon">
//     Copyright ©2020 RedClay LLC. All rights reserved.
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
        ///     Debug entry
        /// </summary>
        DEBUG,

        /// <summary>
        ///     Information entry
        /// </summary>
        INFO,

        /// <summary>
        ///     Warning entry
        /// </summary>
        WARNING,

        /// <summary>
        ///     Error entry
        /// </summary>
        ERROR,

        /// <summary>
        ///     Fatal entry
        /// </summary>
        FATAL
    }
}
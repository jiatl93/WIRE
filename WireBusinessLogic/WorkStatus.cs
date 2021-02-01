// ***********************************************************************
// Assembly         : WireBusinessLogic
// Author           : jiatli93
// Created          : 12-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-15-2020
// ***********************************************************************
// <copyright file="WorkStatus.cs" company="">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WireBusinessLogic
{
    /// <summary>
    ///     Enum StateEnum
    /// </summary>
    public enum StateEnum
    {
        /// <summary>
        ///     The started
        /// </summary>
        Started,

        /// <summary>
        ///     The paused
        /// </summary>
        Paused,

        /// <summary>
        ///     The stopped
        /// </summary>
        Stopped
    }

    /// <summary>
    ///     Class WorkStatus.
    /// </summary>
    public class WorkStatus
    {
        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public StateEnum Status { get; set; }
    }
}
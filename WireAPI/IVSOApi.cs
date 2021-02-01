// ***********************************************************************
// Assembly         : WireAPI
// Author           : jiatli93
// Created          : 12-10-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-20-2020
// ***********************************************************************
// <copyright file="IVSOApi.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WireCommon;

namespace WireAPI
{
    /// <summary>
    ///     Interface IVSOApi
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    ///     Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="WireCommon.IWireCommunicator" />
    /// <seealso cref="System.IDisposable" />
    public interface IVSOApi : IWireCommunicator, IDisposable
    {
        /// <summary>
        ///     Connects this instance.
        /// </summary>
        void Connect();

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        void Disconnect();

        /// <summary>
        ///     Selects the items.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="teamProjects">The team projects.</param>
        /// <param name="assignedTo">The assigned to.</param>
        /// <returns>List&lt;WorkItem&gt;.</returns>
        List<WorkItem> SelectItems(DateTime dateTime, IList<string> teamProjects, IList<string> assignedTo);
    }
}
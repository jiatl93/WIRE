// ***********************************************************************
// Assembly         : WireBusinessLogic
// Author           : jiatli93
// Created          : 01-14-2021
//
// Last Modified By : jiatli93
// Last Modified On : 01-24-2021
// ***********************************************************************
// <copyright file="TaskItemErrors.cs" company="Red Clay">
//     Copyright ©2021 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using WireConfig;

namespace WireBusinessLogic
{
    /// <summary>
    ///     Class to contain the errors for a particular work item
    ///     for a particular user.
    /// </summary>
    public class TaskItemErrors
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskItemErrors" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="workItemId">The work item identifier.</param>
        /// <param name="workItemTitle">The work item title.</param>
        /// <param name="workItemWorkItemUrl">The work item work item URL.</param>
        public TaskItemErrors(string userName, int workItemId, string workItemTitle,
            string workItemWorkItemUrl)
        {
            UserName = userName;
            WorkItemID = workItemId;
            WorkItemTitle = workItemTitle;
            WorkItemURL = workItemWorkItemUrl;
            ErrorItems = new List<Tuple<TaskItemConfig, bool>>();
        }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the work item identifier.
        /// </summary>
        /// <value>
        ///     The work item identifier.
        /// </value>
        public int WorkItemID { get; set; }

        /// <summary>
        ///     Gets or sets the work item title.
        /// </summary>
        /// <value>
        ///     The work item title.
        /// </value>
        public string WorkItemTitle { get; set; }

        /// <summary>
        ///     Gets or sets the work item URL.
        /// </summary>
        /// <value>
        ///     The work item URL.
        /// </value>
        public string WorkItemURL { get; set; }

        /// <summary>
        ///     Gets the error items.
        /// </summary>
        /// <value>
        ///     The error items.
        /// </value>
        public List<Tuple<TaskItemConfig, bool>> ErrorItems { get; }

        /// <summary>
        ///     Adds the error item.
        /// </summary>
        /// <param name="errorItem">The error item.</param>
        /// <param name="hasValueForField">if set to <c>true</c> [has value for field].</param>
        public void AddErrorItem(TaskItemConfig errorItem, bool hasValueForField)
        {
            ErrorItems.Add(new Tuple<TaskItemConfig, bool>(errorItem, hasValueForField));
        }
    }
}
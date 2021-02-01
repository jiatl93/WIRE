// ***********************************************************************
// Assembly         : WireCommon
// Author           : jiatli93
// Created          : 12-06-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="Constants.cs" company="WireCommon">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace WireCommon
{
    /// <summary>
    ///     Class Constants.
    /// </summary>
    public class Constants
    {
        /// <summary>
        ///     The prompt string for our console
        /// </summary>
        public const string PROMPT = "WIRE>";

        /// <summary>
        ///     The key
        /// </summary>
        public const int KEY = 0;

        /// <summary>
        ///     The value
        /// </summary>
        public const int VALUE = 1;

        /// <summary>
        ///     The encryption key
        /// </summary>
        public const string ENCRYPTION_KEY = "55sH*IXuzIxawvMvK*^1pRL#wWv8^@#Q";

        /// <summary>
        ///     The property set confirmation FMT
        /// </summary>
        public const string PROPERTY_SET_CONFIRMATION_FMT = "Setting {0} successfully changed to {1}";

        /// <summary>
        ///     The property set confirmation FMT
        /// </summary>
        public const string DICTIONARY_PROPERTY_SET_CONFIRMATION_FMT = "Setting {0}: {1} successfully changed to {2}";

        /// <summary>
        ///     The property delete confirmation FMT
        /// </summary>
        public const string PROPERTY_DELETE_CONFIRMATION_FMT = "Setting {0} successfully deleted";

        /// <summary>
        ///     The SQL date format
        /// </summary>
        public const string SQL_DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss.ffZ";

        /// <summary>
        ///     The work item assigned to
        /// </summary>
        public const string WORK_ITEM_ASSIGNED_TO = "System.AssignedTo";

        /// <summary>
        ///     The work item title
        /// </summary>
        public const string WORK_ITEM_TITLE = "System.Title";

        /// <summary>
        ///     The log check header
        /// </summary>
        public const string LOG_CHECK_HEADER = "Checking work items on {0}...";

        /// <summary>
        ///     The log email subject format
        /// </summary>
        public const string LOG_EMAIL_SUBJECT_FORMAT = "Most recent log as of {0:yyyy-MM-dd, HH:mm}";

        /// <summary>
        ///     The log reminder field line
        /// </summary>
        public const string LOG_REMINDER_FIELD_LINE = "{0}: {1}";

        /// <summary>
        ///     The no value format
        /// </summary>
        public const string NO_VALUE_FORMAT = "    NO VALUE - {0}";

        /// <summary>
        ///     The bad value format
        /// </summary>
        public const string BAD_VALUE_FORMAT = "    BAD VALUE - {0}";

        /// <summary>
        ///     The report email subject format
        /// </summary>
        public static string REPORT_EMAIL_SUBJECT_FORMAT = "Work Item non-compliance report {0:yyyy-MM-dd, HH:mm}";

        /// <summary>
        ///     The report HTML format
        /// </summary>
        public static string REPORT_HTML_FORMAT =
            "<html><head><meta content=\"text/html; charset=ISO-8859-1\" http-equiv=\"content-type\"><title></title>" +
            Environment.NewLine +
            "</head><body><table style=\"text-align: left; width: 100%;\" border=\"1\" cellpadding=\"5\" cellspacing=\"0\"><tbody>" +
            Environment.NewLine +
            "{0}" + Environment.NewLine +
            "</tbody></table><br></body></html>";

        /// <summary>
        ///     The general help text displayed when the user just types 'help' with
        ///     no other parameters.
        /// </summary>
        public static string GENERAL_HELP_TEXT =
            Environment.NewLine +
            "help - Gets help for a command. Syntax: help <command name>" + Environment.NewLine +
            "config - Gets or sets configuration values." + Environment.NewLine +
            "run - Runs the process to query the VSO server, evaluate, and send reminders." + Environment.NewLine +
            "test - Runs the process to query the VSO server, evaluate, but only sends report." + Environment.NewLine +
            "start - Starts the VSO query process to run at the configured interval." + Environment.NewLine +
            "stop - Stops the timed running of the VSO query process." + Environment.NewLine +
            "pause - Toggles pausing the timed query process." + Environment.NewLine +
            "print - Prints the most recent report to the screen." + Environment.NewLine +
            "clear - Clears the specified list dictionary config item." + Environment.NewLine +
            "send - Sends the most recent report or log to the configured report email address." + Environment.NewLine +
            "status - Gets the status of the query process - i.e., running, stopped, or paused." + Environment.NewLine;

        /// <summary>
        ///     The property error FMT
        /// </summary>
        public static readonly string PROPERTY_ERROR_FMT = "Unrecognized property - '{0}'. " +
                                                           Environment.NewLine +
                                                           "Please check the spelling of the property name.";

        /// <summary>
        ///     The email subject format
        /// </summary>
        public static readonly string USER_EMAIL_SUBJECT = "Work Item(s) in need of correction";

        /// <summary>
        ///     The user email body
        /// </summary>
        public static string USER_EMAIL_BODY =
            "<html><head><meta content=\"text/html; charset=ISO-8859-1\" http-equiv=\"content-type\"><title></title></head>" +
            Environment.NewLine +
            "<body><span style=\"font-family: Helvetica,Arial,sans-serif;\">This is an automated email concerning the " +
            Environment.NewLine +
            "following error(s) detected in work item(s) assigned to you. Please address them to prevent " +
            Environment.NewLine +
            "further notifications. For more information please see the <a href=\"https://osgwiki.com/wiki/CSD_Ops_-_Tier_1_Request\">" +
            Environment.NewLine +
            "Tier 1 Request</a> page:</span><br>" + Environment.NewLine +
            "<br><div><table style=\"text-align: left; width: 1495px;\" border=\"1\" cellpadding=\"5\" cellspacing=\"0\"><tbody>" +
            Environment.NewLine +
            "{0}</tbody></table></div></body></html>";

        /// <summary>
        ///     The user email task header
        /// </summary>
        public static string USER_EMAIL_TASK_HEADER =
            "<tr><td colspan=\"4\" rowspan=\"1\" style=\"margin: 0px; font-family: Helvetica,Arial,sans-serif; vertical-align: top; background-color: blue;\">" +
            Environment.NewLine +
            "<span style=\"color: yellow; font-weight: bold;\">Work Item</span><br></td></tr>" + Environment.NewLine +
            "<tr><td colspan=\"4\" rowspan=\"1\" style=\"margin: 0px; font-family: Helvetica,Arial,sans-serif; vertical-align: top;\">" +
            Environment.NewLine +
            "<a href=\"{0}\">{1}</a><br></td>" + Environment.NewLine +
            "</tr><tr><th style=\"vertical-align: top; font-family: Helvetica,Arial,sans-serif; background-color: blue; color: yellow; font-weight: bold;\">Error Type<br></th>" +
            Environment.NewLine +
            "<th style=\"vertical-align: top; font-family: Helvetica,Arial,sans-serif; background-color: blue; color: yellow; font-weight: bold;\">Field Name<br></th>" +
            Environment.NewLine +
            "<th style=\"vertical-align: top; font-family: Helvetica,Arial,sans-serif; background-color: blue; color: yellow; font-weight: bold;\">Description<br></th>" +
            Environment.NewLine +
            "<th style=\"vertical-align: top; font-family: Helvetica,Arial,sans-serif; background-color: blue; color: yellow; font-weight: bold;\">Requirements<br></th></tr>";

        /// <summary>
        ///     The user email task line item
        /// </summary>
        public static string USER_EMAIL_TASK_LINE_ITEM =
            "<tr><td style=\"margin: 0px; font-family: Helvetica,Arial,sans-serif; vertical-align: top; text-align: center;\">" +
            Environment.NewLine +
            "<span style=\"color: red; font-weight: bold;\">{0}</span><br></td>" + Environment.NewLine +
            "<td style=\"margin: 0px; font-family: Helvetica,Arial,sans-serif; vertical-align: top;\">{1}<br></td>" +
            Environment.NewLine +
            "<td style=\"margin: 0px; font-family: Helvetica,Arial,sans-serif; vertical-align: top;\">{2}<br></td>" +
            Environment.NewLine +
            "<td style=\"margin: 0px; font-family: Helvetica,Arial,sans-serif; vertical-align: top;\">{3}<br></td></tr>";
    }
}
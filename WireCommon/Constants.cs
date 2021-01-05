// ***********************************************************************
// Assembly         : WireCommon
// Author           : jiatli93
// Created          : 12-06-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="Constants.cs" company="WireCommon">
//     Copyright (c) . All rights reserved.
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
        ///     The key
        /// </summary>
        public const int KEY = 0;

        /// <summary>
        ///     The value
        /// </summary>
        public const int VALUE = 1;

        /// <summary>
        ///     The property set confirmation FMT
        /// </summary>
        public const string PROPERTY_SET_CONFIRMATION_FMT = "Setting {0} succesfully changed to {1}";

        /// <summary>
        ///     The property set confirmation FMT
        /// </summary>
        public const string DICTIONARY_PROPERTY_SET_CONFIRMATION_FMT = "Setting {0}: {1} succesfully changed to {2}";

        /// <summary>
        ///     The property delete confirmation FMT
        /// </summary>
        public const string PROPERTY_DELETE_CONFIRMATION_FMT = "Setting {0} succesfully deleted";

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
        ///     The log reminder header
        /// </summary>
        public const string LOG_REMINDER_HEADER = "Composing reminder for {0}.\nItem: {1}\nFields: ";

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
        ///     The general help text displayed when the user just types 'help' with
        ///     no other parameters.
        /// </summary>
        public static string GENERAL_HELP_TEXT = 
            Environment.NewLine +
            "help - Gets help for a command. Syntax: help <command name>" + Environment.NewLine +
            "config - Gets or sets configuration values." + Environment.NewLine +
            "run - Runs the process to query the VSO server, evaluate, and send reminders." + Environment.NewLine +
            "start - Starts the VSO query process to run at the configured interval." + Environment.NewLine +
            "stop - Stops the timed running of the VSO query process." + Environment.NewLine +
            "pause - Toggles pausing the timed query process." + Environment.NewLine +
            "print - Prints the most recent report to the screen." + Environment.NewLine +
            "clear - Clears the specified list dictionary config item." + Environment.NewLine +
            "send - Sends the most recent report to the configured report email address." + Environment.NewLine +
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
        public static readonly string EMAIL_SUBJECT_FORMAT = "Work Item {0} - {1}";

        /// <summary>
        ///     The email body format
        /// </summary>
        public static readonly string EMAIL_BODY_FORMAT =
            "<html>" + Environment.NewLine +
            "<head>" + Environment.NewLine +
            "    <meta content=\"text/html; charset=ISO-8859-1\" http-equiv=\"content-type\">" + Environment.NewLine +
            "    <title></title>" + Environment.NewLine +
            "</head>" + Environment.NewLine +
            "<body>" + Environment.NewLine +
            "    <p class=\"MsoNormal\"" + Environment.NewLine +
            "        style=\"margin: 0in; color: rgb(34, 34, 34); font-style: normal; font-weight: 400; " +
            "letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; " +
            "white-space: normal; widows: 2; word-spacing: 0px; background-color: rgb(255, 255, 255); " +
            "font-size: 11pt; font-family: Calibri,sans-serif;\">" + Environment.NewLine +
            "        Hi,<br>" + Environment.NewLine +
            "    </p>" + Environment.NewLine +
            "    <p class=\"MsoNormal\"" + Environment.NewLine +
            "        style=\"margin: 0in; color: rgb(34, 34, 34); font-style: normal; font-weight: 400; " +
            "letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; " +
            "white-space: normal; widows: 2; word-spacing: 0px; background-color: rgb(255, 255, 255); " +
            "font-size: 11pt; font-family: Calibri,sans-serif;\">" + Environment.NewLine +
            "        <br>" + Environment.NewLine +
            "    </p>" + Environment.NewLine +
            "    <p class=\"MsoNormal\"" + Environment.NewLine +
            "        style=\"margin: 0in; color: rgb(34, 34, 34); font-style: normal; font-weight: 400; " +
            "letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; " +
            "white-space: normal; widows: 2; word-spacing: 0px; background-color: rgb(255, 255, 255); " +
            "font-size: 11pt; font-family: Calibri,sans-serif;\">" + Environment.NewLine +
            "        This" + Environment.NewLine +
            "        is an automated email concerning <a" + Environment.NewLine +
            "            href=\"{0}\"" + Environment.NewLine +
            "            target=\"_blank\"" + Environment.NewLine +
            "            style=\"color: rgb(17, 85, 204); font-family: Arial,Helvetica,sans-serif; " +
            "font-size: small; font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; " +
            "text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; " +
            "word-spacing: 0px; background-color: rgb(255, 255, 255);\">Work" + Environment.NewLine +
            "            Item {1} - {2}</a><span" + Environment.NewLine +
            "            style=\"color: rgb(80, 0, 80); font-family: Arial,Helvetica,sans-serif; " +
            "font-size: small; font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; " +
            "text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; " +
            "word-spacing: 0px; background-color: rgb(255, 255, 255); display: inline ! important; " +
            "float: none;\"><span></span></span>," + Environment.NewLine +
            "        in which the following errors were detected. Please address them to" + Environment.NewLine +
            "        prevent further notifications:<br>" + Environment.NewLine +
            "    </p>" + Environment.NewLine +
            "    <p class=\"MsoNormal\"" + Environment.NewLine +
            "        style=\"margin: 0in; color: rgb(34, 34, 34); font-style: normal; font-weight: 400; " +
            "letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; " +
            "white-space: normal; widows: 2; word-spacing: 0px; background-color: rgb(255, 255, 255); " +
            "font-size: 11pt; font-family: Calibri,sans-serif;\">" + Environment.NewLine +
            "        <b></b><br>" + Environment.NewLine +
            "    </p>" + Environment.NewLine +
            "    <table" + Environment.NewLine +
            "        style=\"color: rgb(34, 34, 34); font-family: Arial,Helvetica,sans-serif; font-size: small; " +
            "font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start;" +
            " text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; " +
            "background-color: rgb(255, 255, 255); border-collapse: collapse;\"" + Environment.NewLine +
            "        border=\"0\" cellpadding=\"0\" cellspacing=\"0\">" + Environment.NewLine +
            "        <thead>" + Environment.NewLine +
            "            <tr>" + Environment.NewLine +
            "                <td" + Environment.NewLine +
            "                    style=\"border: 1pt solid rgb(173, 186, 220); margin: 0px; padding: 6pt; " +
            "background: rgb(49, 68, 114) none repeat scroll 0%; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; " +
            "-moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; " +
            "-moz-background-inline-policy: -moz-initial;\">" + Environment.NewLine +
            "                    <p class=\"MsoNormal\"" + Environment.NewLine +
            "                        style=\"margin: 0in 0in 12pt; text-align: center; font-size: 11pt; " +
            "font-family: Calibri,sans-serif;\"" + Environment.NewLine +
            "                        align=\"center\"><b><span style=\"color: rgb(245, 209, 53);\">Error Type<br>" +
            Environment.NewLine +
            "                            </span></b></p>" + Environment.NewLine +
            "                </td>" + Environment.NewLine +
            "                <td" + Environment.NewLine +
            "                    style=\"border: 1pt solid rgb(173, 186, 220); margin: 0px; padding: 6pt; " +
            "background: rgb(49, 68, 114) none repeat scroll 0%; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; " +
            "-moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; " +
            "-moz-background-inline-policy: -moz-initial;\">" + Environment.NewLine +
            "                    <p class=\"MsoNormal\"" + Environment.NewLine +
            "                        style=\"margin: 0in 0in 12pt; text-align: center; font-size: 11pt; " +
            "font-family: Calibri,sans-serif;\"" + Environment.NewLine +
            "                        align=\"center\"><b><span style=\"color: rgb(245, 209, 53);\">Field Name<br>" +
            Environment.NewLine +
            "                            </span></b></p>" + Environment.NewLine +
            "                </td>" + Environment.NewLine +
            "                <td" + Environment.NewLine +
            "                    style=\"border-style: solid solid solid none; border-color: rgb(173, 186, 220) " +
            "rgb(173, 186, 220) rgb(173, 186, 220) -moz-use-text-color; border-width: 1pt 1pt 1pt medium; " +
            "margin: 0px; padding: 6pt; background: rgb(49, 68, 114) none repeat scroll 0%; " +
            "font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; -moz-background-clip: -moz-initial; " +
            "-moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial;\">" +
            Environment.NewLine +
            "                    <p class=\"MsoNormal\"" + Environment.NewLine +
            "                        style=\"margin: 0in 0in 12pt; text-align: center; font-size: 11pt; " +
            "font-family: Calibri,sans-serif;\"" + Environment.NewLine +
            "                        align=\"center\"><b><span style=\"color: rgb(245, 209, 53);\">" +
            "Description</span></b></p>" + Environment.NewLine +
            "                </td>" + Environment.NewLine +
            "                <td" + Environment.NewLine +
            "                    style=\"border-style: solid solid solid none; border-color: rgb(173, 186, 220) " +
            "rgb(173, 186, 220) rgb(173, 186, 220) -moz-use-text-color; border-width: 1pt 1pt 1pt medium; margin: 0px; " +
            "padding: 6pt; background: rgb(49, 68, 114) none repeat scroll 0%; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; " +
            "-moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; " +
            "-moz-background-inline-policy: -moz-initial;\">" + Environment.NewLine +
            "                    <p class=\"MsoNormal\"" + Environment.NewLine +
            "                        style=\"margin: 0in 0in 12pt; text-align: center; font-size: 11pt; " +
            "font-family: Calibri,sans-serif;\"" + Environment.NewLine +
            "                        align=\"center\"><b><span style=\"color: rgb(245, 209, 53);\">Requirements</span>" +
            "</b></p>" + Environment.NewLine +
            "                </td>" + Environment.NewLine +
            "            </tr>" + Environment.NewLine +
            "        </thead>" + Environment.NewLine +
            "        <tbody>" + Environment.NewLine +
            "            {3}" + Environment.NewLine +
            "        </tbody>" + Environment.NewLine +
            "    </table>" + Environment.NewLine +
            "    <span style=\"font-family: Calibri,sans-serif;\"><span style=\"font-weight: bold;\"></span>" +
            "</span><span" + Environment.NewLine +
            "        style=\"color: rgb(34, 34, 34); font-family: Arial,Helvetica,sans-serif; font-size: small; " +
            "font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; " +
            "text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; " +
            "background-color: rgb(255, 255, 255); display: inline ! important; float: none;\"><br>" +
            Environment.NewLine +
            "        For more information please see the<span>&nbsp;</span></span><a" + Environment.NewLine +
            "        href=\"https://osgwiki.com/wiki/CSD_Ops_-_Tier_1_Request\" target=\"_blank\"" +
            Environment.NewLine +
            "        style=\"color: rgb(17, 85, 204); font-family: Arial,Helvetica,sans-serif; font-size: small; " +
            "font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; " +
            "text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; " +
            "background-color: rgb(255, 255, 255);\">Tier" + Environment.NewLine +
            "        1 Request page</a><span" + Environment.NewLine +
            "        style=\"color: rgb(80, 0, 80); font-family: Arial,Helvetica,sans-serif; font-size: small; " +
            "font-style: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; " +
            "text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; background-color: rgb(255, 255, 255); " +
            "display: inline ! important; float: none;\">.</span><br>" + Environment.NewLine +
            "    <p" + Environment.NewLine +
            "        style=\"color: rgb(34, 34, 34); font-style: normal; font-weight: 400; letter-spacing: normal; " +
            "orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; " +
            "word-spacing: 0px; background-color: rgb(255, 255, 255); margin-right: 0in; margin-left: 0in; font-size: 11pt; " +
            "font-family: Calibri,sans-serif;\">" + Environment.NewLine +
            "        Thanks!</p>" + Environment.NewLine +
            "</body>" + Environment.NewLine +
            "</html>";

        /// <summary>
        ///     The email table row format
        /// </summary>
        public static readonly string EMAIL_TABLE_ROW_FORMAT =
            "<tr>" + Environment.NewLine +
            "    <td" + Environment.NewLine +
            "        style=\"border-style: none solid solid; border-color: -moz-use-text-color rgb(173, 186, 220) " +
            "rgb(173, 186, 220); border-width: medium 1pt 1pt; margin: 0px; padding: 6pt; font-family: Roboto,RobotoDraft," +
            "Helvetica,Arial,sans-serif;\">" + Environment.NewLine +
            "        <p class=\"MsoNormal\"" + Environment.NewLine +
            "            style=\"margin: 0in 0in 12pt; font-size: 11pt; font-family: Calibri,sans-serif;\"" +
            ">{0}</p>" + Environment.NewLine +
            "    </td>" + Environment.NewLine +
            "    <td" + Environment.NewLine +
            "        style=\"border-style: none solid solid; border-color: -moz-use-text-color rgb(173, 186, 220) " +
            "rgb(173, 186, 220); border-width: medium 1pt 1pt; margin: 0px; padding: 6pt; font-family: Roboto,RobotoDraft," +
            "Helvetica,Arial,sans-serif;\">" + Environment.NewLine +
            "        <p class=\"MsoNormal\"" + Environment.NewLine +
            "            style=\"margin: 0in 0in 12pt; font-size: 11pt; font-family: Calibri,sans-serif;\"" +
            ">{1}</p>" + Environment.NewLine +
            "    </td>" + Environment.NewLine +
            "    <td" + Environment.NewLine +
            "        style=\"border-style: none solid solid none; border-color: -moz-use-text-color rgb(173, 186, 220) " +
            "rgb(173, 186, 220) -moz-use-text-color; border-width: medium 1pt 1pt medium; margin: 0px; padding: 6pt; " +
            "font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif;\">" + Environment.NewLine +
            "        <p class=\"MsoNormal\"" + Environment.NewLine +
            "            style=\"margin: 0in 0in 12pt; font-size: 11pt; font-family: Calibri,sans-serif;\"" +
            ">{2}</p>" + Environment.NewLine +
            "    </td>" + Environment.NewLine +
            "    <td" + Environment.NewLine +
            "        style=\"border-style: none solid solid none; border-color: -moz-use-text-color rgb(173, 186, 220) " +
            "rgb(173, 186, 220) -moz-use-text-color; border-width: medium 1pt 1pt medium; margin: 0px; padding: 6pt; " +
            "font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif;\">" + Environment.NewLine +
            "        <p class=\"MsoNormal\"" + Environment.NewLine +
            "            style=\"margin: 0in 0in 12pt; font-size: 11pt; font-family: Calibri,sans-serif;\"" +
            ">{3}<br>" + Environment.NewLine +
            "        </p>" + Environment.NewLine +
            "    </td>" + Environment.NewLine +
            "</tr>";
    }
}
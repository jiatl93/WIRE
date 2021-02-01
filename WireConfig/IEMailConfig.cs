// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-29-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="IEMailConfig.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Interface IEMailConfig
    ///     Implements the <see cref="WireConfig.IConfigBase" />
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    /// </summary>
    /// <seealso cref="WireConfig.IConfigBase" />
    /// <seealso cref="WireCommon.IWireCommunicator" />
    public interface IEMailConfig : IConfigBase, IWireCommunicator
    {
        /// <summary>
        ///     Gets or sets the email host.
        /// </summary>
        /// <value>The email host.</value>
        string Host { get; set; }

        /// <summary>
        ///     Gets or sets the port for the email host.
        /// </summary>
        /// <value>The host port.</value>
        int Port { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="EMailConfig" /> uses SSL.
        /// </summary>
        /// <value><c>true</c> if SSL; otherwise, <c>false</c>.</value>
        bool Ssl { get; set; }

        /// <summary>
        ///     Gets or sets the email address that emails will be sent with.
        /// </summary>
        /// <value>From email address.</value>
        string FromEmail { get; set; }

        /// <summary>
        ///     Gets or sets the user name for the email server.
        /// </summary>
        /// <value>The user name for the email server.</value>
        string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password to use with the email server.
        /// </summary>
        /// <value>The password for the email server.</value>
        string Password { get; set; }

        /// <summary>
        ///     Email recipients keyed by user name.
        /// </summary>
        /// <value>The recipients list.</value>
        Dictionary<string, string> Recipients { get; }
    }
}
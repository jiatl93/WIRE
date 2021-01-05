// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-29-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="IControllerConfig.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Interface IControllerConfig
    ///     Implements the <see cref="WireConfig.IConfigBase" />
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    /// </summary>
    /// <seealso cref="WireConfig.IConfigBase" />
    /// <seealso cref="WireCommon.IWireCommunicator" />
    public interface IControllerConfig : IConfigBase, IWireCommunicator
    {
        /// <summary>
        ///     Gets or sets the interval in seconds at which the VSO data is polled for changes.
        /// </summary>
        /// <value>The polling interval.</value>
        int PollingInterval { get; set; }

        /// <summary>
        ///     Gets or sets the interval in seconds at which compliance reports are sent to the
        ///     report email.
        /// </summary>
        /// <value>The reporting interval.</value>
        int ReportingInterval { get; set; }

        /// <summary>
        ///     Gets or sets the email address to which compliance reports are sent.
        /// </summary>
        /// <value>The report email address.</value>
        string ReportEmail { get; set; }

        /// <summary>
        ///     Gets or sets the report folder.
        /// </summary>
        /// <value>The report folder.</value>
        string ReportFolder { get; set; }

        /// <summary>
        ///     Gets or sets the log folder.
        /// </summary>
        /// <value>The log folder.</value>
        string LogFolder { get; set; }
    }
}
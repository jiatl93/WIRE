// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-29-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="IVSOConfig.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Interface IVSOConfig
    ///     Implements the <see cref="WireConfig.IConfigBase" />
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    /// </summary>
    /// <seealso cref="WireConfig.IConfigBase" />
    /// <seealso cref="WireCommon.IWireCommunicator" />
    public interface IVSOConfig : IConfigBase, IWireCommunicator
    {
        /// <summary>
        ///     Gets or sets the base URI for the VSO instance being monitored.
        /// </summary>
        /// <value>The base URI.</value>
        string BaseUri { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [prompt for login].
        /// </summary>
        /// <value><c>true</c> if [prompt for login]; otherwise, <c>false</c>.</value>
        bool PromptForLogin { get; set; }

        /// <summary>
        ///     Gets or sets the personal authentication token used to authenticate to the VSO server.
        /// </summary>
        /// <value>The personal authentication token.</value>
        string Token { get; set; }

        /// <summary>
        ///     Gets the dictionary configuration items stored in this instance.
        /// </summary>
        /// <value>The configuration items.</value>
        Dictionary<string, TaskItemConfig> ConfigItems { get; }
    }
}
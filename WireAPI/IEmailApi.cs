// ***********************************************************************
// Assembly         : WireAPI
// Author           : jiatli93
// Created          : 11-29-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-24-2020
// ***********************************************************************
// <copyright file="IEmailApi.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using WireCommon;

namespace WireAPI
{
    /// <summary>
    ///     Interface IEmailApi
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    ///     Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="WireCommon.IWireCommunicator" />
    /// <seealso cref="System.IDisposable" />
    public interface IEmailApi : IWireCommunicator, IDisposable
    {
        /// <summary>
        ///     Sends the reminder email to the specified party.
        /// </summary>
        /// <param name="recipientName">Name of the recipient.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        void SendEmail(string recipientName, string emailAddress, string subject, string body, bool isHtml = true);
    }
}
// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-29-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="IConfigFile.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Interface IConfigFile
    ///     Implements the <see cref="WireCommon.IWireCommunicator" />
    /// </summary>
    /// <seealso cref="WireCommon.IWireCommunicator" />
    public interface IConfigFile : IWireCommunicator
    {
        /// <summary>
        ///     Gets or sets the name of the configuration file.
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; set; }

        /// <summary>
        ///     Gets the configuration object which contains all the others.
        /// </summary>
        /// <value>The configuration.</value>
        Configuration Configuration { get; }

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        void Load();

        /// <summary>
        ///     Loads configuration from the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Load(string fileName);

        /// <summary>
        ///     Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        ///     Saves the configuration using the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Save(string fileName);
    }
}
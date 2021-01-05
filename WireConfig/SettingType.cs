// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-21-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="SettingType.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WireConfig
{
    /// <summary>
    ///     Enumeration for differentiating single settings from ones that have multiple
    ///     values in a dictionary.
    /// </summary>
    public enum SettingType
    {
        /// <summary>
        ///     The single
        /// </summary>
        Single,

        /// <summary>
        ///     The dictionary
        /// </summary>
        Dictionary
    }
}
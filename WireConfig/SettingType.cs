// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-21-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="SettingType.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
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
        ///     The setting is a single type
        /// </summary>
        Single,

        /// <summary>
        ///     The setting is a dictionary type
        /// </summary>
        Dictionary
    }
}
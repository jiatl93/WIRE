// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 12-06-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="IConfigBase.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WireConfig
{
    /// <summary>
    ///     Interface IConfigBase
    /// </summary>
    public interface IConfigBase
    {
        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        void Init();

        /// <summary>
        ///     Determines whether the specified setting exists.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns><c>true</c> if the specified setting exists; otherwise, <c>false</c>.</returns>
        bool ContainsSetting(string settingName);

        /// <summary>
        ///     Gets the display name for the specified setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>The display name.</returns>
        string GetDisplayName(string settingName);

        /// <summary>
        ///     Gets the value specified by the setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>The value of the setting.</returns>
        dynamic GetValue(string settingName);

        /// <summary>
        ///     Gets the value for the specified setting for the specified dictionary key.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="dictionaryKey">The dictionary key within the setting.</param>
        /// <returns>The value of the setting's dictionary item.</returns>
        dynamic GetValue(string settingName, string dictionaryKey);

        /// <summary>
        ///     Sets the value specified by the setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">The value to set.</param>
        void SetValue(string settingName, dynamic value);

        /// <summary>
        ///     Sets the value for the specified setting for the specified dictionary key.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="dictionaryKey">The dictionary key within the setting.</param>
        /// <param name="value">The value of the dictionary item to set.</param>
        void SetValue(string settingName, string dictionaryKey, dynamic value);

        /// <summary>
        ///     Deletes the value for the specified setting for the specified dictionary key.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="dictionaryKey">The dictionary key.</param>
        void DeleteValue(string settingName, string dictionaryKey);

        /// <summary>
        ///     Clears the specified value. If it's a dictionary type, the DictionaryClearer
        ///     is called.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        void ClearValue(string settingName);

        /// <summary>
        ///     Gets help for the specified setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>System.String.</returns>
        string GetHelp(string settingName);
    }
}
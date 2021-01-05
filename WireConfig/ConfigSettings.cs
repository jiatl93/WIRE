// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-21-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="ConfigSettings.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Newtonsoft.Json;

namespace WireConfig
{
    /// <summary>
    ///     Class that carries information about a particular configuration item, including
    ///     name (key), display name, type, setter, getter, and dictionary getter (for the
    ///     dictionary type).
    /// </summary>
    public class ConfigSettings
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
        ///     Initializes a new instance of the <see cref="ConfigSettings" /> class.
        /// </summary>
        /// <param name="key">The key name of the setting.</param>
        /// <param name="displayName">The display name of the setting.</param>
        /// <param name="helpText">The help text.</param>
        /// <param name="type">The type of the setting.</param>
        /// <param name="setter">The setter function.</param>
        /// <param name="getter">The getter function.</param>
        /// <param name="dictionaryGetter">The dictionary getter function.</param>
        /// <param name="dictionarySetter">The dictionary setter function.</param>
        /// <param name="dictionaryItemDeleter">The dictionary item deleter.</param>
        /// <param name="dictionaryClearer">The dictionary clearer.</param>
        public ConfigSettings(string key, string displayName, string helpText, SettingType type,
            Action<dynamic> setter, Func<dynamic> getter, Func<dynamic, string> dictionaryGetter = null,
            Action<string, dynamic> dictionarySetter = null, Action<string> dictionaryItemDeleter = null,
            Action<string> dictionaryClearer = null)
        {
            Key = key;
            DisplayName = displayName;
            HelpText = helpText;
            Type = type;
            Setter = setter;
            Getter = getter;
            DictionaryGetter = dictionaryGetter;
            DictionarySetter = dictionarySetter;
            DictionaryItemDeleter = dictionaryItemDeleter;
            DictionaryClearer = dictionaryClearer;
        }

        /// <summary>
        ///     Gets or sets the help text.
        /// </summary>
        /// <value>The help text.</value>
        [JsonIgnore]
        public string HelpText { get; set; }

        /// <summary>
        ///     Gets or sets the key for finding the setting.
        /// </summary>
        /// <value>The key (name) of the setting.</value>
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets the display name of the setting.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the type of the setting.
        /// </summary>
        /// <value>The type of the setting.</value>
        public SettingType Type { get; set; }

        /// <summary>
        ///     Gets or sets the setter function for setting the value of the setting.
        /// </summary>
        /// <value>The setter function.</value>
        [JsonIgnore]
        public Action<dynamic> Setter { get; set; }

        /// <summary>
        ///     Gets or sets the getter function for the value of the settings.
        /// </summary>
        /// <value>The getter function.</value>
        [JsonIgnore]
        public Func<dynamic> Getter { get; set; }

        /// <summary>
        ///     Gets or sets the getter function for dictionary setting types.
        /// </summary>
        /// <value>The dictionary getter function.</value>
        [JsonIgnore]
        public Func<dynamic, string> DictionaryGetter { get; set; }

        /// <summary>
        ///     Gets or sets the setter function for dictionary setting types.
        /// </summary>
        /// <value>The dictionary setter function.</value>
        [JsonIgnore]
        public Action<string, dynamic> DictionarySetter { get; set; }

        /// <summary>
        ///     Gets or sets the deleter function for dictionary settings types.
        /// </summary>
        /// <value>The dictionary item deleter.</value>
        [JsonIgnore]
        public Action<string> DictionaryItemDeleter { get; set; }

        /// <summary>
        ///     Gets or sets the dictionary clearer function.
        /// </summary>
        /// <value>The dictionary clearer.</value>
        [JsonIgnore]
        public Action<string> DictionaryClearer { get; set; }
    }
}
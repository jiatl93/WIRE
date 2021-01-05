// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-11-2020
// ***********************************************************************
// <copyright file="ConfigBase.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Newtonsoft.Json;

namespace WireConfig
{
    /// <summary>
    ///     Base class for configuration objects. Contains common code.
    /// </summary>
    public abstract class ConfigBase : IConfigBase
    {
        /// <summary>
        ///     Dictionary of configuration items as ConfigSettings objects.
        /// </summary>
        [JsonIgnore] public Dictionary<string, ConfigSettings> Settings;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigBase" /> class.
        /// </summary>
        public ConfigBase()
        {
            Settings = new Dictionary<string, ConfigSettings>();
            Init();
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        public abstract void Init();

        /// <summary>
        ///     Determines whether the specified setting exists.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns><c>true</c> if the specified setting exists; otherwise, <c>false</c>.</returns>
        public virtual bool ContainsSetting(string settingName)
        {
            return Settings.ContainsKey(settingName.ToLower());
        }

        /// <summary>
        ///     Gets the display name for the specified setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>The display name.</returns>
        public virtual string GetDisplayName(string settingName)
        {
            if (ContainsSetting(settingName))
                return Settings[settingName].DisplayName;

            return string.Empty;
        }

        /// <summary>
        ///     Gets the value specified by the setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>The value of the setting.</returns>
        public virtual dynamic GetValue(string settingName)
        {
            if (ContainsSetting(settingName))
            {
                var setting = Settings[settingName.ToLower()];
                return Settings[settingName.ToLower()].Getter.Invoke();
            }

            return null;
        }

        /// <summary>
        ///     Gets the value for the specified setting for the specified dictionary key.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="dictionaryKey">The dictionary key within the setting.</param>
        /// <returns>The value of the setting's dictionary item.</returns>
        public virtual dynamic GetValue(string settingName, string dictionaryKey)
        {
            if (ContainsSetting(settingName))
            {
                var setting = Settings[settingName.ToLower()];
                if (setting.Type == SettingType.Dictionary)
                    return Settings[settingName.ToLower()].DictionaryGetter.Invoke(dictionaryKey);
            }

            return null;
        }

        /// <summary>
        ///     Sets the value specified by the setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">The value to set.</param>
        public virtual void SetValue(string settingName, dynamic value)
        {
            if (ContainsSetting(settingName)) Settings[settingName.ToLower()].Setter.Invoke(value);
        }

        /// <summary>
        ///     Sets the value for the specified setting for the specified dictionary key.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="dictionaryKey">The dictionary key within the setting.</param>
        /// <param name="value">The value of the dictionary item to set.</param>
        public virtual void SetValue(string settingName, string dictionaryKey, dynamic value)
        {
            if (ContainsSetting(settingName))
            {
                var setting = Settings[settingName.ToLower()];
                if (setting.Type == SettingType.Dictionary)
                    Settings[settingName.ToLower()].DictionarySetter.Invoke(dictionaryKey, value);
            }
        }

        /// <summary>
        ///     Deletes the value for the specified setting for the specified dictionary key.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="dictionaryKey">The dictionary key.</param>
        public virtual void DeleteValue(string settingName, string dictionaryKey)
        {
            if (ContainsSetting(settingName)) Settings[settingName.ToLower()].DictionaryItemDeleter(dictionaryKey);
        }

        /// <summary>
        ///     Clears the specified value. If it's a dictionary type, the DictionaryClearer
        ///     is called.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        public virtual void ClearValue(string settingName)
        {
            if (ContainsSetting(settingName))
            {
                var setting = Settings[settingName.ToLower()];
                if (setting.Type == SettingType.Dictionary)
                    Settings[settingName.ToLower()].DictionaryClearer.Invoke(settingName);
                else
                    Settings[settingName.ToLower()] = null;
            }
        }

        /// <summary>
        ///     Gets help for the specified setting name.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>System.String.</returns>
        public virtual string GetHelp(string settingName)
        {
            if (ContainsSetting(settingName))
            {
                var setting = Settings[settingName.ToLower()];
                return Settings[settingName.ToLower()].HelpText;
            }

            return null;
        }
    }
}
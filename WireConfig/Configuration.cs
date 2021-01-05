// ***********************************************************************
// Assembly         : WireConfig
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-23-2020
// ***********************************************************************
// <copyright file="Configuration.cs" company="Red Clay">
//     ${AuthorCopyright}
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Newtonsoft.Json;
using WireCommon;

namespace WireConfig
{
    /// <summary>
    ///     Class which encapsulates instances of configuration objects so that
    ///     they can be serialized or deserialized in one stream.
    /// </summary>
    public class Configuration : IWireCommunicator
    {
        /// <summary>
        ///     Backing field for the error handler action.
        /// </summary>
        private Action<Exception> _handleError;

        /// <summary>
        ///     The handle log
        /// </summary>
        private Action<LogEntryType, string> _handleLog;

        /// <summary>
        ///     Backing field for the message handler action.
        /// </summary>
        private Action<string> _handleMessage;

        /// <summary>
        ///     The handle report
        /// </summary>
        private Action<string> _handleReport;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration() : this(new EMailConfig(), new VSOConfig(), new ControllerConfig())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        /// <param name="emailConfig">The email configuration.</param>
        /// <param name="vsoConfig">The vso configuration.</param>
        /// <param name="controllerConfig">The controller configuration.</param>
        public Configuration(IEMailConfig emailConfig, IVSOConfig vsoConfig, IControllerConfig controllerConfig)
        {
            EMailConfig = emailConfig;
            VsoConfig = vsoConfig;
            ControllerConfig = controllerConfig;
        }

        /// <summary>
        ///     Gets the e-mail configuration object.
        /// </summary>
        /// <value>The e-mail configuration object.</value>
        public IEMailConfig EMailConfig { get; }

        /// <summary>
        ///     Gets the VSO configuration object.
        /// </summary>
        /// <value>The VSO configuration.</value>
        public IVSOConfig VsoConfig { get; }

        /// <summary>
        ///     Gets the controller configuration.
        /// </summary>
        /// <value>The controller configuration.</value>
        public IControllerConfig ControllerConfig { get; }

        /// <summary>
        ///     Gets or sets the error handler.
        /// </summary>
        /// <value>The error handler.</value>
        [JsonIgnore]
        public Action<Exception> HandleError
        {
            get => _handleError;
            set
            {
                _handleError = value;
                EMailConfig.HandleError += _handleError;
                VsoConfig.HandleError += _handleError;
                ControllerConfig.HandleError += _handleError;
            }
        }

        /// <summary>
        ///     Gets or sets the message handler.
        /// </summary>
        /// <value>The handle message.</value>
        [JsonIgnore]
        public Action<string> HandleMessage
        {
            get => _handleMessage;
            set
            {
                _handleMessage = value;
                EMailConfig.HandleMessage += _handleMessage;
                VsoConfig.HandleMessage += _handleMessage;
                ControllerConfig.HandleMessage += _handleMessage;
            }
        }

        /// <summary>
        ///     Gets or sets the handler for logging operations.
        /// </summary>
        /// <value>The handle log.</value>
        public Action<LogEntryType, string> HandleLog
        {
            get => _handleLog;
            set
            {
                _handleLog = value;
                EMailConfig.HandleLog += _handleLog;
                VsoConfig.HandleLog += _handleLog;
                ControllerConfig.HandleLog += _handleLog;
            }
        }

        /// <summary>
        ///     Gets or sets the report operations handler.
        /// </summary>
        /// <value>The handle report.</value>
        public Action<string> HandleReport
        {
            get => _handleReport;
            set
            {
                _handleReport = value;
                EMailConfig.HandleReport += _handleReport;
                VsoConfig.HandleReport += _handleReport;
                ControllerConfig.HandleReport += _handleReport;
            }
        }

        /// <summary>
        ///     Calls the error handler with the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            if (HandleError != null)
                HandleError.Invoke(exception);
            else
                throw exception;
        }

        /// <summary>
        ///     Calls the message handler with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Message(string message)
        {
            HandleMessage?.Invoke(message);
        }

        /// <summary>
        ///     Logs the specified message.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Log(LogEntryType entryType, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Reports the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Report(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the display name of the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>System.String.</returns>
        public string GetDisplayName(string setting)
        {
            if (EMailConfig.ContainsSetting(setting))
                return EMailConfig.GetDisplayName(setting);

            if (VsoConfig.ContainsSetting(setting))
                return VsoConfig.GetDisplayName(setting);

            if (ControllerConfig.ContainsSetting(setting))
                return ControllerConfig.GetDisplayName(setting);

            Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT, setting)));
            return null;
        }

        /// <summary>
        ///     Gets the value of the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>dynamic.</returns>
        public dynamic GetValue(string setting)
        {
            if (EMailConfig.ContainsSetting(setting))
                return EMailConfig.GetValue(setting);

            if (VsoConfig.ContainsSetting(setting))
                return VsoConfig.GetValue(setting);

            if (ControllerConfig.ContainsSetting(setting))
                return ControllerConfig.GetValue(setting);

            Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT, setting)));
            return null;
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="key">The key.</param>
        /// <returns>dynamic.</returns>
        public dynamic GetValue(string setting, string key)
        {
            if (EMailConfig.ContainsSetting(setting))
                return EMailConfig.GetValue(setting, key);

            if (VsoConfig.ContainsSetting(setting))
                return VsoConfig.GetValue(setting, key);

            if (ControllerConfig.ContainsSetting(setting))
                return ControllerConfig.GetValue(setting, key);

            Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT, setting)));
            return null;
        }

        /// <summary>
        ///     Sets the value specfied by the setting name.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void SetValue(string setting)
        {
            try
            {
                var settingValuePair = setting.Split('=');
                if (EMailConfig.ContainsSetting(settingValuePair[Constants.KEY]))
                {
                    EMailConfig.SetValue(settingValuePair[Constants.KEY], settingValuePair[Constants.VALUE]);
                    Message(string.Format(Constants.PROPERTY_SET_CONFIRMATION_FMT,
                        settingValuePair[Constants.KEY], settingValuePair[Constants.VALUE]));
                }
                else if (VsoConfig.ContainsSetting(settingValuePair[Constants.KEY]))
                {
                    VsoConfig.SetValue(settingValuePair[Constants.KEY], settingValuePair[Constants.VALUE]);
                    Message(string.Format(Constants.PROPERTY_SET_CONFIRMATION_FMT,
                        settingValuePair[Constants.KEY], settingValuePair[Constants.VALUE]));
                }
                else if (ControllerConfig.ContainsSetting(settingValuePair[Constants.KEY]))
                {
                    ControllerConfig.SetValue(settingValuePair[Constants.KEY], settingValuePair[Constants.VALUE]);
                    Message(string.Format(Constants.PROPERTY_SET_CONFIRMATION_FMT,
                        settingValuePair[Constants.KEY], settingValuePair[Constants.VALUE]));
                }
                else
                {
                    Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT,
                        settingValuePair[Constants.KEY])));
                }
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="setting">The setting.</param>
        public void SetValue(string category, string setting)
        {
            try
            {
                var settingValuePair = setting.Split('=');
                if (EMailConfig.ContainsSetting(category))
                {
                    EMailConfig.SetValue(category, settingValuePair[0], settingValuePair[1]);
                    Message(string.Format(Constants.DICTIONARY_PROPERTY_SET_CONFIRMATION_FMT, category,
                        settingValuePair[0], settingValuePair[1]));
                }
                else if (VsoConfig.ContainsSetting(category))
                {
                    VsoConfig.SetValue(category, settingValuePair[0], settingValuePair[1]);
                    Message(string.Format(Constants.DICTIONARY_PROPERTY_SET_CONFIRMATION_FMT, category,
                        settingValuePair[0], settingValuePair[1]));
                }
                else if (ControllerConfig.ContainsSetting(category))
                {
                    ControllerConfig.SetValue(category, settingValuePair[0], settingValuePair[1]);
                    Message(string.Format(Constants.DICTIONARY_PROPERTY_SET_CONFIRMATION_FMT, category,
                        settingValuePair[0], settingValuePair[1]));
                }
                else
                {
                    Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT, category)));
                }
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        /// <summary>
        ///     Deletes the specified value in a dictionary-type config item.
        /// </summary>
        /// <param name="category">The category (name of dictionary).</param>
        /// <param name="setting">The setting.</param>
        public void DeleteValue(string category, string setting)
        {
            if (EMailConfig.ContainsSetting(category))
            {
                EMailConfig.DeleteValue(category, setting);
                Message(string.Format(Constants.PROPERTY_DELETE_CONFIRMATION_FMT, setting));
            }
            else if (VsoConfig.ContainsSetting(category))
            {
                VsoConfig.DeleteValue(category, setting);
                Message(string.Format(Constants.PROPERTY_DELETE_CONFIRMATION_FMT, setting));
            }
            else if (ControllerConfig.ContainsSetting(category))
            {
                ControllerConfig.DeleteValue(category, setting);
                Message(string.Format(Constants.PROPERTY_DELETE_CONFIRMATION_FMT, setting));
            }
            else
            {
                Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT, category)));
            }
        }

        /// <summary>
        ///     Clears the values in a dictionary setting type.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ClearValue(string setting)
        {
            if (EMailConfig.ContainsSetting(setting))
            {
                EMailConfig.ClearValue(setting);
                Message(string.Format(Constants.PROPERTY_DELETE_CONFIRMATION_FMT, setting));
            }
            else if (VsoConfig.ContainsSetting(setting))
            {
                VsoConfig.ClearValue(setting);
                Message(string.Format(Constants.PROPERTY_DELETE_CONFIRMATION_FMT, setting));
            }
            else if (ControllerConfig.ContainsSetting(setting))
            {
                ControllerConfig.ClearValue(setting);
                Message(string.Format(Constants.PROPERTY_DELETE_CONFIRMATION_FMT, setting));
            }
            else
            {
                Error(new Exception(string.Format(Constants.PROPERTY_ERROR_FMT, setting)));
            }
        }

        /// <summary>
        ///     Gets the help.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>System.String.</returns>
        public string GetHelp(string setting)
        {
            if (EMailConfig.ContainsSetting(setting))
                return EMailConfig.GetHelp(setting);

            if (VsoConfig.ContainsSetting(setting))
                return VsoConfig.GetHelp(setting);

            if (ControllerConfig.ContainsSetting(setting))
                return ControllerConfig.GetHelp(setting);

            return string.Empty;
        }
    }
}
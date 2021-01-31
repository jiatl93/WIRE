// ***********************************************************************
// Assembly         : WireConfigTests
// Author           : jiatli93
// Created          : 12-05-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-10-2020
// ***********************************************************************
// <copyright file="ConfigFileTestCommon.cs" company="Red Clay">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WireCommon;
using WireConfig;

namespace WireConfigTests
{
    /// <summary>
    /// Class ConfigFileTestCommon.
    /// </summary>
    public static class ConfigFileTestCommon
    {
        static ConfigFileTestCommon()
        {
            AesProvider.EncryptionKey = Constants.ENCRYPTION_KEY;
        }

        /// <summary>
        /// The configuration test file
        /// </summary>
        public static string CONFIG_TEST_FILE =
            "{" + Environment.NewLine +
            "  \"EMailConfig\": {" + Environment.NewLine +
            "    \"Host\": \"" + TEST_HOST + "\"," + Environment.NewLine +
            "    \"Port\": " + TEST_PORT + "," + Environment.NewLine +
            "    \"Ssl\": true," + Environment.NewLine +
            "    \"FromEmail\": \"" + TEST_FROM_EMAIL + "\"," + Environment.NewLine +
            "    \"UserName\": \"" + AesProvider.Encrypt(TEST_USER_NAME) + "\"," + Environment.NewLine +
            "    \"Password\": \"" + AesProvider.Encrypt(TEST_PASSWORD) + "\"," + Environment.NewLine +
            "    \"Recipients\": {" + Environment.NewLine +
            "      \"" + TEST_RECIPIENT_1_NAME + "\": \"" + TEST_RECIPIENT_1_EMAIL + "\"," + Environment.NewLine +
            "      \"" + TEST_RECIPIENT_2_NAME + "\": \"" + TEST_RECIPIENT_2_EMAIL + "\"" + Environment.NewLine +
            "    }" + Environment.NewLine +
            "  }," + Environment.NewLine +
            "  \"VsoConfig\": {" + Environment.NewLine +
            "    \"BaseUri\": \"" + TEST_BASE_URI + "\"," + Environment.NewLine +
            "    \"PromptForLogin\": true," + Environment.NewLine +
            "    \"Token\": \"" + AesProvider.Encrypt(TEST_TOKEN) + "\"," + Environment.NewLine +
            "    \"ConfigItems\": {" + Environment.NewLine +
            "      \"" + TEST_TASK_ITEM_1_FIELD_NAME + "\": {" + Environment.NewLine +
            "        \"FieldPrefix\": \"" + TEST_TASK_ITEM_1_FIELD_PREFIX + "\"," + Environment.NewLine +
            "        \"AreaPath\": \"" + TEST_TASK_ITEM_1_AREA_PATH + "\"," + Environment.NewLine +
            "        \"FieldName\": \"" + TEST_TASK_ITEM_1_FIELD_NAME + "\"," + Environment.NewLine +
            "        \"Description\": \"" + TEST_TASK_ITEM_1_DESCRIPTION + "\"," + Environment.NewLine +
            "        \"ValidationRegex\": \"" + TEST_TASK_ITEM_1_REGEX + "\"," + Environment.NewLine +
            "        \"HelpMessage\": \"" + TEST_TASK_ITEM_1_HELP_TEXT + "\"," + Environment.NewLine +
            "        \"GracePeriodInHours\": " + TEST_TASK_ITEM_1_GRACE_PERIOD + "" + Environment.NewLine +
            "      }," + Environment.NewLine +
            "      \"" + TEST_TASK_ITEM_2_FIELD_NAME + "\": {" + Environment.NewLine +
            "        \"FieldPrefix\": \"" + TEST_TASK_ITEM_2_FIELD_PREFIX + "\"," + Environment.NewLine +
            "        \"AreaPath\": \"" + TEST_TASK_ITEM_2_AREA_PATH + "\"," + Environment.NewLine +
            "        \"FieldName\": \"" + TEST_TASK_ITEM_2_FIELD_NAME + "\"," + Environment.NewLine +
            "        \"Description\": \"" + TEST_TASK_ITEM_2_DESCRIPTION + "\"," + Environment.NewLine +
            "        \"ValidationRegex\": \"" + TEST_TASK_ITEM_2_REGEX + "\"," + Environment.NewLine +
            "        \"HelpMessage\": \"" + TEST_TASK_ITEM_2_HELP_TEXT + "\"," + Environment.NewLine +
            "        \"GracePeriodInHours\": " + TEST_TASK_ITEM_2_GRACE_PERIOD + "" + Environment.NewLine +
            "      }" + Environment.NewLine +
            "    }" + Environment.NewLine +
            "  }," + Environment.NewLine +
            "  \"ControllerConfig\": {" + Environment.NewLine +
            "    \"PollingInterval\": " + TEST_POLLING_INTERVAL + "," + Environment.NewLine +
            "    \"ReportingInterval\": " + TEST_REPORTING_INTERVAL + "," + Environment.NewLine +
            "    \"ReportEmail\": \"" + TEST_REPORT_EMAIL + "\"," + Environment.NewLine +
            "    \"LogFolder\": \"" + TEST_LOG_FOLDER_ESCAPED + "\"," + Environment.NewLine +
            "    \"ReportFolder\": \"" + TEST_REPORT_FOLDER_ESCAPED + "\"" + Environment.NewLine +
            "  }" + Environment.NewLine +
            "}";

        /// <summary>
        /// The supplied file name
        /// </summary>
        public const string SUPPLIED_FILE_NAME = "test_config_name.json";

        /// <summary>
        /// The test host
        /// </summary>
        public const string TEST_HOST = "smtp.constoso.com";
        /// <summary>
        /// The test port
        /// </summary>
        public const int TEST_PORT = 31;
        /// <summary>
        /// The test from email
        /// </summary>
        public const string TEST_FROM_EMAIL = "test.user@contoso.com";
        /// <summary>
        /// The test user name
        /// </summary>
        public const string TEST_USER_NAME = "testuser";
        /// <summary>
        /// The test password
        /// </summary>
        public const string TEST_PASSWORD = "p@ssw0rd";
        /// <summary>
        /// The test recipient 1 name
        /// </summary>
        public const string TEST_RECIPIENT_1_NAME = "Edward Cullen";
        /// <summary>
        /// The test recipient 1 email
        /// </summary>
        public const string TEST_RECIPIENT_1_EMAIL = "edward.cullen@constoso.com";
        /// <summary>
        /// The test recipient 2 name
        /// </summary>
        public const string TEST_RECIPIENT_2_NAME = "Bella Swan";
        /// <summary>
        /// The test recipient 2 email
        /// </summary>
        public const string TEST_RECIPIENT_2_EMAIL = "bella.swan@constoso.com";

        /// <summary>
        /// The test base URI
        /// </summary>
        public const string TEST_BASE_URI = "https://vso.constoso.com/project";
        /// <summary>
        /// The test token
        /// </summary>
        public const string TEST_TOKEN = "806D5FE592584CFBAD117F8930A331AE";

        /// <summary>
        /// The test task item 1 area path
        /// </summary>
        public const string TEST_TASK_ITEM_1_AREA_PATH = "AreaPath1";

        /// <summary>
        /// The test task item 1 field type
        /// </summary>
        public const string TEST_TASK_ITEM_1_FIELD_PREFIX = "Field1Prefix";

        /// <summary>
        /// The test task item 1 field name
        /// </summary>
        public const string TEST_TASK_ITEM_1_FIELD_NAME = "Field1";

        /// <summary>
        /// The test task item 1 description
        /// </summary>
        public const string TEST_TASK_ITEM_1_DESCRIPTION = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        /// <summary>
        /// The test task item 1 regex
        /// </summary>
        public const string TEST_TASK_ITEM_1_REGEX = @"^([\\d]+)$";
        /// <summary>
        /// The test task item 1 help text
        /// </summary>
        public const string TEST_TASK_ITEM_1_HELP_TEXT = "Aenean dapibus massa a eleifend euismod.";
        /// <summary>
        /// The test task item 1 grace period
        /// </summary>
        public const int TEST_TASK_ITEM_1_GRACE_PERIOD = 41;

        public const string TEST_TASK_ITEM_2_AREA_PATH = "AreaPath2";

        /// <summary>
        /// The test task item 2 field type
        /// </summary>
        public const string TEST_TASK_ITEM_2_FIELD_PREFIX = "Field2Prefix";
        /// <summary>
        /// The test task item 2 field name
        /// </summary>
        public const string TEST_TASK_ITEM_2_FIELD_NAME = "Field2";
        /// <summary>
        /// The test task item 2 description
        /// </summary>
        public const string TEST_TASK_ITEM_2_DESCRIPTION = "Nulla in nunc quis diam viverra tincidunt sed eu massa.";
        /// <summary>
        /// The test task item 2 regex
        /// </summary>
        public const string TEST_TASK_ITEM_2_REGEX = @"^([\\w]+)$";
        /// <summary>
        /// The test task item 2 help text
        /// </summary>
        public const string TEST_TASK_ITEM_2_HELP_TEXT = "Integer nec imperdiet erat. Nulla egestas dictum elit at ultrices.";
        /// <summary>
        /// The test task item 2 grace period
        /// </summary>
        public const int TEST_TASK_ITEM_2_GRACE_PERIOD = 59;
        /// <summary>
        /// The test task item 1 regex non escaped
        /// </summary>
        public const string TEST_TASK_ITEM_1_REGEX_NON_ESCAPED = @"^([\d]+)$";
        /// <summary>
        /// The test task item 2 regex non escaped
        /// </summary>
        public const string TEST_TASK_ITEM_2_REGEX_NON_ESCAPED = @"^([\w]+)$";

        /// <summary>
        /// The test polling interval
        /// </summary>
        public const int TEST_POLLING_INTERVAL = 26;
        /// <summary>
        /// The test reporting interval
        /// </summary>
        public const int TEST_REPORTING_INTERVAL = 53;
        /// <summary>
        /// The test report email
        /// </summary>
        public const string TEST_REPORT_EMAIL = "jia.li@contoso.com";

        public const string TEST_REPORT_FOLDER = @"c:\test\WIRE-Reports";

        public const string TEST_LOG_FOLDER = @"c:\test\WIRE-Logs";

        public const string TEST_REPORT_FOLDER_ESCAPED = @"c:\\test\\WIRE-Reports";

        public const string TEST_LOG_FOLDER_ESCAPED = @"c:\\test\\WIRE-Logs";

        public const string TEST_MESSAGE = "Test message";

        public const string TEST_LOG = "Test log";

        /// <summary>
        /// Setups the configuration file.
        /// </summary>
        /// <param name="configFileFileName">Name of the configuration file.</param>
        public static void SetupConfigFile(string configFileFileName)
        {
            File.WriteAllText(configFileFileName, CONFIG_TEST_FILE);
        }

        /// <summary>
        /// Setups the configuration object.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void SetupConfigObject(Configuration config)
        {
            SetupEMailConfig(config.EMailConfig);
            SetupVsoConfig(config.VsoConfig);
            SetupControllerConfig(config.ControllerConfig);
        }

        /// <summary>
        /// Setups the controller configuration.
        /// </summary>
        /// <param name="controllerConfig">The controller configuration.</param>
        public static void SetupControllerConfig(IControllerConfig controllerConfig)
        {
            // set up ControllerConfig
            controllerConfig.PollingInterval = TEST_POLLING_INTERVAL;
            controllerConfig.ReportingInterval = TEST_REPORTING_INTERVAL;
            controllerConfig.ReportEmail = TEST_REPORT_EMAIL;
            controllerConfig.ReportFolder = TEST_REPORT_FOLDER;
            controllerConfig.LogFolder = TEST_LOG_FOLDER;
        }

        /// <summary>
        /// Setups the vso configuration.
        /// </summary>
        /// <param name="vsoConfig">The vso configuration.</param>
        private static void SetupVsoConfig(IVSOConfig vsoConfig)
        {
            // set up VSO config
            vsoConfig.BaseUri = TEST_BASE_URI;
            vsoConfig.PromptForLogin = true;
            vsoConfig.Token = TEST_TOKEN;

            // set up the task items
            TaskItemConfig config1 = new TaskItemConfig(
                TEST_TASK_ITEM_1_AREA_PATH,
                TEST_TASK_ITEM_1_FIELD_PREFIX,
                TEST_TASK_ITEM_1_FIELD_NAME,
                TEST_TASK_ITEM_1_DESCRIPTION,
                TEST_TASK_ITEM_1_REGEX_NON_ESCAPED,
                TEST_TASK_ITEM_1_HELP_TEXT,
                TEST_TASK_ITEM_1_GRACE_PERIOD
            );
            vsoConfig.ConfigItems.Add(config1.FieldName, config1);

            TaskItemConfig config2 = new TaskItemConfig(
                TEST_TASK_ITEM_2_AREA_PATH,
                TEST_TASK_ITEM_2_FIELD_PREFIX,
                TEST_TASK_ITEM_2_FIELD_NAME,
                TEST_TASK_ITEM_2_DESCRIPTION,
                TEST_TASK_ITEM_2_REGEX_NON_ESCAPED,
                TEST_TASK_ITEM_2_HELP_TEXT,
                TEST_TASK_ITEM_2_GRACE_PERIOD
            );
            vsoConfig.ConfigItems.Add(config2.FieldName, config2);
        }

        /// <summary>
        /// Setups the e mail configuration.
        /// </summary>
        /// <param name="eMailConfig">The e mail configuration.</param>
        public static void SetupEMailConfig(IEMailConfig eMailConfig)
        {
            // set up the email config
            eMailConfig.Host = TEST_HOST;
            eMailConfig.Port = TEST_PORT;
            eMailConfig.Ssl = true;
            eMailConfig.FromEmail = TEST_FROM_EMAIL;
            eMailConfig.UserName = TEST_USER_NAME;
            eMailConfig.Password = TEST_PASSWORD;

            // set up the recipients
            eMailConfig.Recipients.Add(TEST_RECIPIENT_1_NAME, TEST_RECIPIENT_1_EMAIL);
            eMailConfig.Recipients.Add(TEST_RECIPIENT_2_NAME, TEST_RECIPIENT_2_EMAIL);
        }
    }

}

// ***********************************************************************
// Assembly         : WireConfigTests
// Author           : jiatli93
// Created          : 11-15-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-10-2020
// ***********************************************************************
// <copyright file="ConfigFileTests.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using WireConfig;
using WireCommon;

namespace WireConfigTests
{
    /// <summary>
    /// Unit tests putatively for the ConfigFile class.
    /// </summary>
    [TestClass]
    public class ConfigFileTests
    {
        /// <summary>
        /// The bad file name
        /// </summary>
        private const string BAD_FILE_NAME = "*%$#";
        /// <summary>
        /// The bad json
        /// </summary>
        private const string BAD_JSON = "XXXXXXXXXX";

        /// <summary>
        /// Defines the test method Load_StateUnderTest_ExpectedBehavior_Default_File_Name.
        /// </summary>
        [TestMethod]
        public void Load_StateUnderTest_ExpectedBehavior_Default_File_Name()
        {

            // Arrange
            var configFile = new ConfigFile();
            ConfigFileTestCommon.SetupConfigFile(configFile.FileName);
            // DEBUG-------------------------------------------------------
            // ConfigFileTestCommon.SetupConfigFile(@"h:\temp\expected.json");
            // ConfigFileTestCommon.SetupConfigObject(configFile.Configuration);
            // configFile.Save(@"h:\temp\actual.json");
            // DEBUG-------------------------------------------------------

            // Act
            configFile.Load();

            // Assert
            AssertConfigContents(configFile.Configuration);
        }

        /// <summary>
        /// Defines the test method Load_StateUnderTest_ExpectedBehavior_Supplied_File_Name.
        /// </summary>
        [TestMethod]
        public void Load_StateUnderTest_ExpectedBehavior_Supplied_File_Name()
        {
            // Arrange
            var configFile = new ConfigFile();
            ConfigFileTestCommon.SetupConfigFile(ConfigFileTestCommon.SUPPLIED_FILE_NAME);

            // Act
            configFile.Load(ConfigFileTestCommon.SUPPLIED_FILE_NAME);

            // Assert
            AssertConfigContents(configFile.Configuration);
        }

        /// <summary>
        /// Defines the test method Save_StateUnderTest_ExpectedBehavior_Default_File_Name.
        /// </summary>
        [TestMethod]
        public void Save_StateUnderTest_ExpectedBehavior_Default_File_Name()
        {
            // Arrange
            var configFileWrite = new ConfigFile();
            var configFileRead = new ConfigFile();
            ConfigFileTestCommon.SetupConfigObject(configFileWrite.Configuration);

            // Act
            configFileWrite.Save();
            configFileRead.Load();

            // Assert
            AssertConfigContents(configFileRead.Configuration);
        }

        /// <summary>
        /// Defines the test method Save_StateUnderTest_ExpectedBehavior_Supplied_File_Name.
        /// </summary>
        [TestMethod]
        public void Save_StateUnderTest_ExpectedBehavior_Supplied_File_Name()
        {
            // Arrange
            var configFileWrite = new ConfigFile();
            var configFileRead = new ConfigFile();
            ConfigFileTestCommon.SetupConfigFile(ConfigFileTestCommon.SUPPLIED_FILE_NAME);
            ConfigFileTestCommon.SetupConfigObject(configFileWrite.Configuration);

            // Act
            configFileWrite.Save(ConfigFileTestCommon.SUPPLIED_FILE_NAME);
            configFileRead.Load(ConfigFileTestCommon.SUPPLIED_FILE_NAME);

            // Assert
            AssertConfigContents(configFileRead.Configuration);
        }

        /// <summary>
        /// Defines the test method Load_Error_On_BadFileName.
        /// </summary>
        [TestMethod]
        public void Load_Error_On_BadFileName()
        {
            // Arrange
            var configFile = new ConfigFile();
            var errorText = string.Empty;
            configFile.HandleError = (e) => errorText = e.Message;

            // Act
            configFile.Load(BAD_FILE_NAME);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(errorText));
        }

        /// <summary>
        /// Defines the test method Load_Error_On_BadJSon.
        /// </summary>
        [TestMethod]
        public void Load_Error_On_BadJSon()
        {
            // Arrange
            var configFile = new ConfigFile();
            var errorText = string.Empty;
            var fileName = Path.GetTempFileName();
            configFile.HandleError = (e) => errorText = e.Message;
            File.WriteAllText(fileName, BAD_JSON);

            // Act
            configFile.Load(fileName);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(errorText));
        }

        /// <summary>
        /// Defines the test method Save_Error_On_BadFileName.
        /// </summary>
        [TestMethod]
        public void Save_Error_On_BadFileName()
        {
            // Arrange
            var configFile = new ConfigFile();
            var errorText = string.Empty;
            configFile.HandleError = (e) => errorText = e.Message;

            // Act
            configFile.Save(BAD_FILE_NAME);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(errorText));
        }

        /// <summary>
        /// Asserts the configuration contents.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void AssertConfigContents(Configuration config)
        {
            const string ERROR_FMT = "Expected: {0}; Actual: {1}";

            // test the email configuration
            Assert.AreEqual(ConfigFileTestCommon.TEST_HOST, config.EMailConfig.Host,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_HOST, config.EMailConfig.Host));
            Assert.AreEqual(ConfigFileTestCommon.TEST_PORT, config.EMailConfig.Port,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_PORT, config.EMailConfig.Port));
            Assert.IsTrue(config.EMailConfig.Ssl);
            Assert.AreEqual(ConfigFileTestCommon.TEST_FROM_EMAIL, config.EMailConfig.FromEmail,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_FROM_EMAIL, config.EMailConfig.FromEmail));
            Assert.AreEqual(ConfigFileTestCommon.TEST_USER_NAME, config.EMailConfig.UserName,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_USER_NAME, config.EMailConfig.UserName));
            Assert.AreEqual(ConfigFileTestCommon.TEST_PASSWORD, config.EMailConfig.Password,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_PASSWORD, config.EMailConfig.Password));

            // test the email recipients dictionary
            Assert.IsTrue(config.EMailConfig.Recipients.ContainsKey(ConfigFileTestCommon.TEST_RECIPIENT_1_NAME));
            Assert.IsTrue(config.EMailConfig.Recipients[ConfigFileTestCommon.TEST_RECIPIENT_1_NAME] == ConfigFileTestCommon.TEST_RECIPIENT_1_EMAIL);
            Assert.IsTrue(config.EMailConfig.Recipients.ContainsKey(ConfigFileTestCommon.TEST_RECIPIENT_2_NAME));
            Assert.IsTrue(config.EMailConfig.Recipients[ConfigFileTestCommon.TEST_RECIPIENT_2_NAME] == ConfigFileTestCommon.TEST_RECIPIENT_2_EMAIL);

            // test the VSO configuration
            Assert.AreEqual(ConfigFileTestCommon.TEST_BASE_URI, config.VsoConfig.BaseUri,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_POLLING_INTERVAL, config.VsoConfig.BaseUri));
            Assert.AreEqual(ConfigFileTestCommon.TEST_TOKEN, config.VsoConfig.Token,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_TOKEN, config.VsoConfig.Token));

            // test the config items dictionary
            Assert.IsTrue(config.VsoConfig.ConfigItems.ContainsKey(ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME));
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME].AreaPath == ConfigFileTestCommon.TEST_TASK_ITEM_1_AREA_PATH);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME].FieldName == ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME].Description == ConfigFileTestCommon.TEST_TASK_ITEM_1_DESCRIPTION);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME].ValidationRegex == ConfigFileTestCommon.TEST_TASK_ITEM_1_REGEX_NON_ESCAPED);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME].HelpMessage == ConfigFileTestCommon.TEST_TASK_ITEM_1_HELP_TEXT);

            Assert.IsTrue(config.VsoConfig.ConfigItems.ContainsKey(ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME));
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME].AreaPath == ConfigFileTestCommon.TEST_TASK_ITEM_2_AREA_PATH);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME].FieldName == ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME].Description == ConfigFileTestCommon.TEST_TASK_ITEM_2_DESCRIPTION);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME].ValidationRegex == ConfigFileTestCommon.TEST_TASK_ITEM_2_REGEX_NON_ESCAPED);
            Assert.IsTrue(config.VsoConfig.ConfigItems[ConfigFileTestCommon.TEST_TASK_ITEM_2_FIELD_NAME].HelpMessage == ConfigFileTestCommon.TEST_TASK_ITEM_2_HELP_TEXT);

            // test the ControllerConfig
            Assert.AreEqual(ConfigFileTestCommon.TEST_POLLING_INTERVAL, config.ControllerConfig.PollingInterval,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_POLLING_INTERVAL, config.ControllerConfig.PollingInterval));
            Assert.AreEqual(ConfigFileTestCommon.TEST_REPORTING_INTERVAL, config.ControllerConfig.ReportingInterval,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_REPORTING_INTERVAL, config.ControllerConfig.ReportingInterval));
            Assert.AreEqual(ConfigFileTestCommon.TEST_REPORT_EMAIL, config.ControllerConfig.ReportEmail,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_REPORT_EMAIL, config.ControllerConfig.ReportEmail));
            Assert.AreEqual(ConfigFileTestCommon.TEST_REPORT_FOLDER, config.ControllerConfig.ReportFolder,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_REPORT_FOLDER, config.ControllerConfig.ReportFolder));
            Assert.AreEqual(ConfigFileTestCommon.TEST_LOG_FOLDER, config.ControllerConfig.LogFolder,
                string.Format(ERROR_FMT, ConfigFileTestCommon.TEST_LOG_FOLDER, config.ControllerConfig.LogFolder));
        }
    }
}

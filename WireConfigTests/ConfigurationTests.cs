// ***********************************************************************
// Assembly         : WireConfigTests
// Author           : jiatli93
// Created          : 12-07-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="ConfigurationTests.cs" company="Red Clay">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Runtime.Remoting.Channels;
using NSubstitute.ExceptionExtensions;
using WireCommon;
using WireConfig;

namespace WireConfigTests
{
    /// <summary>
    /// Defines test class ConfigurationTests.
    /// </summary>
    [TestClass]
    public class ConfigurationTests
    {
        /// <summary>
        /// The mock e mail configuration
        /// </summary>
        private IEMailConfig mockEMailConfig;
        /// <summary>
        /// The mock vso configuration
        /// </summary>
        private IVSOConfig mockVsoConfig;
        /// <summary>
        /// The mock controller configuration
        /// </summary>
        private IControllerConfig mockControllerConfig;

        /// <summary>
        /// The valid setting name
        /// </summary>
        private const string VALID_SETTING_NAME = "VALID_SETTING_NAME";
        /// <summary>
        /// The invalid setting name
        /// </summary>
        private const string INVALID_SETTING_NAME = "INVALID_SETTING_NAME";
        /// <summary>
        /// The valid display name
        /// </summary>
        private const string VALID_DISPLAY_NAME = "VALID_DISPLAY_NAME";
        /// <summary>
        /// The invalid display name
        /// </summary>
        private const string INVALID_DISPLAY_NAME = "INVALID_DISPLAY_NAME";
        /// <summary>
        /// The valid setting key
        /// </summary>
        private const string VALID_SETTING_KEY = "VALID_SETTING_KEY";
        /// <summary>
        /// The invalid setting key
        /// </summary>
        private const string INVALID_SETTING_KEY = ", VALID_SETTING_KEY";
        /// <summary>
        /// The valid value
        /// </summary>
        private const string VALID_VALUE = "VALID_VALUE";
        /// <summary>
        /// The invalid value
        /// </summary>
        private const string INVALID_VALUE = "INVALID_VALUE";
        /// <summary>
        /// The exception text
        /// </summary>
        private const string EXCEPTION_TEXT = "EXCEPTION_TEXT";

        /// <summary>
        /// The configuration error text
        /// </summary>
        private string configErrorText = null;
        /// <summary>
        /// The configuration message text
        /// </summary>
        private string configMessageText = null;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mockEMailConfig = Substitute.For<EMailConfig>();
            mockVsoConfig = Substitute.For<VSOConfig>();
            mockControllerConfig = Substitute.For<ControllerConfig>();
        }

        /// <summary>
        /// Creates the configuration.
        /// </summary>
        /// <returns>Configuration.</returns>
        private Configuration CreateConfiguration()
        {
            Configuration result = new Configuration(mockEMailConfig, mockVsoConfig, mockControllerConfig);
            result.HandleError = (e) => configErrorText = e.Message;
            result.HandleMessage = (s) => configMessageText = s;
            return result;
        }

        /// <summary>
        /// Defines the test method GetDisplayName_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetDisplayName_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetDisplayName(VALID_SETTING_NAME).Returns(VALID_DISPLAY_NAME);
            mockVsoConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockControllerConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);

            // Act
            var result = configuration.GetDisplayName(VALID_SETTING_NAME);

            // Assert
            Assert.AreEqual(VALID_DISPLAY_NAME, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetDisplayName_VSOConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetDisplayName_VSOConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockVsoConfig.GetDisplayName(VALID_SETTING_NAME).Returns(VALID_DISPLAY_NAME);
            mockControllerConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);

            // Act
            var result = configuration.GetDisplayName(VALID_SETTING_NAME);

            // Assert
            Assert.AreEqual(VALID_DISPLAY_NAME, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetDisplayName_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetDisplayName_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            mockEMailConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockVsoConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockControllerConfig.GetDisplayName(VALID_SETTING_NAME).Returns(VALID_DISPLAY_NAME);

            // Act
            var result = configuration.GetDisplayName(VALID_SETTING_NAME);

            // Assert
            Assert.AreEqual(VALID_DISPLAY_NAME, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetDisplayName_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetDisplayName_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetDisplayName(VALID_SETTING_NAME).Returns(VALID_DISPLAY_NAME);
            mockVsoConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockControllerConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);

            // Act
            var result = configuration.GetDisplayName(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetDisplayName_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetDisplayName_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockVsoConfig.GetDisplayName(VALID_SETTING_NAME).Returns(VALID_DISPLAY_NAME);
            mockControllerConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);

            // Act
            var result = configuration.GetDisplayName(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetDisplayName_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetDisplayName_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            mockEMailConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockVsoConfig.GetDisplayName(VALID_SETTING_NAME).Returns(INVALID_DISPLAY_NAME);
            mockControllerConfig.GetDisplayName(VALID_SETTING_NAME).Returns(VALID_DISPLAY_NAME);

            // Act
            var result = configuration.GetDisplayName(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetValue01_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetValue01_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();
            
            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME).Returns(VALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(VALID_SETTING_NAME);

            // Assert
            Assert.AreEqual(VALID_VALUE, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetValue01_VSOConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetValue01_VSOConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();
            
            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME).Returns(VALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(VALID_SETTING_NAME);

            // Assert
            Assert.AreEqual(VALID_VALUE, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetValue01_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetValue01_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();
            
            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            mockEMailConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME).Returns(VALID_VALUE);

            // Act
            var result = configuration.GetValue(VALID_SETTING_NAME);

            // Assert
            Assert.AreEqual(VALID_VALUE, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetValue01_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetValue01_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME).Returns(VALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetValue01_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetValue01_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME).Returns(VALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetValue01_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetValue01_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            mockEMailConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME).Returns(VALID_VALUE);

            // Act
            var result = configuration.GetValue(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetValue02_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetValue02_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(VALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY);

            // Assert
            Assert.AreEqual(VALID_VALUE, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetValue02_VSOConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetValue02_VSOConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(VALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY);

            // Assert
            Assert.AreEqual(VALID_VALUE, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetValue02_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void GetValue02_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            mockEMailConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(VALID_VALUE);

            // Act
            var result = configuration.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY);

            // Assert
            Assert.AreEqual(VALID_VALUE, result);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method GetValue02_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetValue02_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(VALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME, INVALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME, INVALID_SETTING_KEY).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetValue02_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetValue02_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            mockEMailConfig.GetValue(VALID_SETTING_NAME, INVALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(VALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME, INVALID_SETTING_KEY).Returns(INVALID_VALUE);

            // Act
            var result = configuration.GetValue(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method GetValue02_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void GetValue02_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            mockEMailConfig.GetValue(VALID_SETTING_NAME, INVALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockVsoConfig.GetValue(VALID_SETTING_NAME, INVALID_SETTING_KEY).Returns(INVALID_VALUE);
            mockControllerConfig.GetValue(VALID_SETTING_NAME, VALID_SETTING_KEY).Returns(VALID_VALUE);

            // Act
            var result = configuration.GetValue(INVALID_SETTING_NAME);

            // Assert
            Assert.IsNull(result);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue01_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void SetValue01_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue($"{VALID_SETTING_NAME}={VALID_VALUE}");

            // Assert
            mockEMailConfig.Received().SetValue(VALID_SETTING_NAME, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method SetValue01_VSOConfig_Positive.
        /// </summary>
        [TestMethod]
        public void SetValue01_VSOConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue($"{VALID_SETTING_NAME}={VALID_VALUE}");

            // Assert
            mockVsoConfig.Received().SetValue(VALID_SETTING_NAME, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method SetValue01_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void SetValue01_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.SetValue($"{VALID_SETTING_NAME}={VALID_VALUE}");

            // Assert
            mockControllerConfig.Received().SetValue(VALID_SETTING_NAME, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method SetValue02_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void SetValue02_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue(VALID_SETTING_NAME, $"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            mockEMailConfig.Received().SetValue(VALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method SetValue02_VSOConfig_Positive.
        /// </summary>
        [TestMethod]
        public void SetValue02_VSOConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue(VALID_SETTING_NAME, $"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            mockVsoConfig.Received().SetValue(VALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method SetValue02_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void SetValue02_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.SetValue(VALID_SETTING_NAME, $"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            mockControllerConfig.Received().SetValue(VALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method SetValue01_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void SetValue01_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue($"{INVALID_SETTING_NAME}={VALID_VALUE}");

            // Assert
            mockEMailConfig.DidNotReceive().SetValue(INVALID_SETTING_NAME, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue01_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void SetValue01_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue($"{INVALID_SETTING_NAME}={VALID_VALUE}");

            // Assert
            mockVsoConfig.DidNotReceive().SetValue(INVALID_SETTING_NAME, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue01_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void SetValue01_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.SetValue($"{INVALID_SETTING_NAME}={VALID_VALUE}");

            // Assert
            mockControllerConfig.DidNotReceive().SetValue(INVALID_SETTING_NAME, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue02_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void SetValue02_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue(INVALID_SETTING_NAME, $"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            mockEMailConfig.DidNotReceive().SetValue(INVALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            mockEMailConfig.DidNotReceive().SetValue(VALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue02_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void SetValue02_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.SetValue(INVALID_SETTING_NAME, $"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            mockVsoConfig.DidNotReceive().SetValue(INVALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            mockVsoConfig.DidNotReceive().SetValue(VALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue02_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void SetValue02_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.SetValue(INVALID_SETTING_NAME, $"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            mockControllerConfig.DidNotReceive().SetValue(INVALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            mockControllerConfig.DidNotReceive().SetValue(VALID_SETTING_NAME, VALID_SETTING_KEY, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue01_ThrowException.
        /// </summary>
        [TestMethod]
        public void SetValue01_ThrowException()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();
            mockEMailConfig.ContainsSetting(string.Empty).ThrowsForAnyArgs(new Exception(EXCEPTION_TEXT));

            // Act
            configuration.SetValue($"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            Assert.AreEqual(EXCEPTION_TEXT, configErrorText);
        }

        /// <summary>
        /// Defines the test method SetValue02_ThrowException.
        /// </summary>
        [TestMethod]
        public void SetValue02_ThrowException()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();
            mockEMailConfig.ContainsSetting(string.Empty).ThrowsForAnyArgs(new Exception(EXCEPTION_TEXT));

            // Act
            configuration.SetValue(VALID_SETTING_NAME,$"{VALID_SETTING_KEY}={VALID_VALUE}");

            // Assert
            Assert.AreEqual(EXCEPTION_TEXT, configErrorText);
        }

        /// <summary>
        /// Defines the test method DeleteValue_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void DeleteValue_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.DeleteValue(VALID_SETTING_NAME, VALID_VALUE);

            // Assert
            mockEMailConfig.Received().DeleteValue(VALID_SETTING_NAME, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method DeleteValue_VsoConfig_Positive.
        /// </summary>
        [TestMethod]
        public void DeleteValue_VsoConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.DeleteValue(VALID_SETTING_NAME, VALID_VALUE);

            // Assert
            mockVsoConfig.Received().DeleteValue(VALID_SETTING_NAME, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method DeleteValue_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void DeleteValue_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.DeleteValue(VALID_SETTING_NAME, VALID_VALUE);

            // Assert
            mockControllerConfig.Received().DeleteValue(VALID_SETTING_NAME, VALID_VALUE);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method DeleteValue_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void DeleteValue_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.DeleteValue(INVALID_SETTING_NAME, VALID_VALUE);

            // Assert
            mockEMailConfig.DidNotReceive().DeleteValue(INVALID_SETTING_NAME, VALID_VALUE);
            mockEMailConfig.DidNotReceive().DeleteValue(VALID_SETTING_NAME, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method DeleteValue_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void DeleteValue_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.DeleteValue(INVALID_SETTING_NAME, VALID_VALUE);

            // Assert
            mockVsoConfig.DidNotReceive().DeleteValue(INVALID_SETTING_NAME, VALID_VALUE);
            mockVsoConfig.DidNotReceive().DeleteValue(VALID_SETTING_NAME, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method DeleteValue_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void DeleteValue_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.DeleteValue(INVALID_SETTING_NAME, VALID_VALUE);

            // Assert
            mockControllerConfig.DidNotReceive().DeleteValue(INVALID_SETTING_NAME, VALID_VALUE);
            mockControllerConfig.DidNotReceive().DeleteValue(VALID_SETTING_NAME, VALID_VALUE);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method ClearValue_EMailConfig_Positive.
        /// </summary>
        [TestMethod]
        public void ClearValue_EMailConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.ClearValue(VALID_SETTING_NAME);

            // Assert
            mockEMailConfig.Received().ClearValue(VALID_SETTING_NAME);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method ClearValue_VSOConfig_Positive.
        /// </summary>
        [TestMethod]
        public void ClearValue_VSOConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);

            // Act
            configuration.ClearValue(VALID_SETTING_NAME);

            // Assert
            mockVsoConfig.Received().ClearValue(VALID_SETTING_NAME);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method ClearValue_ControllerConfig_Positive.
        /// </summary>
        [TestMethod]
        public void ClearValue_ControllerConfig_Positive()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.ClearValue(VALID_SETTING_NAME);

            // Assert
            mockControllerConfig.Received().ClearValue(VALID_SETTING_NAME);
            Assert.IsTrue(string.IsNullOrEmpty(configErrorText));
        }

        /// <summary>
        /// Defines the test method ClearValue_EMailConfig_Negative.
        /// </summary>
        [TestMethod]
        public void ClearValue_EMailConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.ClearValue(INVALID_SETTING_NAME);

            // Assert
            mockEMailConfig.DidNotReceive().ClearValue(INVALID_SETTING_NAME);
            mockEMailConfig.DidNotReceive().ClearValue(VALID_SETTING_NAME);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method ClearValue_VSOConfig_Negative.
        /// </summary>
        [TestMethod]
        public void ClearValue_VSOConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);
            mockControllerConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);

            // Act
            configuration.ClearValue(INVALID_SETTING_NAME);

            // Assert
            mockVsoConfig.DidNotReceive().ClearValue(INVALID_SETTING_NAME);
            mockVsoConfig.DidNotReceive().ClearValue(VALID_SETTING_NAME);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }

        /// <summary>
        /// Defines the test method ClearValue_ControllerConfig_Negative.
        /// </summary>
        [TestMethod]
        public void ClearValue_ControllerConfig_Negative()
        {
            // Arrange
            configErrorText = null;
            var configuration = this.CreateConfiguration();

            mockEMailConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockVsoConfig.ContainsSetting(INVALID_SETTING_NAME).Returns(false);
            mockControllerConfig.ContainsSetting(VALID_SETTING_NAME).Returns(true);

            // Act
            configuration.ClearValue(INVALID_SETTING_NAME);

            // Assert
            mockControllerConfig.DidNotReceive().ClearValue(INVALID_SETTING_NAME);
            mockControllerConfig.DidNotReceive().ClearValue(VALID_SETTING_NAME);
            var expected = string.Format(Constants.PROPERTY_ERROR_FMT, INVALID_SETTING_NAME);
            Assert.AreEqual(expected, configErrorText);
        }
    }
}

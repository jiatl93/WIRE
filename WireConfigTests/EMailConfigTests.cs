// ***********************************************************************
// Assembly         : WireConfigTests
// Author           : jiatli93
// Created          : 12-07-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="EMailConfigTests.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using WireCommon;
using WireConfig;

namespace WireConfigTests
{
    /// <summary>
    /// Defines test class EMailConfigTests.
    /// </summary>
    [TestClass]
    public class EMailConfigTests
    {
        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {

        }

        /// <summary>
        /// Creates the e mail configuration.
        /// </summary>
        /// <returns>EMailConfig.</returns>
        private EMailConfig CreateEMailConfig()
        {
            EMailConfig result = new EMailConfig();
            return result;
        }

        /// <summary>
        /// Defines the test method Init_StateUnderTest_ExpectedBehavior.
        /// </summary>
        [TestMethod]
        public void Init_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            // Init() runs in the constructor
            var eMailConfig = this.CreateEMailConfig();

            // Assert
            // We can test the count of settings in the settings dictionary
            Assert.AreEqual(7, eMailConfig.Settings.Count);
        }

        /// <summary>
        /// Defines the test method Assignment_Error_Port.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void Assignment_Error_Port()
        {
            // Arrange
            var eMailConfig = this.CreateEMailConfig();

            // Act
            eMailConfig.SetValue(EMailConfig.PORT_KEY, "X");

            // Assert - exception expected
        }

        /// <summary>
        /// Defines the test method Assignment_Error_Ssl.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void Assignment_Error_Ssl()
        {
            // Arrange
            var eMailConfig = this.CreateEMailConfig();

            // Act
            eMailConfig.SetValue(EMailConfig.SSL_KEY, "X");

            // Assert - exception expected
        }

        /// <summary>
        /// Defines the test method Deletion_Success_Recipient.
        /// </summary>
        [TestMethod]
        public void Deletion_Success_Recipient()
        {
            // Arrange
            var eMailConfig = this.CreateEMailConfig();
            ConfigFileTestCommon.SetupEMailConfig(eMailConfig);

            // Act
            eMailConfig.DeleteValue(EMailConfig.RECIPIENTS_KEY,
                ConfigFileTestCommon.TEST_RECIPIENT_1_NAME);

            // Assert
            Assert.AreEqual(1, eMailConfig.Recipients.Count);
        }

        /// <summary>
        /// Defines the test method GetValue_Error_Recipient.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValue_Error_Recipient()
        {
            // Arrange
            var eMailConfig = this.CreateEMailConfig();
            ConfigFileTestCommon.SetupEMailConfig(eMailConfig);

            // Act
            eMailConfig.GetValue(EMailConfig.RECIPIENTS_KEY, "X");

            // Assert - exception expected
        }

        /// <summary>
        /// Defines the test method Deletion_Error_Recipient.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deletion_Error_Recipient()
        {
            // Arrange
            var eMailConfig = this.CreateEMailConfig();
            ConfigFileTestCommon.SetupEMailConfig(eMailConfig);

            // Act
            eMailConfig.DeleteValue(EMailConfig.RECIPIENTS_KEY, "X");

            // Assert - exception expected
        }

        /// <summary>
        /// Defines the test method Clear_Success_Recipient.
        /// </summary>
        [TestMethod]
        public void Clear_Success_Recipient()
        {
            // Arrange
            var eMailConfig = this.CreateEMailConfig();
            ConfigFileTestCommon.SetupEMailConfig(eMailConfig);

            // Act
            int initialCount = eMailConfig.Recipients.Count;
            eMailConfig.ClearValue(EMailConfig.RECIPIENTS_KEY);

            // Assert
            Assert.AreEqual(2, initialCount);
            Assert.AreEqual(0, eMailConfig.Recipients.Count);
        }
    }
}

// ***********************************************************************
// Assembly         : WireConfigTests
// Author           : jiatli93
// Created          : 12-08-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="VSOConfigTests.cs" company="Red Clay">
//     Copyright ©  2020
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
    /// Defines test class VSOConfigTests.
    /// </summary>
    [TestClass]
    public class VSOConfigTests
    {
        /// <summary>
        /// The configuration error text
        /// </summary>
        private string configErrorText;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {

        }

        /// <summary>
        /// Creates the vso configuration.
        /// </summary>
        /// <returns>VSOConfig.</returns>
        private VSOConfig CreateVSOConfig()
        {
            VSOConfig result = new VSOConfig();
            result.HandleError = (e) => configErrorText = e.Message;
            return result;
        }

        /// <summary>
        /// Defines the test method Init_StateUnderTest_ExpectedBehavior.
        /// </summary>
        [TestMethod]
        public void Init_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            configErrorText = null;

            // Act - calls Init() in the constructor
            var vsoConfig = this.CreateVSOConfig();

            // Assert
            Assert.AreEqual(4, vsoConfig.Settings.Count);
            Assert.IsNull(configErrorText);
        }

        //PromptForLogin
        /// <summary>
        /// Defines the test method Assignment_Error_PromptForLogin.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void Assignment_Error_PromptForLogin()
        {
            // Arrange
            configErrorText = null;
            var vsoConfig = this.CreateVSOConfig();

            // Act
            vsoConfig.SetValue(VSOConfig.PROMPT_FOR_LOGIN_KEY, "X");

            // Assert - exception expected
        }

        //ConfigItems
        /// <summary>
        /// Defines the test method Assignment_Error_Recipient.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Assignment_Error_Recipient()
        {
            // Arrange
            configErrorText = null;
            var vsoConfig = this.CreateVSOConfig();

            // Act
            vsoConfig.SetValue(VSOConfig.CONFIGITEMS_KEY,
                ConfigFileTestCommon.TEST_TASK_ITEM_1_FIELD_NAME,
                0);

            // Assert - exception expected
        }
    }
}

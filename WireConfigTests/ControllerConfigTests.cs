// ***********************************************************************
// Assembly         : WireConfigTests
// Author           : jiatli93
// Created          : 12-09-2020
//
// Last Modified By : jiatli93
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="ControllerConfigTests.cs" company="Red Clay">
//     Copyright ©2020 RedClay LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using WireConfig;

namespace WireConfigTests
{
    /// <summary>
    /// Defines test class ControllerConfigTests.
    /// </summary>
    [TestClass]
    public class ControllerConfigTests
    {
        /// <summary>
        /// Creates the controller configuration.
        /// </summary>
        /// <returns>ControllerConfig.</returns>
        private ControllerConfig CreateControllerConfig()
        {
            return new ControllerConfig();
        }

        /// <summary>
        /// Defines the test method Init_Positive.
        /// </summary>
        [TestMethod]
        public void Init_Positive()
        {
            // Arrange/Act (init runs in the constructor)
            var controllerConfig = this.CreateControllerConfig();

            // Assert
            Assert.AreEqual(5, controllerConfig.Settings.Count);
        }

        /// <summary>
        /// Defines the test method Assignment_Error_PollingInterval.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void Assignment_Error_PollingInterval()
        {
            // Arrange
            var controllerConfig = this.CreateControllerConfig();

            // Act
            controllerConfig.SetValue(ControllerConfig.POLLINGINTERVAL_KEY, "X");

            // Assert - exception expected
        }

        /// <summary>
        /// Defines the test method Assignment_Error_ReportingInterval.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void Assignment_Error_ReportingInterval()
        {
            // Arrange
            var controllerConfig = this.CreateControllerConfig();

            // Act
            controllerConfig.SetValue(ControllerConfig.REPORTINGINTERVAL_KEY, "X");

            // Assert - exception expected
        }
    }
}

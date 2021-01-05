using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using NSubstitute.Routing.Handlers;
using WireCLI;
using WireCommon;
using WireConfig;

namespace WireCLITests
{
    [TestClass]
    public class CommandProcessorTests
    {
        private IConfigFile configFile;

        [TestInitialize]
        public void TestInitialize()
        {
            this.configFile = Substitute.For<IConfigFile>();
        }

        private CommandProcessor CreateCommandProcessor()
        {
            //mockConfigFile = Substitute.For<IConfigFile>();
            configFile = new ConfigFile();
            return new CommandProcessor(configFile);
        }

        [TestMethod]
        public void Message_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandProcessor = this.CreateCommandProcessor();
            string actual = string.Empty;
            commandProcessor.HandleMessage += s => actual = s;
            string expected = "Test message";

            // Act
            commandProcessor.Message(expected);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Log_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandProcessor = this.CreateCommandProcessor();
            LogEntryType entryType = default(global::WireCommon.LogEntryType);
            string message = null;

            // Act
            commandProcessor.Log(entryType, message);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Report_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandProcessor = this.CreateCommandProcessor();
            string message = null;

            // Act
            commandProcessor.Report(
                message);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Error_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandProcessor = this.CreateCommandProcessor();
            Exception exception = null;

            // Act
            commandProcessor.Error(
                exception);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ProcessCommand_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandProcessor = this.CreateCommandProcessor();
            string commandString = null;

            // Act
            commandProcessor.ProcessCommand(
                commandString);

            // Assert
            Assert.Fail();
        }
    }
}

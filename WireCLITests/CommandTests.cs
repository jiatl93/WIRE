using Microsoft.VisualStudio.TestTools.UnitTesting;
using WireCLI;

namespace WireCLITests
{
    /// <summary>
    ///     Unit tests for the Command class.
    /// </summary>
    [TestClass]
    public class CommandTests
    {
        /// <summary>
        ///     The phrase
        /// </summary>
        private static readonly string[] phrase = { "life", "the", "universe", "and", "everything" };

        /// <summary>
        ///     The command 1
        /// </summary>
        private readonly string COMMAND_1 = phrase[0];

        /// <summary>
        ///     The command 2
        /// </summary>
        private readonly string COMMAND_2 = $"{phrase[0]} {phrase[1]}";

        /// <summary>
        ///     The command 3
        /// </summary>
        private readonly string COMMAND_3 = $"{phrase[0]} {phrase[1]}={phrase[2]}";

        /// <summary>
        ///     The command 4
        /// </summary>
        private readonly string COMMAND_4 = $"{phrase[0]} \"{phrase[1]} {phrase[2]}={phrase[3]} {phrase[4]}\"";

        /// <summary>
        ///     Tests the method1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange/Act
            var command = new Command(COMMAND_1);

            // Assert
            Assert.AreEqual(1, command.Values.Count);
            Assert.AreEqual(COMMAND_1, command.Values[0]);
        }

        /// <summary>
        ///     Tests the method2.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            // Arrange/Act
            var command = new Command(COMMAND_2);

            // Assert
            Assert.AreEqual(2, command.Values.Count);
            Assert.AreEqual(phrase[0], command.Values[0]);
            Assert.AreEqual(phrase[1], command.Values[1]);
        }

        /// <summary>
        ///     Tests the method3.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            // Arrange/Act
            var command = new Command(COMMAND_3);

            // Assert
            Assert.AreEqual(2, command.Values.Count);
            Assert.AreEqual(phrase[0], command.Values[0]);
            Assert.AreEqual($"{phrase[1]}={phrase[2]}", command.Values[1]);
        }

        /// <summary>
        ///     Tests the method4.
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            // Arrange/Act
            var command = new Command(COMMAND_4);

            // Assert
            Assert.AreEqual(2, command.Values.Count);
            Assert.AreEqual(phrase[0], command.Values[0]);
            Assert.AreEqual($"{phrase[1]} {phrase[2]}={phrase[3]} {phrase[4]}", command.Values[1]);
        }
    }
}
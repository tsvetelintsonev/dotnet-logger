using NUnit.Framework;
using Solution.Sinks;
using System;
using System.IO;

namespace Solution.Tests
{
    [TestFixture]
    public class RollingFileSinkTests
    {
        private string _logsFolderName = "logs";
        private string _logsDirectoryPath = string.Empty;
        private string _rollingStyleDateLogFilePath = string.Empty;

        [SetUp]
        public void Setup() 
        {
            _logsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), _logsFolderName);
            _rollingStyleDateLogFilePath = Path.Combine(_logsDirectoryPath, DateTimeOffset.UtcNow.ToString("yyyy-MM-dd"));
        }

        [TearDown]
        public void TearDown() 
        {
            Directory.Delete(_logsDirectoryPath, true);
        }

        [Test]
        public void IsCreatingLogFileWithDateRollingStyleIfFileDoesNotExists() 
        {
            // Arrange
            var rollingFileSink = new RollingFileSink(new DirectoryInfo(_logsDirectoryPath));

            // Assert
            FileAssert.DoesNotExist(_rollingStyleDateLogFilePath);
            
            rollingFileSink.Write("Message");

            FileAssert.Exists(_rollingStyleDateLogFilePath);
        }

        [Test]
        public void IsWritingLogMessage()
        {
            // Arrange
            var expectedLogMessage = "Message";
            var rollingFileSink = new RollingFileSink(new DirectoryInfo(_logsDirectoryPath));

            // Act
            rollingFileSink.Write(expectedLogMessage);
            var logLines = File.ReadAllLines(_rollingStyleDateLogFilePath);

            // Assert
            Assert.AreEqual(expectedLogMessage, logLines[0]);
        }

        [Test]
        public void IsAppendingLogMessages()
        {
            // Arrange
            var expectedFirstLogMessage = "First Message";
            var expectedSecondLogMessage = "Second Message";
            
            var rollingFileSink = new RollingFileSink(new DirectoryInfo(_logsDirectoryPath));

            // Act
            rollingFileSink.Write(expectedFirstLogMessage);
            rollingFileSink.Write(expectedSecondLogMessage);
            var logLines = File.ReadAllLines(_rollingStyleDateLogFilePath);

            // Assert
            Assert.AreEqual(expectedFirstLogMessage, logLines[0]);
            Assert.AreEqual(expectedSecondLogMessage, logLines[1]);
        }
    }
}

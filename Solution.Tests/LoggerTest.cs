using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Moq;
using NUnit.Framework;
using Solution.Sinks;

namespace Solution.Tests
{
    [TestFixture]
    public class LoggerTest
    {

        [Test]
        public void IsLoggingAsync()
        {
            // Arrange
            var delay = 1000;
            var maxTimeToWriteToLogInMilliseconds = delay;
            var sink = new SimulatedSlowSink(delay);
            var logger = new Logger(new List<ISink> { sink });
            var sw = Stopwatch.StartNew();

            // Act
            logger.Log("Message");

            // Assert
            Assert.Less(sw.ElapsedMilliseconds, maxTimeToWriteToLogInMilliseconds);

            Thread.Sleep(1500);
            Assert.AreEqual(sink.WrittenLine, logger.CurrentLogStatement.ToString());
        }

        [Test]
        public void IsNotFailingIfExceptionIsThrown()
        {
            // Arrange
            var sinkMock = new Mock<ISink>(MockBehavior.Strict);
            var logger = new Logger(new List<ISink> { sinkMock.Object });

            sinkMock.Setup(sink => sink.Write(It.IsAny<string>())).Throws(new Exception("Sink write failed."));

            // Act
            TestDelegate logAction = () => logger.Log("Message");

            // Assert
            Assert.DoesNotThrow(logAction);
        }

        public class SimulatedSlowSink : ISink
        {
            private readonly int _delayInMilliseconds;

            public SimulatedSlowSink(int delayInMilliseconds)
            {
                _delayInMilliseconds = delayInMilliseconds;
            }
            public void Write(string line)
            {
                Thread.Sleep(_delayInMilliseconds);
                Console.WriteLine(line);
                WrittenLine = line;
            }

            public string WrittenLine { get; private set; }
        }
    }
}

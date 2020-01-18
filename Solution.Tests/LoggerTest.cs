using System;
using System.Diagnostics;
using System.Threading;
using Moq;
using NUnit.Framework;

namespace Solution.Tests
{
    [TestFixture]
    public class LoggerTest
    {

        [Test]
        public void IsLoggingAsync()
        {
            var delay = 1000;
            var sink = new SimulatedSlowSink(delay);
            ILogger logger = new Logger(sink);

            var sw = Stopwatch.StartNew();

            logger.Log("Message");


            var maxTimeToWriteToLogInMilliseconds = delay;

            Assert.Less(sw.ElapsedMilliseconds, maxTimeToWriteToLogInMilliseconds);

            Thread.Sleep(delay);
            Assert.AreEqual("Message", sink.WrittenLine);
        }

        [Test]
        public void IsNotFailingIfExceptionIsThrown()
        {
            // Arrange
            var sinkMock = new Mock<ISink>(MockBehavior.Strict);
            var logger = new Logger(sinkMock.Object);

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

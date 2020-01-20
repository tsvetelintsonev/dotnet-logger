using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Moq;
using NUnit.Framework;
using Solution.Dispatchers;
using Solution.Sinks;
using Solution.Statements;

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
            var logStatementDispathcer = new AsyncLogStatementDispatcher(new List<ISink> { sink });

            var sw = Stopwatch.StartNew();

            // Act
            var logger = new Logger(logStatementDispathcer);
            logger.Log("Message");

            // Assert
            Assert.Less(sw.ElapsedMilliseconds, maxTimeToWriteToLogInMilliseconds);
        }

        [Test]
        public void IsNotFailingIfExceptionIsThrown()
        {
            // Arrange
            var sinkMock = new Mock<ISink>(MockBehavior.Strict);
            var logStatementDispathcerMock = new Mock<ILogStatementDispatcher>(MockBehavior.Strict);

            sinkMock.Setup(sink => sink.Write(It.IsAny<string>())).Throws(new Exception("Sink write failed."));

            // Act
            var logger = new Logger(logStatementDispathcerMock.Object);
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

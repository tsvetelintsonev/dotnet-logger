using System;
using System.Diagnostics;
using System.Threading;
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

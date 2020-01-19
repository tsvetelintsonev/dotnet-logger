using FuturePerspectives.Enrichers;
using FuturePerspectives.Sinks;
using System;
using System.IO;

namespace FuturePerspectives.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logsDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
            var logger = new Logger.Builder()
                .EnrichWith(new DummyLogStatementEnricher())
                .EnrichWith(new SimpleLogStatementEnricher())
                .WriteTo(new RollingFileSink(logsDirectory, RollingStyle.Date))
                .Build();

            logger.LogDebug("Test Debug message");
            logger.LogInformation("Test Information message");
            logger.LogWarning("Test Warning message");
            logger.LogError("Test Error message");
        }
    }
}

using FuturePerspectives.Enrichers;
using FuturePerspectives.Renderers;
using Solution;
using Solution.Sinks;
using System.IO;

namespace FuturePerspectives.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logsDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
            var logger = new Logger.Builder()
                .WriteTo(new RollingFileSink(logsDirectory, RollingStyle.Date))
                .WithRenderer<JsonLogStatementRenderer>()
                .WithEnricher<DummyLogStatementEnricher>()
                .Build();

            logger.LogDebug("Test Debug message");
            logger.LogInformation("Test Information message");
            logger.LogWarning("Test Warning message");
            logger.LogError("Test Error message");

            logger.Flush();
        }
    }
}

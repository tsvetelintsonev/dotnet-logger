using Solution.Dispatchers;
using Solution.Sinks;
using System.Collections.Generic;
using System.IO;

namespace Solution.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logsDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
            var rollingFileSink = new RollingFileSink(logsDirectory, RollingStyle.Date);
            var logStatementDispatcher = new AsyncLogStatementDispatcher(new List<ISink> { rollingFileSink });
            var logger = new Logger(logStatementDispatcher);

            logger.LogDebug("Test Debug message");
            logger.LogInformation("Test Information message");
            logger.LogWarning("Test Warning message");
            logger.LogError("Test Error message");

        }
    }
}

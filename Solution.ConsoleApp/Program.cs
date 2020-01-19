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
            var logger = new Logger(new List<ISink> { rollingFileSink });

            logger.LogDebug("Test Debug message");
            logger.LogInformation("Test Information message");
            logger.LogWarning("Test Warning message");
            logger.LogError("Test Error message");
        }
    }
}

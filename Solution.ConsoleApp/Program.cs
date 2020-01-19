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

            logger.LogInformation("Test information message");
        }
    }
}

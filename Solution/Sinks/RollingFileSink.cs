using System;
using System.IO;

namespace Solution.Sinks
{
    /// <summary>
    /// A sink for writing log messages to a file.
    /// Default file rolling style is 'Date'. A new file will be created at midnight.
    /// </summary>
    public class RollingFileSink : ISink
    {
        private readonly DirectoryInfo _directory;
        private readonly RollingStyle _rollingStyle;

        public RollingFileSink(DirectoryInfo directory, RollingStyle rollingStyle)
        {
            _directory = directory;
            _rollingStyle = rollingStyle;
        }

        public RollingFileSink(DirectoryInfo directory)
        {
            _directory = directory;
            _rollingStyle = RollingStyle.Date;
        }

        public void Write(string line)
        {
            var filePath = Path.Combine(_directory.FullName, CreateFileName(_rollingStyle));
            using (var streamWriter = new StreamWriter(filePath, true)) 
            {
                streamWriter.WriteLine(line);
            }
        }

        private string CreateFileName(RollingStyle rollingStyle)
        {
            var fileName = string.Empty;

            switch (rollingStyle)
            {
                case RollingStyle.Date:
                    fileName = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd");
                    break;
                default:
                    break;
            }

            return fileName;
        }
    }

    public enum RollingStyle 
    {
        Date
    }
}

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

        /// <summary>
        /// Constructs a new <see cref="RollingFileSink"/>
        /// </summary>
        /// <param name="directory">The directory used to write the log file to</param>
        /// <param name="rollingStyle">Rolling style used when creating new log files <see cref="RollingStyle"/></param>
        public RollingFileSink(DirectoryInfo directory, RollingStyle rollingStyle)
        {
            _directory = directory ?? throw new ArgumentNullException(nameof(directory));
            _rollingStyle = rollingStyle;
            EnsureDirectoryExists(directory);
        }

        /// <inheritdoc />
        public void Write(string line)
        {
            var filePath = Path.Combine(_directory.FullName, CreateFileName(_rollingStyle));
            try
            {
                using (var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(line);
                }
            }
            catch (Exception)
            {
                // Sent email etc.
            }
            
        }

        /// <summary>
        /// Creates the given directory if it doesn't exist.
        /// </summary>
        /// <param name="directory"></param>
        private void EnsureDirectoryExists(DirectoryInfo directory) 
        {
            if (!directory.Exists) 
            {
                Directory.CreateDirectory(directory.FullName);
            }
        }

        /// <summary>
        /// Creates a file name based on the given <see cref="RollingStyle" />
        /// <para>Examples:</para>
        /// <para>For <see cref="RollingStyle.Date"/> file name will be "yyyy-MM-dd" e.g. "2020-01-20"</para>
        /// </summary>
        /// <param name="rollingStyle">The <see cref="RollingStyle"/></param>
        /// <returns>File name</returns>
        private string CreateFileName(RollingStyle rollingStyle)
        {
            string fileName;
            switch (rollingStyle)
            {
                case RollingStyle.Date:
                    fileName = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd");
                    break;
                default:
                    fileName = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd");
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

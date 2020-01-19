using Solution.Sinks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solution
{
    public class Logger : ILogger
    {
        private readonly IList<ISink> _sinks;

        public Logger(IList<ISink> sinks)
        {
            _sinks = sinks;
        }
        
        public void Log(string message, LogLevel? logLevel = null)
        {
            Task.Run(() =>
            {
                try
                {
                    WriteToAllSinks(message);
                }
                catch (Exception)
                {
                    // Send email etc.
                }
            });
        }

        public void LogDebug(string message) 
        {
            Log(message, LogLevel.Debug);
        }

        public void LogInformation(string message)
        {
            Log(message, LogLevel.Information);
        }

        public void LogWarning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        public void LogError(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void WriteToAllSinks(string message) 
        {
            foreach (var sink in _sinks)
            {
                sink.Write(message);
            }
        }
    }
}
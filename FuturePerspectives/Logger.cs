using FuturePerspectives.Statements;
using Solution.Sinks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuturePerspectives
{
    /// <summary>
    /// The core logging pipeline.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly IList<ISink> _sinks;

        /// <summary>
        /// Constructs a new <see cref="Logger"/>
        /// </summary>
        /// <param name="sinks">The sinks to which the log statements will be written to.</param>
        public Logger(IList<ISink> sinks)
        {
            _sinks = sinks ?? throw new ArgumentNullException(nameof(sinks));
        }

        /// <inheritdoc />
        public void Log(string message, LogLevel? logLevel = null)
        {
            Task.Run(() =>
            {
                try
                {
                    CurrentLogStatement = new LogStatement(DateTimeOffset.UtcNow, logLevel ?? LogLevel.Information, message);
                    WriteToAllSinks(CurrentLogStatement.ToString());
                }
                catch (Exception)
                {
                    // Send email etc.
                }
            });
        }

        /// <inheritdoc />
        public void LogDebug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        /// <inheritdoc />
        public void LogInformation(string message)
        {
            Log(message, LogLevel.Information);
        }

        /// <inheritdoc />
        public void LogWarning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        /// <inheritdoc />
        public void LogError(string message)
        {
            Log(message, LogLevel.Error);
        }

        /// <inheritdoc />
        public ILogStatement CurrentLogStatement { get; private set; }

        private void WriteToAllSinks(string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Write(message);
            }
        }


    }
}
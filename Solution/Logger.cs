using Solution.Dispatchers;
using System;
using System.Threading.Tasks;

namespace Solution
{
    /// <summary>
    /// The core logging pipeline.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILogStatementDispatcher _logStatementDispatcher;

        /// <summary>
        /// Constructs a new <see cref="Logger"/>
        /// </summary>
        /// <param name="logStatementDispatcher">The log statement dispatcher <see cref="ILogStatementDispatcher"/></param>
        public Logger(ILogStatementDispatcher logStatementDispatcher)
        {
            _logStatementDispatcher = logStatementDispatcher;
        }

        /// <inheritdoc />
        public void Log(string message, LogLevel? logLevel = null)
        {
            try
            {
                var logStatement = new LogStatement(DateTimeOffset.UtcNow, logLevel ?? LogLevel.Information, message);
                _logStatementDispatcher.Dispatch(logStatement);
            }
            catch (Exception)
            {
                // Send email etc.
            }
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
        public void Flush()
        {
            _logStatementDispatcher.EnsureDispatchingFinished();
        }
    }
}
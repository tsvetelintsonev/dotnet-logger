using Solution.Statements;

namespace Solution
{
    /// <summary>
    /// The core API of the logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs message with the given <see cref="LogLevel"/> level. 
        /// If no log level is specified, <see cref="LogLevel.Information"/> is used.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level <see cref="LogLevel"/></param>
        void Log(string message, LogLevel? logLevel = null);

        /// <summary>
        /// Logs message with <see cref="LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="message">The log message</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs message with <see cref="LogLevel.Information"/> level.
        /// </summary>
        /// <param name="message">The log message</param>
        void LogInformation(string message);

        /// <summary>
        /// Logs message with <see cref="LogLevel.Warning"/> level.
        /// </summary>
        /// <param name="message">The log message</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs message with <see cref="LogLevel.Error"/> level.
        /// </summary>
        /// <param name="message">The log message</param>
        void LogError(string message);

        /// <summary>
        /// The current log statement that is being logged.
        /// </summary>
        ILogStatement CurrentLogStatement { get; }
    }
}

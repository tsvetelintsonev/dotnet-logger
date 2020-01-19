using System;

namespace Solution
{
    /// <summary>
    /// A log statement
    /// </summary>
    public class LogStatement : ILogStatement
    {
        /// <summary>
        /// Constructs a new <see cref="LogStatement"/>
        /// </summary>
        /// <param name="timestamp">The timestamp at which the log statement occurred.</param>
        /// <param name="logLevel">The level of the log statement.</param>
        /// <param name="message">The message describing the log statement.</param>
        public LogStatement(DateTimeOffset timestamp, LogLevel logLevel, string message)
        {
            Timestamp = timestamp;
            LogLevel = logLevel;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public DateTimeOffset Timestamp { get; private set; }

        public LogLevel LogLevel { get; private set; }

        public string Message { get; private set; }

        public override string ToString() 
        {
            return $"[{Timestamp.ToString("yyyy-MM-dd")}] [{LogLevel}] [{Message}]";
        }
    }
}

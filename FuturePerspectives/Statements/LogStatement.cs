using System;

namespace FuturePerspectives.Statements
{
    /// <summary>
    /// A log statement
    /// </summary>
    public class LogStatement : ILogStatement
    {
        private string _value;

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

        /// <inheritdoc />
        public DateTimeOffset Timestamp { get; private set; }

        /// <inheritdoc />
        public LogLevel LogLevel { get; private set; }

        /// <inheritdoc />
        public string Message { get; private set; }

        /// <inheritdoc />
        public string Render()
        {
            if (_value == null)
            {
                _value = $"[{Timestamp.ToString("yyyy-MM-dd HH:mm:ss.FFF zzz")}] [{LogLevel}] [{Message}]";
            }
            return _value;
        }
    }
}

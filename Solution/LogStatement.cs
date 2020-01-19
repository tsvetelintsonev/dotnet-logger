using System;

namespace Solution
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

        public DateTimeOffset Timestamp { get; private set; }

        public LogLevel LogLevel { get; private set; }

        public string Message { get; private set; }

        public override string ToString() 
        {
            if (_value == null) 
            {
                _value = $"[{Timestamp.ToString()}] [{LogLevel}] [{Message}]";
            }
            return _value;
        }
    }
}

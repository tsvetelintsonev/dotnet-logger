using FuturePerspectives;
using FuturePerspectives.Renderers;
using Solution.Statements;
using System;
using System.Collections.Generic;

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
        public LogStatement(DateTimeOffset timestamp, LogLevel logLevel, string message, IList<ILogStatementProperty> properties = null)
        {
            Timestamp = timestamp;
            LogLevel = logLevel;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Properties = properties;
        }

        /// <summary>
        /// The timestamp at which the log statement occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// The level of the log statement.
        /// </summary>
        public LogLevel LogLevel { get; private set; }

        /// <summary>
        /// The message describing the log statement.
        /// </summary>
        public string Message { get; private set; }

        public IList<ILogStatementProperty> Properties { get; private set; }

        /// <inheritdoc />
        public string Render(ILogStatementRenderer renderer) 
        {
            if (_value == null) 
            {
                _value = renderer.Render(this);
            }
            return _value;
        }
    }
}

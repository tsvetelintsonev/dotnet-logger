using System;

namespace Solution.Statements
{
    public interface ILogStatement
    {
        /// <summary>
        /// The timestamp at which the log statement occurred.
        /// </summary>
        DateTimeOffset Timestamp { get; }

        /// <summary>
        /// The level of the log statement.
        /// </summary>
        LogLevel LogLevel { get; }

        /// <summary>
        /// The message describing the log statement.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Renders the <see cref="string"/> representation of the log statement 
        /// </summary>
        /// <returns></returns>
        string Render();
    }
}

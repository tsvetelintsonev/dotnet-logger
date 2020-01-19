using System;

namespace Solution.Statements
{
    public interface ILogStatement
    {
        DateTimeOffset Timestamp { get; }
        LogLevel LogLevel { get; }
        string Message { get; }
    }
}

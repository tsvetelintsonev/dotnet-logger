using System;

namespace Solution
{
    public interface ILogStatement
    {
        DateTimeOffset Timestamp { get; }
        LogLevel LogLevel { get; }
        string Message { get; }
    }
}

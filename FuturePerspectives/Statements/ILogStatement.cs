using System;

namespace FuturePerspectives.Statements
{
    public interface ILogStatement
    {
        DateTimeOffset Timestamp { get; }
        LogLevel LogLevel { get; }
        string Message { get; }
    }
}

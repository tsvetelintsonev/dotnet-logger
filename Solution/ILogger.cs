namespace Solution
{
    public interface ILogger
    {
        void Log(string message, LogLevel? logLevel = null);
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        ILogStatement CurrentLogStatement { get; }
    }
}

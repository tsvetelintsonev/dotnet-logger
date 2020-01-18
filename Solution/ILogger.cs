namespace Solution
{
    public interface ILogger
    {
        void Log(string message, LogLevel? logLevel = null);
    }
}

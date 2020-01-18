namespace Solution.Enrichers
{
    public interface IDefaultLogStatementEnricher
    {
        void EnrichWithTimestamp(ref string message);
        void EnrichWithLogLevel(ref string message, LogLevel logLevel);
    }
}

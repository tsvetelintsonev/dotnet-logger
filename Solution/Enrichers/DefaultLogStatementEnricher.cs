using System;

namespace Solution.Enrichers
{
    public class DefaultLogStatementEnricher
    {
        public void EnrichWithTimestamp(ref string message) 
        {
            message = $"{DateTimeOffset.UtcNow.ToString()} {message}";
        }

        public void EnrichWithLogLevel(ref string message, LogLevel logLevel)
        {
            message = $"{logLevel.ToString()} {message}";
        }
    }
}

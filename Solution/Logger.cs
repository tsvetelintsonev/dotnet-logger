using Solution.Enrichers;
using System;
using System.Threading.Tasks;

namespace Solution
{
    public class Logger : ILogger
    {
        private readonly ISink _sink;
        private readonly IDefaultLogStatementEnricher _defaultLogStatementEnricher;

        public Logger(ISink sink)
        {
            _sink = sink;
            _defaultLogStatementEnricher = new DefaultLogStatementEnricher();
        }
        
        public void Log(string message, LogLevel? logLevel = null)
        {
            Task.Run(() =>
            {
                try
                {
                    if (logLevel != null) 
                    {
                        _defaultLogStatementEnricher.EnrichWithLogLevel(ref message, (LogLevel)logLevel);
                    }

                    _defaultLogStatementEnricher.EnrichWithTimestamp(ref message);

                    _sink.Write(message);
                }
                catch (Exception)
                {
                    // Send email etc.
                }
            });
        }
    }
}
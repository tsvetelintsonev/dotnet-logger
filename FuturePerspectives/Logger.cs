using FuturePerspectives.Enrichers;
using FuturePerspectives.Statements;
using Solution.Sinks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuturePerspectives
{
    /// <summary>
    /// The core logging pipeline.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly IList<ISink> _sinks;
        private readonly IList<ILogStatementEnricher> _enrichers;

        /// <summary>
        /// Constructs a new <see cref="Logger"/>
        /// </summary>
        /// <param name="sinks">The sinks to which the log statements will be written to.</param>
        private Logger(IList<ISink> sinks, IList<ILogStatementEnricher> enrichers = null)
        {
            _sinks = sinks ?? throw new ArgumentNullException(nameof(sinks));
            
            if (sinks.Count == 0)
            {
                throw new ArgumentException("At least one sink must be provided.", nameof(sinks));
            }

            _enrichers = enrichers;
        }

        /// <inheritdoc />
        public void Log(string message, LogLevel? logLevel = null)
        {
            Task.Run(() =>
            {
                try
                {
                    CurrentLogStatement = new LogStatement(DateTimeOffset.UtcNow, logLevel ?? LogLevel.Information, message);
                    WriteToAllSinks(CurrentLogStatement.ToString());
                }
                catch (Exception)
                {
                    // Send email etc.
                }
            });
        }

        /// <inheritdoc />
        public void LogDebug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        /// <inheritdoc />
        public void LogInformation(string message)
        {
            Log(message, LogLevel.Information);
        }

        /// <inheritdoc />
        public void LogWarning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        /// <inheritdoc />
        public void LogError(string message)
        {
            Log(message, LogLevel.Error);
        }

        /// <inheritdoc />
        public ILogStatement CurrentLogStatement { get; private set; }

        private void WriteToAllSinks(string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Write(message);
            }
        }

        public class Builder 
        {
            private IList<ISink> _sinks;
            private IList<ILogStatementEnricher> _enrichers;

            public Builder()
            {
                _sinks = new List<ISink>();
                _enrichers = new List<ILogStatementEnricher>();
            }

            public Builder WriteTo(ISink sink) 
            {
                _sinks.Add(sink);
                return this;
            }

            public Builder EnrichWith(ILogStatementEnricher enricher) 
            {
                _enrichers.Add(enricher);
                return this;
            }

            public ILogger Build() 
            {
                return new Logger(_sinks, _enrichers);
            }
        }
    }
}
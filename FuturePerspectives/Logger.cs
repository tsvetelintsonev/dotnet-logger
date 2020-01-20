using FuturePerspectives;
using FuturePerspectives.Enrichers;
using FuturePerspectives.Renderers;
using Solution.Dispatchers;
using Solution.Sinks;
using System;
using System.Collections.Generic;

namespace Solution
{
    /// <summary>
    /// The core logging pipeline.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILogStatementDispatcher _logStatementDispatcher;
        private readonly IList<ILogStatementEnricher> _logStatementEnrichers;

        /// <summary>
        /// Constructs a new <see cref="Logger"/>
        /// </summary>
        /// <param name="logStatementDispatcher">The log statement dispatcher <see cref="ILogStatementDispatcher"/></param>
        private Logger(ILogStatementDispatcher logStatementDispatcher, IList<ILogStatementEnricher> logStatementEnrichers = null)
        {
            _logStatementDispatcher = logStatementDispatcher;
            _logStatementEnrichers = logStatementEnrichers;
        }

        /// <inheritdoc />
        public void Log(string message, LogLevel? logLevel = null)
        {
            try
            {
                var logStatementProperties = new List<ILogStatementProperty>();
                foreach (var logStatementEnricher in _logStatementEnrichers)
                {
                    logStatementProperties.Add(logStatementEnricher.Enrich());
                }

                var logStatement = new LogStatement(DateTimeOffset.UtcNow, logLevel ?? LogLevel.Information, message, logStatementProperties);
                _logStatementDispatcher.Dispatch(logStatement);
            }
            catch (Exception)
            {
                // Send email etc.
            }
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
        public void Flush()
        {
            _logStatementDispatcher.EnsureDispatchingFinished();
        }

        public class Builder
        {
            private IList<ILogStatementEnricher> _logStatementEnrichers;
            private IList<ISink> _sinks;
            private ILogStatementDispatcher _logStatementDispatcher;
            private ILogStatementRenderer _logStatementRenderer;

            public Builder()
            {
                _logStatementEnrichers = new List<ILogStatementEnricher>();
                _sinks = new List<ISink>();
            }

            public Builder WriteTo(ISink sink)
            {
                _sinks.Add(sink);
                return this;
            }

            public Builder WithRenderer<TLogStatementRenderer>() where TLogStatementRenderer : ILogStatementRenderer, new()
            {
                _logStatementRenderer = new TLogStatementRenderer();
                return this;
            }

            public Builder WithEnricher<TLogStatementEnricher>() where TLogStatementEnricher : ILogStatementEnricher, new()
            {
                var enricher = new TLogStatementEnricher();
                _logStatementEnrichers.Add(enricher);
                return this;
            }
            public ILogger Build() 
            {
                _logStatementDispatcher = new AsyncLogStatementDispatcher(_sinks, _logStatementRenderer);
                return new Logger(_logStatementDispatcher, _logStatementEnrichers);
            }
        }
    }
}
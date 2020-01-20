using Solution.Sinks;
using Solution.Statements;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Solution.Dispatchers
{
    public class AsyncLogStatementDispatcher : ILogStatementDispatcher
    {
        private readonly IList<ISink> _sinks;
        private readonly Thread _thread;
        private readonly BlockingCollection<ILogStatement> _logQueue;

        /// <summary>
        /// Constructs a new <see cref="AsyncLogStatementDispatcher"
        /// </summary>
        /// <param name="sinks">The sinks to which the log statements will be written to.</param>
        public AsyncLogStatementDispatcher(IList<ISink> sinks)
        {
            _sinks = sinks ?? throw new ArgumentNullException(nameof(sinks));

            if (sinks.Count == 0)
            {
                throw new ArgumentException("At least one sink must be provided.", nameof(sinks));
            }

            _sinks = sinks;
            _logQueue = new BlockingCollection<ILogStatement>();

            _thread = new Thread(WriteToAllSinks);
            _thread.Name = "Log writer thread";
            _thread.IsBackground = true;
            _thread.Priority = ThreadPriority.BelowNormal;
            _thread.Start();
        }

        /// <summary>
        /// Dispatches the given log statement asynchronously to all sinks
        /// </summary>
        /// <param name="logStatement">The <see cref="ILogStatement" /></param>
        public void Dispatch(ILogStatement logStatement)
        {
            _logQueue.Add(logStatement);
        }

        /// <summary>
        /// Ensures that all log statements have been dispatched, in this case joins the calling <see cref="Thread" />.
        /// This method should typically be called in an application "exiting" event listener or similar to ensure that all log statements are correctly written.
        /// </summary>
        public void EnsureDispatchingFinished()
        {
            _logQueue.CompleteAdding();
            _thread.Join();
        }

        /// <summary>
        /// Writes a log statements to all sinks.
        /// </summary>
        private void WriteToAllSinks()
        {
            try
            {
                while (true)
                {
                    var logStatement = _logQueue.Take();

                    foreach (var sink in _sinks)
                    {
                        sink.Write(logStatement.Render());
                    }
                }
            }
            catch (Exception)
            {
                // Send email etc.
            }
        }
    }
}

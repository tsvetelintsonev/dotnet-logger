using Solution.Statements;

namespace Solution.Dispatchers
{
    public interface ILogStatementDispatcher
    {
        /// <summary>
        /// Dispatches the given log statement to all sinks
        /// </summary>
        /// <param name="logStatement">The <see cref="ILogStatement"</param>
        void Dispatch(ILogStatement logStatement);

        /// <summary>
        /// Ensures that all log statements have been dispatched.
        /// </summary>
        void EnsureDispatchingFinished();
    }
}

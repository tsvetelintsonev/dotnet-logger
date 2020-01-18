namespace Solution
{
    public class Logger : ILogger
    {
        private readonly ISink _sink;

        public Logger(ISink sink)
        {
            _sink = sink;
        }
        
        public void Log(string message)
        {
            _sink.Write(message);
        }
    }
}
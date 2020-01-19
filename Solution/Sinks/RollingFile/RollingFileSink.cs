namespace Solution.Sinks.File
{
    /// <summary>
    /// A sink for writing log messages to a file.
    /// Default file rolling style is 'Date', a new file will be created at midnight.
    /// </summary>
    public class RollingFileSink : ISink
    {
        private readonly RollingStyle _rollingStyle;

        public RollingFileSink(RollingStyle rollingStyle)
        {
            _rollingStyle = rollingStyle;
        }

        public RollingFileSink()
        {
            _rollingStyle = RollingStyle.Date;
        }

        public void Write(string line)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum RollingStyle 
    {
        Date
    }
}

namespace Solution.Sinks
{
    /// <summary>
    /// A sink for writing log messages to a file.
    /// Default file rolling style is 'Date', a new file will be created at midnight.
    /// </summary>
    public class FileSink : ISink
    {
        private readonly RollingStyle _rollingStyle;

        public FileSink(RollingStyle rollingStyle)
        {
            _rollingStyle = rollingStyle;
        }

        public FileSink()
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

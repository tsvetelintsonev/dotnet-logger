namespace Solution.Sinks
{
    public interface ISink
    {
        /// <summary>
        /// Writes the given single line.
        /// </summary>
        /// <param name="line"></param>
        void Write(string line);
    }
}
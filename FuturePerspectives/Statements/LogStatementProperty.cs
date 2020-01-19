﻿namespace FuturePerspectives.Statements
{
    public class LogStatementProperty : ILogStatementProperty
    {
        public LogStatementProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}

using FuturePerspectives.Statements;

namespace FuturePerspectives.Enrichers
{
    public class DummyLogStatementEnricher : ILogStatementEnricher
    {
        public ILogStatementProperty Enrich()
        {
            return new LogStatementProperty("dummyName", "dummyValue");
        }
    }
}

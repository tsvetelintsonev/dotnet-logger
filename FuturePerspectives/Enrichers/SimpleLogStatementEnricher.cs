using FuturePerspectives.Statements;

namespace FuturePerspectives.Enrichers
{
    public class SimpleLogStatementEnricher : ILogStatementEnricher
    {
        public ILogStatementProperty Enrich()
        {
            return new LogStatementProperty("simpleName", "simpleValue");
        }
    }
}

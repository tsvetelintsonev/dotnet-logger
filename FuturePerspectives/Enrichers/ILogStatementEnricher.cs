using FuturePerspectives.Statements;

namespace FuturePerspectives.Enrichers
{
    public interface ILogStatementEnricher
    {
        ILogStatementProperty Enrich();
    }
}

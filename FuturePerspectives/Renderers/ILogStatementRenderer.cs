using Solution.Statements;

namespace FuturePerspectives.Renderers
{
    public interface ILogStatementRenderer
    {
        string Render(ILogStatement logStatement);
    }
}

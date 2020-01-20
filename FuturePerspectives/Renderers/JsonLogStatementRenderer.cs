using Solution.Statements;
using System.Text.Json;

namespace FuturePerspectives.Renderers
{
    public class JsonLogStatementRenderer : ILogStatementRenderer
    {
        public string Render(ILogStatement logStatement)
        {
            return JsonSerializer.Serialize(logStatement);
        }
    }
}

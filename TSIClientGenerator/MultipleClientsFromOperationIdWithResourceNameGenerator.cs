using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;
using System.Linq;


namespace TSIClientGenerator
{
    public class MultipleClientsFromOperationIdWithResourceNameGenerator : MultipleClientsFromOperationIdOperationNameGenerator
    {
        public override string GetClientName(SwaggerDocument document, string path, SwaggerOperationMethod httpMethod, SwaggerOperation operation)
        {
            return string.Join("", path.Split('/')
                .Where(p => !p.Contains("{") && !string.IsNullOrWhiteSpace(p))
                .Select(p => char.ToUpper(p.First()) + p.Substring(1)));
        }
    }
}

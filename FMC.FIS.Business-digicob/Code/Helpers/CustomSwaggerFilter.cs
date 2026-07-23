
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace SwaggerFilter.Filters
{
    public class CustomSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            /*
            var nonMobileRoutes = swaggerDoc.Paths
                .Where(x => string.IsNullOrEmpty(x.Value.Description))
                .ToList();
            nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
            */
        }
    }
}

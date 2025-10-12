using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Live_Book.Classes
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>(); 
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "token",
                In = ParameterLocation.Header, 
                Required = false,  
                Description = "user login token", 
                Schema = new OpenApiSchema
                {
                    Type = "string", 
                }
            });
        }
    }
}

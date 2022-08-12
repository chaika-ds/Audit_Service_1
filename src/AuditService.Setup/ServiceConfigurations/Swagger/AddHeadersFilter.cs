using AuditService.Common.Attributes;
using bgTeam.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AuditService.Setup.ServiceConfigurations.Swagger;

/// <summary>
///     Swagger filter to add headers to request
/// </summary>
public class AddHeadersFilter : IOperationFilter
{
    private const string SchemaType = "string";

    /// <summary>
    ///     Apply filter to add headers
    /// </summary>
    /// <param name="operation">Swagger operation object</param>
    /// <param name="context">Swagger filter context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = (UseHeaderAttribute[])context.MethodInfo.GetCustomAttributes(typeof(UseHeaderAttribute), true);

        if (!attributes.Any())
            return;

        operation.Parameters ??= new List<OpenApiParameter>();

        attributes.DoForEach(attribute =>
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = attribute.Name,
                In = ParameterLocation.Header,
                Required = attribute.IsRequired,
                Schema = new OpenApiSchema
                {
                    Type = SchemaType
                }
            });
        });
    }
}
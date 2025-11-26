#if NET10_0_OR_GREATER
using Microsoft.AspNetCore.OpenApi;

namespace AltaSoft.DomainPrimitives.SwaggerExtensions;

/// <summary>
/// Extensions for Microsoft.AspNetCore.OpenApi.OpenApiOptions to add schema transformer for all DomainPrimitive types.
/// </summary>
public static class OpenApiOptionsExt
{
    /// <summary>
    /// Extensions for Microsoft.AspNetCore.OpenApi.OpenApiOptions to add schema transformer for all DomainPrimitive types.
    /// </summary>
    /// <param name="options"></param>
    extension(OpenApiOptions options)
    {
        /// <summary>
        /// Registers a schema transformer that applies OpenApi helper schemas for all domain primitives discovered in assemblies.
        /// </summary>
        public OpenApiOptions AddDomainPrimitivesSchemaTransformer()
        {
            options.AddSchemaTransformer(new DomainPrimitiveOpenApiSchemaTransformer());
            return options;
        }
    }
}
#endif

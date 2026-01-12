using System;
using System.Collections.Frozen;
using System.Reflection;
using System.Runtime.CompilerServices;
using AltaSoft.DomainPrimitives.OpenApiExtensions;
using Microsoft.Extensions.DependencyInjection;
#if NET10_0_OR_GREATER
using Microsoft.OpenApi;
#else
using Microsoft.OpenApi.Models;
#endif
using Swashbuckle.AspNetCore.SwaggerGen;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives.SwaggerExtensions;

/// <summary>
/// A static class providing methods to configure Swagger mappings for DomainPrimitive types.
/// </summary>
public static class SwaggerGenOptionsExt
{
    /// <param name="options">The SwaggerGenOptions to which the mappings will be added.</param>
    extension(SwaggerGenOptions options)
    {
        /// <summary>
        /// Adds Swagger mappings for all DomainPrimitive types to the specified SwaggerGenOptions.
        /// </summary>
        /// <param name="assemblies">The assemblies containing the DomainPrimitive types.</param>
        public void AddDomainPrimitivesSwaggerMappings(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                OpenApiHelperProcessor.ProcessAssembly(assembly, options.ProcessSwaggerOptions);
            }
        }

        /// <summary>
        /// Adds Swagger mappings for all DomainPrimitive types to the specified SwaggerGenOptions.
        /// </summary>
        public void AddAllDomainPrimitivesSwaggerMappings()
        {
            OpenApiHelperProcessor.ProcessOpenApiHelpers(options.ProcessSwaggerOptions);
        }

        /// <summary>
        /// Processes swagger options
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessSwaggerOptions(FrozenDictionary<Type, OpenApiSchema> values)
        {
            foreach (var (type, schema) in values)
                options.MapType(type, () => schema);
        }
    }
}

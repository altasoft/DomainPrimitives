using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi;

namespace AltaSoft.DomainPrimitives.SwaggerExtensions
{
    /// <summary>
    /// Provides functionality to scan loaded assemblies, detect those marked with
    /// <see cref="DomainPrimitiveAssemblyAttribute"/>, locate their OpenApiHelper types,
    /// extract their <c>Schemas</c> mappings, and pass these mappings to the provided callback.
    /// </summary>
    /// <remarks>
    /// This class recursively loads referenced assemblies (excluding System/Microsoft assemblies),
    /// finds all public <c>OpenApiHelper</c> types, reads their static <c>Schemas</c> fields,
    /// and invokes the callback for each discovered <see cref="FrozenDictionary{TKey, TValue}"/>
    /// containing <see cref="OpenApiSchema"/> definitions.
    /// </remarks>
    public static class OpenApiHelperProcessor
    {
        /// <summary>
        /// Scans all loaded assemblies and their references to find those marked with
        /// </summary>
        /// <param name="processOpenApiHelper"></param>
        internal static void ProcessOpenApiHelpers(Action<FrozenDictionary<Type, OpenApiSchema>> processOpenApiHelper)
        {
            var loadedAssemblies = new HashSet<string>(AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => IsSystemAssembly(a.FullName)).Select(a => a.FullName!));

            var assembliesToCheck = new Queue<Assembly>(AppDomain.CurrentDomain.GetAssemblies());
            var processedPrimitiveAssemblies = new HashSet<Assembly>();

            while (assembliesToCheck.Count > 0)
            {
                var assembly = assembliesToCheck.Dequeue();

                if (!processedPrimitiveAssemblies.Contains(assembly) &&
                    assembly.GetCustomAttribute<DomainPrimitiveAssemblyAttribute>() is not null)
                {
                    ProcessAssembly(assembly, processOpenApiHelper);
                    processedPrimitiveAssemblies.Add(assembly);
                }

                foreach (var reference in assembly.GetReferencedAssemblies())
                {
                    if (loadedAssemblies.Contains(reference.FullName) || IsSystemAssembly(reference.FullName))
                        continue;

                    var loadedAssembly = Assembly.Load(reference);
                    assembliesToCheck.Enqueue(loadedAssembly);
                    loadedAssemblies.Add(reference.FullName);
                }
            }
        }

        internal static void ProcessAssembly(Assembly assembly, Action<FrozenDictionary<Type, OpenApiSchema>> processOpenApiHelper)
        {
            var typesWithAddSwaggerMappings = assembly.GetExportedTypes()
                .Where(type => type is { IsPublic: true, Name: "OpenApiHelper" })
                .Select(type => type.GetField("Schemas", BindingFlags.Public | BindingFlags.Static));

            foreach (var method in typesWithAddSwaggerMappings)
            {
                var values = method?.GetValue(null) as FrozenDictionary<Type, OpenApiSchema>;
                if (values is null or { Count: 0 })
                    continue;

                processOpenApiHelper(values);
            }
        }

        private static bool IsSystemAssembly(string? assemblyFullName)
        {
            return assemblyFullName?.StartsWith("System.") != false || assemblyFullName.StartsWith("Microsoft.");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// A static class providing methods to configure Swagger mappings for DomainPrimitive types.
/// </summary>
public static class SwaggerGenOptionsExt
{
    /// <summary>
    /// Adds Swagger mappings for all DomainPrimitive types to the specified SwaggerGenOptions.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions to which the mappings will be added.</param>
    /// <param name="assemblies">The assemblies containing the DomainPrimitive types.</param>
    public static void AddDomainPrimitivesSwaggerMappings(this SwaggerGenOptions options, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ProcessAssembly(assembly, options);
        }
    }

    /// <summary>
    /// Adds Swagger mappings for all DomainPrimitive types to the specified SwaggerGenOptions.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions to which the mappings will be added.</param>
    public static void AddAllDomainPrimitivesSwaggerMappings(this SwaggerGenOptions options)
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
                ProcessAssembly(assembly, options);
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

    private static void ProcessAssembly(Assembly assembly, SwaggerGenOptions options)
    {
        var typesWithAddSwaggerMappings = assembly.GetExportedTypes()
            .Where(type => type is { IsPublic: true, Name: "SwaggerTypeHelper" })
            .Select(type => type.GetMethod("AddSwaggerMappings", BindingFlags.Public | BindingFlags.Static));

        // Calls the AddSwaggerMappings method for each type.
        foreach (var method in typesWithAddSwaggerMappings)
        {
            method?.Invoke(null, [options]);
        }
    }

    private static bool IsSystemAssembly(string? assemblyFullName)
    {
        return assemblyFullName?.StartsWith("System.") != false || assemblyFullName.StartsWith("Microsoft.");
    }
}

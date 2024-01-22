using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

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
	public static void AddAllDomainPrimitivesSwaggerMappings(this SwaggerGenOptions options)

	{
		// Retrieves all loaded assemblies in the current AppDomain.
		var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

		// Retrieves all types from all loaded assemblies that have a static SwaggerTypeHelper.AddSwaggerMappings method.
		var typesWithAddSwaggerMappings = loadedAssemblies
			.SelectMany(assembly => assembly.GetExportedTypes().Where(type => type is { IsPublic: true, Name: "SwaggerTypeHelper" }))
			.Select(type => type.GetMethod("AddSwaggerMappings", BindingFlags.Public | BindingFlags.Static));

		// Calls the AddSwaggerMappings method for each type.
		foreach (var method in typesWithAddSwaggerMappings)
		{
			method?.Invoke(null, [options]);
		}
	}
}
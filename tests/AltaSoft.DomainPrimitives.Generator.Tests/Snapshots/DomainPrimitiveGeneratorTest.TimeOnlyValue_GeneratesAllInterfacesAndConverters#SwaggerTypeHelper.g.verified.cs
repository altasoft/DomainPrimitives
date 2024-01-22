﻿//HintName: SwaggerTypeHelper.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a AltaSoft.DomainPrimitives.Generator v1.0.0
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace generator_Test.Converters.Extensions;

/// <summary>
/// Helper class providing methods to configure Swagger mappings for DomainPrimitive types of generator_Test
/// </summary>
public static class SwaggerTypeHelper
{
	/// <summary>
	/// Adds Swagger mappings for specific custom types to ensure proper OpenAPI documentation generation.
	/// </summary>
	/// <param name="options">The SwaggerGenOptions instance to which mappings are added..</param>
	/// <remarks>
	/// The method adds Swagger mappings for the following types:
	/// <see cref="TimeOnlyValue" />
	/// </remarks>
	public static void AddSwaggerMappings(this SwaggerGenOptions options)
	{
		options.MapType<TimeOnlyValue>(() => new OpenApiSchema
		{
			Type = "string",
			Format = "HH:mm:ss",
			Title = "TimeOnlyValue",
			Default = new OpenApiString(TimeOnlyValue.Default.ToString("hh:mm:ss", null))
		});
		options.MapType<TimeOnlyValue?>(() => new OpenApiSchema
		{
			Type = "string",
			Format = "HH:mm:ss",
			Nullable = true,
			Title = "Nullable<TimeOnlyValue>",
			Default = new OpenApiString(TimeOnlyValue.Default.ToString("hh:mm:ss", null))
		});
	}
}

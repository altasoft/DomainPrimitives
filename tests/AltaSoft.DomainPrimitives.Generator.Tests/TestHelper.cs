using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AltaSoft.DomainPrimitives.Generator.Tests
{
    public static class TestHelper
    {
        internal static Task Verify(string source, Action<ImmutableArray<Diagnostic>, List<string>, GeneratorDriver>? additionalChecks = null, DomainPrimitiveGlobalOptions? options = null)
        {
            List<Assembly> assemblies = [typeof(SwaggerGenOptions).Assembly, typeof(JsonSerializer).Assembly, typeof(OpenApiSchema).Assembly];
            var (diagnostics, output, driver) = TestHelpers.GetGeneratedOutput<DomainPrimitiveGenerator>(source, assemblies, options);

            Assert.Empty(diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error));
            additionalChecks?.Invoke(diagnostics, output, driver);

            return Verifier.Verify(driver).UseDirectory("Snapshots");
        }
    }
}

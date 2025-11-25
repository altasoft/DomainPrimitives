using AltaSoft.DomainPrimitives.Generator.Models;

namespace AltaSoft.DomainPrimitives.Generator.Tests;

public class GenerationFlagsTests
{
    [Fact]
    public Task NumericOperators_NotGenerated_WhenDisabled()
    {
        const string source = """
using System;
using AltaSoft.DomainPrimitives;

namespace AltaSoft.DomainPrimitives;

public readonly partial struct NumericTestValue : IDomainValue<double>
{
    public static PrimitiveValidationResult Validate(double value)
    {
        if (value == default)
            return "Invalid Value";

        return PrimitiveValidationResult.Ok;
    }
}

""";

        return TestHelper.Verify(source, (diagnostics, output, driver) =>
        {
            var all = string.Join('\n', output);
            Assert.DoesNotContain("operator +(", all);
            Assert.DoesNotContain("operator -(", all);
            Assert.DoesNotContain("operator *(", all);
            Assert.DoesNotContain("operator /(", all);
            Assert.DoesNotContain("operator %(", all);
        }, new DomainPrimitiveGlobalOptions
        {
            GenerateNumericOperators = false,
            GenerateOpenApiHelper = false
        });
    }

    [Fact]
    public Task ImplicitOperators_NotGeneratedForStructs_WhenDisabled()
    {
        const string source = """
using System;
using AltaSoft.DomainPrimitives;

namespace AltaSoft.DomainPrimitives;

public readonly partial struct ImplicitTestValue : IDomainValue<int>
{
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value == default)
            return "Invalid Value";

        return PrimitiveValidationResult.Ok;
    }
}

""";

        return TestHelper.Verify(source, (diagnostics, output, driver) =>
        {
            var all = string.Join('\n', output);
            Assert.DoesNotContain("implicit operator", all);
        }, new DomainPrimitiveGlobalOptions
        {
            GenerateOpenApiHelper = false,
            GenerateImplicitOperators = false,
        });
    }

    [Fact]
    public Task ImplicitOperators_NotGeneratedForClass_WhenDisabled()
    {
        const string source = """
using System;
using AltaSoft.DomainPrimitives;

namespace AltaSoft.DomainPrimitives;

public partial class StringClassValue : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "Invalid Value";

        return PrimitiveValidationResult.Ok;
    }
}

""";
        return TestHelper.Verify(source, (diagnostics, output, driver) =>
        {
            var all = string.Join('\n', output);
            Assert.DoesNotContain("implicit operator", all);
        }, new DomainPrimitiveGlobalOptions
        {
            GenerateOpenApiHelper = false,
            GenerateImplicitOperators = false,
        });
    }
}

using System.Collections.Immutable;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;

namespace AltaSoft.DomainPrimitives.Generator.Tests;

public class DomainPrimitiveGeneratorTest
{
    [Fact]
    public Task StringValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		                      using System;
		                      using System.Collections.Generic;
		                      using System.Linq;
		                      using System.Text;
		                      using System.Threading.Tasks;
		                      using AltaSoft.DomainPrimitives;

		                      namespace AltaSoft.DomainPrimitives;

		                      /// <inheritdoc/>
		                      public partial class StringValue : IDomainValue<string>
		                      {
		                          /// <inheritdoc/>
		                          public static PrimitiveValidationResult Validate(string value)
		                          {
		                              if (value=="Test")
		                                  return "Invalid Value";

		                              return PrimitiveValidationResult.Ok;
		                          }
		                      }
		                      """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task GuidValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct GuidValue : IDomainValue<Guid>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(Guid value)
		             	{
		             		if (value==default)
		             			return "Invalid Value";

		                    return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task BoolValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct BoolValue : IDomainValue<bool>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(bool value)
		             	{
		             		if (!value)
		             			return "Invalid Value";

		                    return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task SByteValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct SByteValue : IDomainValue<SByte>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(SByte value)
		             	{
		             		if (value < 10 || value > 20)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task ByteValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct ByteValue : IDomainValue<byte>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(byte value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task Int16Value_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct ShortValue : IDomainValue<short>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(short value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		                    return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task UInt16Value_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct UShortValue : IDomainValue<ushort>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(ushort value)
		             	{
		             		if (value > 100)
		             			return "Value must be between 10 and 100";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task IntValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct IntValue : IDomainValue<int>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(int value)
		             	{
		             		if (value < 10 || value > 20)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task UIntValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct IntValue : IDomainValue<uint>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(uint value)
		             	{
		             		if (value < 10 || value > 20)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task LongValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct LongValue : IDomainValue<long>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(long value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		                    return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task ULongValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct LongValue : IDomainValue<ulong>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(ulong value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task DecimalValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DecimalValue : IDomainValue<decimal>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(decimal value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task FloatValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct FloatValue : IDomainValue<float>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(float value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task DoubleValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DoubleValue : IDomainValue<double>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(double value)
		             	{
		             		if (value < 0)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }
		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task DateTimeValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DateTimeValue : IDomainValue<DateTime>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(DateTime value)
		             	{
		             		if (value == default)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }

		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task DateOnlyValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DateOnlyValue : IDomainValue<DateOnly>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(DateOnly value)
		             	{
		             		if (value == default)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }

		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task TimeOnlyValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct TimeOnlyValue : IDomainValue<TimeOnly>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(TimeOnly value)
		             	{
		             		if (value == default)
		             			return "Invalid Value";

		                    return PrimitiveValidationResult.Ok;
		             	}
		             }

		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task TimeSpanValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct TimeSpanValue : IDomainValue<TimeSpan>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(TimeSpan value)
		             	{
		             		if (value == default)
		             			return "Invalid Value";
		             		return PrimitiveValidationResult.Ok;
		             	}
		             }

		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task DateTimeOffsetValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DateTimeOffsetValue : IDomainValue<DateTimeOffset>
		             {
		             	/// <inheritdoc/>
		             	public static PrimitiveValidationResult Validate(DateTimeOffset value)
		             	{
		             		if (value == default)
		             			return "Invalid Value";

		             		return PrimitiveValidationResult.Ok;
		             	}
		             }

		             """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task CharValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
		                      using System;
		                      using System.Collections.Generic;
		                      using System.Linq;
		                      using System.Text;
		                      using System.Threading.Tasks;
		                      using AltaSoft.DomainPrimitives;

		                      namespace AltaSoft.DomainPrimitives;

		                      public readonly partial struct CharValue : IDomainValue<char>
		                      {
		                      	/// <inheritdoc/>
		                      	public static PrimitiveValidationResult Validate(char value)
		                      	{
		                      		if (value == default)
		                      			return "Invalid Value";

		                      		return PrimitiveValidationResult.Ok;
		                      	}
		                      }

		                      """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
    }

    [Fact]
    public Task StringOfStringValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
                              using System;
                              using System.Collections.Generic;
                              using System.Linq;
                              using System.Text;
                              using System.Threading.Tasks;
                              using AltaSoft.DomainPrimitives;

                              namespace AltaSoft.DomainPrimitives;

                              /// <inheritdoc/>
                              public partial class StringValue : IDomainValue<string>
                              {
                                  /// <inheritdoc/>
                                  public static PrimitiveValidationResult Validate(string value)
                                  {
                                      if (value=="Test")
                                          return "Invalid Value";

                                      return PrimitiveValidationResult.Ok;
                                  }
                              }

                              /// <inheritdoc/>
                              public partial class StringOfStringValue : IDomainValue<StringValue>
                              {
                                  /// <inheritdoc/>
                                  public static PrimitiveValidationResult Validate(StringValue value)
                                  {
                                      if (value=="Test")
                                          return "Invalid Value";

                                      return PrimitiveValidationResult.Ok;
                                  }
                              }
                              """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(7, x.Count));
    }

    [Fact]
    public Task IntOfIntValue_GeneratesAllInterfacesAndConverters()
    {
        const string source = """
                              using System;
                              using System.Collections.Generic;
                              using System.Linq;
                              using System.Text;
                              using System.Threading.Tasks;
                              using AltaSoft.DomainPrimitives;

                              namespace AltaSoft.DomainPrimitives;

                              /// <inheritdoc/>
                              public readonly partial struct IntValue : IDomainValue<int>
                              {
                              	/// <inheritdoc/>
                              	public static PrimitiveValidationResult Validate(int value)
                              	{
                              		if (value < 10 || value > 20)
                              			return "Invalid Value";

                              		return PrimitiveValidationResult.Ok;
                              	}
                              }

                              /// <inheritdoc/>
                              public readonly partial struct IntOfIntValue : IDomainValue<IntValue>
                              {
                                 	/// <inheritdoc/>
                                 	public static PrimitiveValidationResult Validate(IntValue value)
                                 	{
                                    		if (value < 10 || value > 20)
                                       			return "Invalid Value";

                                       		return PrimitiveValidationResult.Ok;
                                 	}
                              }
                              """;

        return TestHelper.Verify(source, (_, x, _) => Assert.Equal(7, x.Count));
    }

    public static class TestHelper
    {
        internal static Task Verify(string source, Action<ImmutableArray<Diagnostic>, List<string>, GeneratorDriver>? additionalChecks = null, DomainPrimitiveGlobalOptions? options = null)
        {
            var (diagnostics, output, driver) = TestHelpers.GetGeneratedOutput<DomainPrimitiveGenerator>(source, options);

            Assert.Empty(diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error));
            additionalChecks?.Invoke(diagnostics, output, driver);

            return Verifier.Verify(driver).UseDirectory("Snapshots");
        }
    }
}

# CLAUDE.md

Guidance for Claude Code when working in this repository.

## What this is

**AltaSoft.DomainPrimitives** is a C# toolkit built around a Roslyn incremental source generator. Consumers declare a `readonly partial struct`/`partial class` implementing `IDomainValue<T>` with a `Validate(T value)` method; the generator emits the constructor, `TryCreate`, operators, `IParsable<T>`, `IConvertible`, `JsonConverter`, `TypeConverter`, XML serialization, Swagger/OpenAPI mappings, etc. See `README.md` for the full user-facing feature set — keep it in sync with generator behavior when either changes.

## Solution layout

- `src/AltaSoft.DomainPrimitives` — core attributes, base types (`PrimitiveValidationResult`, `InvalidDomainValueException`, etc.) and the `*Extensions` flexible-parsing helpers (`DateOnlyExtensions`, `DateTimeExtensions`, `DateTimeOffsetExtensions`, `TimeOnlyExtensions`, `TimeSpanExtensions`).
- `src/AltaSoft.DomainPrimitives.Generator` — the incremental source generator itself (`DomainPrimitiveGenerator.cs`, `Executor.cs`, `Helpers/*`, `Models/*`).
- `src/AltaSoft.DomainPrimitives.XmlDataTypes` — ready-made XML Schema primitive types (`GDay`, `GMonth`, `GYear`, `NonEmptyString`, etc.) built on top of the generator.
- `src/AltaSoft.DomainPrimitives.SwaggerExtensions` / `src/AltaSoft.DomainPrimitives.OpenApiExtensions` — Swashbuckle and Microsoft.AspNetCore.OpenApi integration.
- `tests/AltaSoft.DomainPrimitives.UnitTests` — behavioral tests against generated code.
- `tests/AltaSoft.DomainPrimitives.Generator.Tests` — Roslyn generator snapshot tests using Verify (`*.verified.cs` files under `Snapshots/`).
- `tests/AltaSoft.DomainPrimitives.XmlDataTypes.Tests` — tests for the XML data types package.
- `Examples/` — a demo API project and standalone example domain types (`DomainPrimitivesDemo/`).

## Build & test

```
dotnet build AltaSoft.DomainPrimitives.sln
dotnet test AltaSoft.DomainPrimitives.sln
```

Target frameworks are `net8.0`, `net9.0`, `net10.0` (`Directory.Build.props`); `LangVersion` is `Latest`. When changing generator output, regenerate/update Verify snapshots by running the generator tests and accepting the new `.verified.cs` output — never hand-edit a `.verified.cs` file to match new code without confirming the generated output is actually correct.

Do not commit `bin/`/`obj/`/`.vs/` — `clean.bat` wipes them locally if needed.

## Conventions

- No explicit constructors, public properties, or public fields on `IDomainValue<T>` implementations — the generator owns those and will emit compiler errors on violations (this is by design, not a bug to "fix").
- Generated code lives in `*.g.cs`; never hand-edit generated output — change the generator instead.
- `SerializationFormatAttribute` and the `IsDateOrTime()`-gated codegen in `MethodGeneratorHelper.cs` are the two places that decide whether `Parse`/`TryParse` for date/time-backed primitives use plain `.Parse`/`.TryParse` vs. the flexible `ParseFlexible`/`TryParseFlexible` helpers — check both when touching date/time parsing behavior.
- Match existing XML doc style (`<summary>`, `<param>`, `<returns>`, `<inheritdoc/>` where applicable) — `GenerateDocumentationFile` is enabled and analyzers run at `AnalysisLevel=latest`.
- Follow this repo's own no-unnecessary-comments style; prefer expressive names over comments in new code.

## Documentation to keep aligned

When adding or changing user-facing generator behavior (new attributes, new supported underlying types, new generation flags, parsing behavior changes), update:
- `README.md` (`What's New`, the relevant feature section, and the table of contents)
- `EntityFrameworkCoreExample.md` if EF Core value-converter behavior is affected

# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Update GitHub Actions workflows to remove `net7.0` from build matrices and add `net10.0` where appropriate.
4. Upgrade src\AltaSoft.DomainPrimitives\AltaSoft.DomainPrimitives.csproj
5. Upgrade src\AltaSoft.DomainPrimitives.SwaggerExtensions\AltaSoft.DomainPrimitives.SwaggerExtensions.csproj
6. Upgrade src\AltaSoft.DomainPrimitives.XmlDataTypes\AltaSoft.DomainPrimitives.XmlDataTypes.csproj
7. Upgrade tests\AltaSoft.DomainPrimitives.UnitTests\AltaSoft.DomainPrimitives.UnitTests.csproj
8. Upgrade tests\AltaSoft.DomainPrimitives.Generator.Tests\AltaSoft.DomainPrimitives.Generator.Tests.csproj
9. Upgrade tests\AltaSoft.DomainPrimitives.XmlDataTypes.Tests\AltaSoft.DomainPrimitives.XmlDataTypes.Tests.csproj


## Settings

### Excluded projects

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|


### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### src\AltaSoft.DomainPrimitives\AltaSoft.DomainPrimitives.csproj modifications

Project properties changes:
  - Target frameworks should be changed from `net7.0;net8.0;net9.0` to `net8.0;net9.0;net10.0`

NuGet packages changes:
  - No NuGet package updates were suggested by analysis for this project.

Other changes:
  - Ensure any API breaking changes for .NET 10 are addressed if they appear during build/test.

#### src\AltaSoft.DomainPrimitives.SwaggerExtensions\AltaSoft.DomainPrimitives.SwaggerExtensions.csproj modifications

Project properties changes:
  - Target frameworks should be changed from `net7.0;net8.0;net9.0` to `net8.0;net9.0;net10.0`

NuGet packages changes:
  - No NuGet package updates were suggested by analysis for this project.

Other changes:
  - Verify compatibility of any build targets or imported MSBuild props/targets with .NET 10.

#### src\AltaSoft.DomainPrimitives.XmlDataTypes\AltaSoft.DomainPrimitives.XmlDataTypes.csproj modifications

Project properties changes:
  - Target frameworks should be changed from `net7.0;net8.0;net9.0` to `net8.0;net9.0;net10.0`

NuGet packages changes:
  - No NuGet package updates were suggested by analysis for this project.

Other changes:
  - Confirm XML-related APIs behave the same under .NET 10 during tests.

#### tests\AltaSoft.DomainPrimitives.UnitTests\AltaSoft.DomainPrimitives.UnitTests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - No NuGet package updates were suggested by analysis for this project.

Other changes:
  - Run unit tests after framework change and fix any test regressions.

#### tests\AltaSoft.DomainPrimitives.Generator.Tests\AltaSoft.DomainPrimitives.Generator.Tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - No NuGet package updates were suggested by analysis for this project.

Other changes:
  - Run unit tests after framework change and fix any test regressions.

#### tests\AltaSoft.DomainPrimitives.XmlDataTypes.Tests\AltaSoft.DomainPrimitives.XmlDataTypes.Tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - No NuGet package updates were suggested by analysis for this project.

Other changes:
  - Run unit tests after framework change and fix any test regressions.

### CI / GitHub Actions changes

Changes required in GitHub Actions workflows:
  - Update any workflow `matrix` entries that list target frameworks to remove `net7.0` and include `net10.0`.
  - Ensure actions that install the SDK (`actions/setup-dotnet`) request an SDK that supports .NET 10 or use the version range that includes preview SDK when necessary.
  - Update job names and any caching keys that include framework names if present.
  - Run CI against `net8.0`, `net9.0`, and `net10.0`.

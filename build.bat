del D:\NUGET\*.nupkg

REM Generator
pushd .

cd src\AltaSoft.DomainPrimitives.Generator
dotnet build -c release

popd

REM 
pushd .

cd src\AltaSoft.DomainPrimitives
dotnet build -c release

popd

REM XmlDataTypes
pushd .

cd src\AltaSoft.DomainPrimitives.XmlDataTypes
dotnet build -c release

popd


nuget pack AltaSoft.DomainPrimitives.nuspec


xcopy *.nupkg d:\nuget\*.*

del *.nupkg


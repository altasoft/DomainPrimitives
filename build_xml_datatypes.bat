REM del D:\NUGET\*.nupkg

REM XmlDataTypes
pushd .

cd src\AltaSoft.DomainPrimitives.XmlDataTypes
dotnet build -c release

popd


nuget pack AltaSoft.DomainPrimitives.XmlDataTypes.nuspec


xcopy *.nupkg d:\nuget\*.*

del *.nupkg


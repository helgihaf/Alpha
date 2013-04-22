rem Arg1 = ProjectDir
xcopy %1bin\Debug\*.dll c:\tools\ds /i /y
xcopy %1bin\Debug\DataSchemaBuild.exe c:\tools\ds /i /y
xcopy %1CommonColumnTypes.xml c:\tools\ds /i /y
xcopy %1..\..\Knightrunner.Library.Database.Schema\Parsing\*.xsd c:\tools\ds /i /y
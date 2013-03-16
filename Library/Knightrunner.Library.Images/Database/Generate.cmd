@echo off
C:\dev\Knightrunner\Library\Tools\DataSchema\bin\Debug\dataschema DataSchema.xml /scriptTarget:MSSQL /scriptOut:Scripts\Create.SQL /scriptDoc- /codeTarget:DotNet /codeOut:Images.dbml /tableSchema:dbo
if %errorlevel% == 0 goto genclass
if %errorlevel% == 4 goto genclass
goto end
:genclass
sqlmetal /namespace:Knightrunner.Library.Images.Database /code:Images.designer.cs /language:csharp Images.dbml
:end

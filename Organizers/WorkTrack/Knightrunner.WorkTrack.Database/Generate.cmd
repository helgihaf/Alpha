@echo off
C:\dev\Knightrunner\Library\bin\Debug\dataschema DataSchema.xml /scriptTarget:MSSQL /scriptOut:Scripts\Create\1.0.0.0\001-Create.SQL /scriptDoc- /codeTarget:DotNet /codeOut:WorkTrack.dbml /tableSchema:dbo /docOut:Documents\WorkTrack.html
if %errorlevel% == 0 goto genclass
if %errorlevel% == 4 goto genclass
goto end
:genclass
sqlmetal /namespace:Knightrunner.WorkTrack.Database /code:WorkTrack.designer.cs /language:csharp WorkTrack.dbml
:end

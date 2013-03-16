using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Core;
using System.Reflection;
using Knightrunner.Library.Database.Schema;
using System.IO;
using Knightrunner.Library.Database.Schema.Verification;
using Knightrunner.Library.Database.Schema.SqlServer;
using Knightrunner.Library.Database.Schema.Linq;
using Knightrunner.Library.Database.Schema.Documentation;
using Knightrunner.Library.Database.Schema.Oracle;

namespace DataSchemaTool
{
    class Program
    {
        private const string programName = "Knightrunner DataSchema";
        private static CommandLineParameter inputFilesParam;
        private static CommandLineParameter scriptTargetParam;
        private static CommandLineParameter scriptOutParam;
        private static CommandLineParameter scriptDocParam;
        private static CommandLineParameter codeTargetParam;
        private static CommandLineParameter codeOutParam;
        private static CommandLineParameter tableSchemaNameParam;
        private static CommandLineParameter docOutParam;
        private static CommandLineParameter docCssFileParam;
        private static CommandLineParameter nameParam;
        private static string[] description = new string[]
        {
            "Reads a Data Schema XML file and generates SQL scripts and",
            "other derived files.",
        };

        private const int returnCodeOk = 0;
        private const int returnCodeArguments = 1;
        private const int returnCodeDataSchema = 2;
        private const int returnCodeNoCodeFile = 3;
        private const int returnCodeNoScriptFile = 4;
        private const int returnCodeNoFile = 5;

        static int Main(string[] args)
        {
            ShowVersion();

            CommandLineProcessor clp = InitializeCommandLine();
            try
            {
                clp.ProcessArguments(args);
            }
            catch (CommandLineProcessorException ex)
            {
                WriteError(ex.Message);
                return returnCodeArguments;
            }

            if (clp.ShowUsageRequested)
            {
                clp.ShowUsage(description);
                return returnCodeArguments;
            }

            DataSchema dataSchema = new DataSchema();
            dataSchema.Name = nameParam.StringValue;
            VerificationContext context = new VerificationContext();

            foreach (var filePath in inputFilesParam.StringValues)
            {
                var inputFilePath = Path.GetFullPath(filePath);

                if (!File.Exists(inputFilePath))
                {
                    WriteError("Input file \"" + inputFilePath + "\" was not found.");
                    return returnCodeArguments;
                }

                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    dataSchema.LoadDataSchemaFile(reader, context);
                }
            }

            dataSchema.Verify(context);
            var entries = context.Entries;
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }

            if (entries.Count != 0)
            {
                WriteWarning("No script file written.");
                return returnCodeDataSchema;
            }

            bool scriptFileWritten = false;
            bool codeFileWritten = false;

            //
            // Write SQL script
            //

            if (scriptTargetParam.StringValue != null)
            {
                if (string.IsNullOrWhiteSpace(scriptOutParam.StringValue))
                {
                    WriteError("Invalid or missing value for " + scriptOutParam.Name);
                    return returnCodeArguments;
                }
                string outputFilePath = Path.GetFullPath(scriptOutParam.StringValue);
                FileInfo fileInfo = new FileInfo(outputFilePath);
                if (!fileInfo.Exists || !fileInfo.IsReadOnly)
                {
                    TargetSystem targetSystem = GetTargetSystem(dataSchema, scriptTargetParam.StringValue);
                    if (targetSystem == null)
                        return returnCodeDataSchema;        // Error msg already shown

                    targetSystem = dataSchema.TargetSystems[scriptTargetParam.StringValue];
                    if (targetSystem == null)
                    {
                        WriteError("Unknown script target " + scriptTargetParam.StringValue);
                        return returnCodeDataSchema;
                    }

                    if (!fileInfo.Directory.Exists)
                    {
                        WriteError("Output directory '" + fileInfo.Directory + "' does not exist");
                        return returnCodeArguments;
                    }

                    Console.WriteLine("Generating script...");
                    //SqlServerScriptGenerator gen = new SqlServerScriptGenerator();
                    var gen = new OracleScriptGenerator();
                    gen.TargetSystem = targetSystem;
                    gen.DataSchema = dataSchema;

                    //if (scriptDocParam.SwitchValue)
                    //{
                    //    gen.ScriptDocumentGenerator = new LSRetailScriptDocumentGenerator();
                    //}
                    using (FileStream stream = new FileStream(outputFilePath, FileMode.Create))
                    {
                        gen.Generate(stream);
                    }
                    scriptFileWritten = true;
                    Console.WriteLine("Wrote script file \"" + outputFilePath + "\"");
                }
                else
                {
                    WriteError("Output file is read only (" + outputFilePath + ")");
                }
            }

            //
            // Write code
            //

            if (codeTargetParam.StringValue != null)
            {
                if (string.IsNullOrWhiteSpace(codeOutParam.StringValue))
                {
                    WriteError("Invalid or missing value for " + codeOutParam.Name);
                    return returnCodeArguments;
                }
                string outputFilePath = Path.GetFullPath(codeOutParam.StringValue);
                FileInfo fileInfo = new FileInfo(outputFilePath);
                if (!fileInfo.Exists || !fileInfo.IsReadOnly)
                {
                    TargetSystem targetSystem = GetTargetSystem(dataSchema, codeTargetParam.StringValue);
                    if (targetSystem == null)
                        return returnCodeDataSchema;    // Error msg already shown

                    TargetSystem databaseTargetSystem = GetTargetSystem(dataSchema, scriptTargetParam.StringValue);
                    if (databaseTargetSystem == null)
                        return returnCodeDataSchema;

                    Console.WriteLine("Generating dbml...");
                    LinqDbmlGenerator linqGen = new LinqDbmlGenerator();
                    linqGen.DataSchema = dataSchema;
                    linqGen.TargetSystem = targetSystem;
                    linqGen.DatabaseTargetSystem = databaseTargetSystem;
                    linqGen.DatabaseColumnTypeMapper = new SqlServerColumnTypeMapper();
                    using (FileStream stream = new FileStream(outputFilePath, FileMode.Create))
                    {
                        linqGen.Generate(stream);
                    }
                    codeFileWritten = true;
                    Console.WriteLine("Wrote code file \"" + outputFilePath + "\"");
                }
                else
                {
                    WriteError("Output file is read only (" + outputFilePath + ")");
                }
            }

            //
            // Write documentation
            //

            if (docOutParam.StringValue != null)
            {
                if (string.IsNullOrWhiteSpace(docOutParam.StringValue))
                {
                    WriteError("Invalid value for " + docOutParam.Name);
                    return returnCodeArguments;
                }
                string outputFilePath = Path.GetFullPath(docOutParam.StringValue);
                FileInfo fileInfo = new FileInfo(outputFilePath);
                if (!fileInfo.Exists || !fileInfo.IsReadOnly)
                {
                    TargetSystem databaseTargetSystem = GetTargetSystem(dataSchema, scriptTargetParam.StringValue);
                    if (databaseTargetSystem == null)
                        return returnCodeDataSchema;

                    Console.WriteLine("Generating documentation...");
                    DocumentGenerator docGen = new DocumentGenerator();
                    docGen.DataSchema = dataSchema;
                    docGen.DatabaseTargetSystem = databaseTargetSystem;
                    docGen.DatabaseColumnTypeMapper = new SqlServerColumnTypeMapper();
                    if (!string.IsNullOrWhiteSpace(tableSchemaNameParam.StringValue))
                    {
                        docGen.TableSchemaName = tableSchemaNameParam.StringValue;
                    }
                    if (docCssFileParam.StringValue != null)
                    {
                        docGen.CssFile = docCssFileParam.StringValue;
                    }
                    using (FileStream stream = new FileStream(outputFilePath, FileMode.Create))
                    {
                        docGen.Generate(stream);
                    }
                    Console.WriteLine("Wrote document file \"" + outputFilePath + "\"");
                }
                else
                {
                    WriteError("Output file is read only (" + outputFilePath + ")");
                }
            }

            if (scriptFileWritten && !codeFileWritten)
                return returnCodeNoCodeFile;
            else if (!scriptFileWritten && codeFileWritten)
                return returnCodeNoScriptFile;
            else if (!scriptFileWritten && !codeFileWritten)
                return returnCodeNoFile;
            else
                return returnCodeOk;
        }

        private static TargetSystem GetTargetSystem(DataSchema dataSchema, string targetName)
        {
            TargetSystem targetSystem = dataSchema.TargetSystems[targetName];

            if (targetSystem == null)
            {
                WriteError("Unknown script target " + targetName);
            }

            return targetSystem;
        }

        private static void ShowVersion()
        {
            Console.WriteLine(programName + ", version " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine("Copyright (C) 2010-2011 Helgi Hafþórsson. All rights reserved.");
            Console.Out.Flush();
        }

        private static CommandLineProcessor InitializeCommandLine()
        {
            CommandLineProcessor clp = new CommandLineProcessor();

            // DataSchema.exe Data.xml /scriptTarget:MSSQL /scriptOut:Data.sql /tableSchema:dbo /codeTarget:DotNet /codeOut:Data.dbml
            inputFilesParam = new CommandLineParameter
            (
                true,
                "inputFiles",
                true,
                "Input file containing data schema"
            );
            clp.CommandLineParameters.Add(inputFilesParam);

            scriptTargetParam = new CommandLineParameter
            (
                "scriptTarget",
                false,
                CommandLineValueOption.One,
                "target",
                false,
                "Script target system, e.g. MSSQL"
            );
            clp.CommandLineParameters.Add(scriptTargetParam);

            scriptOutParam = new CommandLineParameter
            (
                "scriptOut",
                false,
                CommandLineValueOption.One,
                "filePath",
                false,
                "Output file containing script"
            );
            clp.CommandLineParameters.Add(scriptOutParam);

            scriptDocParam = new CommandLineParameter
            (
                "scriptDoc",
                false,
                CommandLineValueOption.Never,
                true,
                "Generate script documentiation statements"
            );
            clp.CommandLineParameters.Add(scriptDocParam);

            tableSchemaNameParam = new CommandLineParameter
            (
                "tableSchema",
                false,
                CommandLineValueOption.One,
                "name",
                false,
                "Name of database schema for tables, e.g. \"dbo\""
            );
            clp.CommandLineParameters.Add(tableSchemaNameParam);

            codeTargetParam = new CommandLineParameter
            (
                "codeTarget",
                false,
                CommandLineValueOption.One,
                "target",
                false,
                "Code target system, e.g. DotNet"
            );
            clp.CommandLineParameters.Add(codeTargetParam);

            codeOutParam = new CommandLineParameter
            (
                "codeOut",
                false,
                CommandLineValueOption.One,
                "filePath",
                false,
                "Output file containing code"
            );
            clp.CommandLineParameters.Add(codeOutParam);

            docOutParam = new CommandLineParameter
            (
                "docOut",
                false,
                CommandLineValueOption.One,
                false,
                "Output file for documentation"
            );
            clp.CommandLineParameters.Add(docOutParam);

            docCssFileParam = new CommandLineParameter
            (
                "docCssFile",
                false,
                CommandLineValueOption.One,
                false,
                "An optional CSS file to embed in the output file for documentation"
            );
            clp.CommandLineParameters.Add(docCssFileParam);

            nameParam = new CommandLineParameter
            (
                "name",
                true,
                CommandLineValueOption.One,
                false,
                "The name of the database schema"
            );
            clp.CommandLineParameters.Add(nameParam);

            clp.AutoCheckForMissingArguments = true;
            return clp;
        }


        private static void WriteWarning(string msg)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + msg);
            Console.ForegroundColor = foregroundColor;
        }


        private static void WriteError(string msg)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + msg);
            Console.ForegroundColor = foregroundColor;
        }

    }
}

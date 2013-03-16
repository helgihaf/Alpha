using Knightrunner.Library.Database.Schema;
using Knightrunner.Library.Database.Schema.Oracle;
using Knightrunner.Library.Database.Schema.PetaPoco;
using Knightrunner.Library.Database.Schema.Project;
using Knightrunner.Library.Database.Schema.SqlServer;
using Knightrunner.Library.Database.Schema.Verification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataSchemaBuild
{
    class Program : ISchemaTransformationFactory, IScriptDocumentGeneratorFactory
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Knightrunner DataSchemaBuild version " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine("Copyright (C) Helgi Hafthorsson. All rights reserved.");

            var program = new Program();
            program.Run(args);
        }

        private void Run(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Error: No input file specified.");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.Error.WriteLine("Input file not found.");
                return;
            }
            var project = new DataSchemaProject();
            var context = new VerificationContext();
            project.LoadFrom(args[0], context, this);
            ShowMessages(context);

            if (context.Entries.Count != 0)
            {
                WriteWarning("No script file written.");
                return;
            }

            context.Clear();

            project.Build(context, this);
            ShowMessages(context);
        }

        private void ShowMessages(VerificationContext context)
        {
            var entries = context.Entries;
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
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


        ISchemaTransformation ISchemaTransformationFactory.Create(string name)
        {
            switch (name)
            {
                case "Oracle":
                    return new OracleSchemaTransformation();
                case "MSSQL":
                    return new SqlServerSchemaTransformation();
                case "PetaPoco":
                    return new PetaPocoTransformation();
                default:
                    throw new ArgumentException("Unknown schema transformation name " + name);
            }
        }

        IScriptDocumentGenerator IScriptDocumentGeneratorFactory.Create(string name)
        {
            switch (name)
            {
                case "Oracle":
                    return new OracleScriptDocumentGenerator();
                case "LSRetail":
                    return new LSRetailScriptDocumentGenerator();
                default:
                    throw new ArgumentException("Unknown script document generator factory name " + name);
            }
        }
    }
}

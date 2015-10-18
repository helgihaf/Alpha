// Copyright (c) 2015 Marson Software
// All rights reserved.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Marson.DevTools
{
    [Cmdlet(VerbsData.Update, "AssemblyInfo")]
    public class UpdateAssemblyInfo : Cmdlet
    {
        [Parameter]
        public string BaseDir { get; set; }

        [Parameter]
        public string AssemblyCompany { get; set; }

        [Parameter]
        public string AssemblyProduct { get; set; }

        [Parameter]
        public string AssemblyCopyright { get; set; }

        [Parameter]
        public string AssemblyTrademark { get; set; }

        [Parameter]
        public string AssemblyVersion { get; set; }

        [Parameter]
        public string AssemblyFileVersion { get; set; }

        [Parameter(HelpMessage = "Confirm every file change")]
        public SwitchParameter Confirm { get; set; }

        [Parameter]
        public string FileSearchPattern { get; set; } = "AssemblyInfo.cs";

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (string.IsNullOrEmpty(BaseDir))
            {
                BaseDir = Environment.CurrentDirectory;
            }

            foreach (var filePath in Directory.EnumerateFiles(BaseDir, FileSearchPattern, SearchOption.AllDirectories))
            {
                if (ShouldProcess(filePath))
                {
                    if (!Confirm || ShouldContinue("", ""))
                    {
                        WriteVerbose("Updating " + filePath);
                        UpdateFile(filePath);
                        WriteVerbose("Done");
                    }
                }
            }
        }

        private void UpdateFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            using (StreamWriter writer = new StreamWriter(filePath + ".out"))
            {
                String line;

                while ((line = reader.ReadLine()) != null)
                {
                    line = ProcessLine(line);
                    writer.WriteLine(line);
                }
            }

            File.Delete(filePath);
            File.Move(filePath + ".out", filePath);
        }


        private string ProcessLine(string line)
        {
            line = ProcessLinePart(line, nameof(AssemblyCompany), AssemblyCompany);
            line = ProcessLinePart(line, nameof(AssemblyProduct), AssemblyProduct);
            line = ProcessLinePart(line, nameof(AssemblyCopyright), AssemblyCopyright);
            line = ProcessLinePart(line, nameof(AssemblyTrademark), AssemblyTrademark);
            line = ProcessLinePart(line, nameof(AssemblyVersion), AssemblyVersion);
            line = ProcessLinePart(line, nameof(AssemblyFileVersion), AssemblyFileVersion);
            return line;
        }

        private string ProcessLinePart(string line, string attributeName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return line;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                return line;
            }

            if (line.Trim().StartsWith("//"))
            {
                return line;
            }

            const string assemblySearch = "[assembly:";
            int index = line.IndexOf(assemblySearch);
            if (index == -1)
            {
                return line;
            }

            string attributeSearch = attributeName + "(\"";
            index = line.IndexOf(attributeSearch, index + assemblySearch.Length);
            if (index == -1)
            {
                return line;
            }

            int endIndex = line.IndexOf("\")", index + attributeSearch.Length);
            if (index == -1)
            {
                return line;
            }

            return line.Substring(0, index + attributeSearch.Length) + value + line.Substring(endIndex);
        }
    }
}


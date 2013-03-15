using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    public class ScriptStore
    {
        public List<Script> CreateScripts { get; private set; }
        public List<Script> UpdateScripts { get; private set; }
        public List<Script> FinalizeScripts { get; private set; }

        public ScriptStore()
        {
            CreateScripts = new List<Script>();
            UpdateScripts = new List<Script>();
            FinalizeScripts = new List<Script>();
        }


        internal Version CreateScriptVersion { get; private set; }
        internal Version MaxUpdateVersion { get; private set; }

        internal void CalculateAggregations()
        {
            CreateScriptVersion = null;
            MaxUpdateVersion = null;

            foreach (var script in CreateScripts)
            {
                if (CreateScriptVersion == null)
                {
                    CreateScriptVersion = script.Version;
                }
                else if (CreateScriptVersion != script.Version)
                {
                    throw new ScriptVersionException("Create scripts have more than one version.");
                }
            }

            foreach (var script in UpdateScripts)
            {
                if (MaxUpdateVersion == null)
                {
                    MaxUpdateVersion = script.Version;
                }
                else if (MaxUpdateVersion < script.Version)
                {
                    MaxUpdateVersion = script.Version;
                }
                else if (MaxUpdateVersion > script.Version)
                {
                    throw new ScriptVersionException("Update scripts are not ordered by version number.");
                }
            }

        }
    }
}

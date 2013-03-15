using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    public class DatabaseUpdater
    {
        // Set in constructor
        private IDatabaseVersionStore versionStore;
        private IScriptRunner scriptRunner;
        private ScriptStore scripts;
        
        // Work

        
        public DatabaseUpdater(IDatabaseVersionStore versionStore, IScriptRunner scriptRunner, ScriptStore scripts)
        {
            if (versionStore == null)
            {
                throw new ArgumentNullException("versionStore");
            }

            if (scriptRunner == null)
            {
                throw new ArgumentNullException("scriptRunner");
            }

            if (scripts == null)
            {
                throw new ArgumentNullException("scripts");
            }

            scripts.CalculateAggregations();

            // Accept arguments
            this.versionStore = versionStore;
            this.scriptRunner = scriptRunner;
            this.scripts = scripts;

        }


        public UpdateType CheckUpdate()
        {
            return CheckUpdate(versionStore.Version);
        }


        private UpdateType CheckUpdate(Version targetDatabaseVersion)
        {
            if (targetDatabaseVersion == null)
            {
                // No current database version: Tables are missing so we signal a NoDatabase update.
                return UpdateType.NoDatabase;
            }

            if (targetDatabaseVersion < scripts.CreateScriptVersion)
            {
                // Target database is too old for this update; the create script available was created after target database was last updated.
                return UpdateType.Obsolete;
            }


            if (targetDatabaseVersion < scripts.MaxUpdateVersion)
            {
                // Target database is older than the script so we signal an update.
                return UpdateType.Update;
            }
            else if (targetDatabaseVersion > scripts.MaxUpdateVersion)
            {
                // Target database is NEWER than the scripts so wi signal a downgrade.
                return UpdateType.Downgrade;
            }
            else
            {
                // Database and scripts are equal. No update.
                return UpdateType.None;
            }
        }

        
        public void UpdateDatabase()
        {
            var targetDatabaseVersion = versionStore.Version;
            var updateType = CheckUpdate(targetDatabaseVersion);

            switch (updateType)
            {
                case UpdateType.None:
                    return;
                case UpdateType.NoDatabase:
                    RunCreateScripts();
                    RunUpdateAndFinalizeScripts(targetDatabaseVersion);
                    break;
                case UpdateType.Update:
                    RunUpdateAndFinalizeScripts(targetDatabaseVersion);
                    break;
                case UpdateType.Downgrade:
                    throw new InvalidOperationException("Target database needs to be downgraded.");
                case UpdateType.Obsolete:
                    throw new InvalidOperationException("Target database is obsolete.");
                default:
                    throw new NotImplementedException("Implementation missing for UpdateType." + updateType.ToString());
            }
        }

        private void RunCreateScripts()
        {
            foreach (var script in scripts.CreateScripts)
            {
                scriptRunner.RunScript(script);
            }
        }

        private void RunUpdateAndFinalizeScripts(Version targetDatabaseVersion)
        {
            foreach (var script in scripts.UpdateScripts)
            {
                if (script.Version > targetDatabaseVersion)
                {
                    scriptRunner.RunScript(script);
                }
            }

            foreach (var script in scripts.FinalizeScripts)
            {
                scriptRunner.RunScript(script);
            }

        }
    }
}

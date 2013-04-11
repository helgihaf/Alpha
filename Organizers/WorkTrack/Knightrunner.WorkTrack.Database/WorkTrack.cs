using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace Knightrunner.WorkTrack.Database
{
    //partial class Project : LinqEntityBase
    //{
    //    public override void Detach()
    //    {
    //        if (null == PropertyChanging)
    //            return;

    //        PropertyChanging = null;
    //        PropertyChanged = null;

    //        this._Activities = Detach(this._Activities, attach_Activities, detach_Activities);
    //    }
    //}


    //partial class Activity : LinqEntityBase
    //{
    //    public override void Detach()
    //    {
    //        if (null == PropertyChanging)
    //            return;

    //        PropertyChanging = null;
    //        PropertyChanged = null;

    //        this._ProjectEntity = Detach(this._ProjectEntity);
    //    }
    //}

    //partial class User : LinqEntityBase
    //{
    //    public override void Detach()
    //    {
    //        if (null == PropertyChanging)
    //            return;

    //        PropertyChanging = null;
    //        PropertyChanged = null;
    //    }
    //}

    //partial class WorkEntry : LinqEntityBase
    //{
    //    public override void Detach()
    //    {
    //        if (null == PropertyChanging)
    //            return;

    //        PropertyChanging = null;
    //        PropertyChanged = null;

    //        this._ProjectEntity = Detach(this._ProjectEntity);
    //        this._ActivityEntity = Detach(this._ActivityEntity);
    //    }
    //}


    public partial class WorkTrackDataContext
    {
        private HashSet<string> projectNames;
        private HashSet<string> projectExternalCodes;

        /// <summary>
        /// Adds a new work entry, closing any open work entries for the user. Data is not submitted.
        /// </summary>
        /// <param name="entry"></param>
        public void AddWorkEntry(WorkEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            if (entry.Id == default(Guid))
            {
                throw new ArgumentException("Invalid user ID on entry");
            }

            if (entry.User == default(Guid))
            {
                throw new ArgumentException("Invalid user ID on entry");
            }

            if (entry.Start == default(DateTime))
            {
                throw new ArgumentException("Invalid start DateTime on entry");
            }

            // Close all open entries
            var openEntries =
                from openEntry in this.WorkEntries
                where openEntry.User == entry.User && openEntry.End == null
                select openEntry;

            foreach (var openEntry in openEntries)
            {
                openEntry.End = entry.Start;
            }

            // Add the new one
            this.WorkEntries.InsertOnSubmit(entry);
        }


        public bool ProjectNameExists(string name)
        {
            if (projectNames == null)
            {
                projectNames = new HashSet<string>
                (
                    (from project in Projects
                     where project.Active
                     select project.Name).Distinct()
                );
            }

            return projectNames.Contains(name);
        }


        public bool ProjectExternalCodeExists(string externalCode)
        {
            if (projectExternalCodes == null)
            {
                projectExternalCodes = new HashSet<string>
                (
                    (from project in Projects
                     where project.Active && project.ExternalCode != null
                     select project.ExternalCode).Distinct()
                );
            }

            return projectExternalCodes.Contains(externalCode);
        }
    }
}

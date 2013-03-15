using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    /// <summary>
    /// The type of update needed to a target database.
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// No update needed.
        /// </summary>
        None,

        /// <summary>
        /// Target database tables are missing, a fresh create of tables is needed.
        /// </summary>
        NoDatabase,

        /// <summary>
        /// An update is needed.
        /// </summary>
        Update,

        /// <summary>
        /// Downgrade, target database is newer than the script files.
        /// </summary>
        Downgrade,

        /// <summary>
        /// Target database is too old for this update; the create script available was created after target database was last updated.
        /// </summary>
        Obsolete,
    }
}

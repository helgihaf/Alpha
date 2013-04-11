using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace WorkTrackLite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settings = Properties.Settings.Default;
            if (string.IsNullOrEmpty(settings.Language))
            {
                settings.Language = Thread.CurrentThread.CurrentCulture.Name;
                settings.Save();
            }
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(settings.Language);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CustomApplicationContext());
        }
    }
}

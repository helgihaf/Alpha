using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WorkTrackWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        
        public App() 
        {
            Startup += new StartupEventHandler(App_Startup); 
        }                
        
        private void App_Startup(object sender, StartupEventArgs e)                
        {                        
            //Setup icon                        
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Visible = true;               
        }
    }
}

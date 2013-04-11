using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Knightrunner.WorkTrack.Wpf;
using Knightrunner.WorkTrack.Database;

namespace WorkTrackWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISessionContext
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var window = new TrackWorkSimpleWindow(this);
            window.Show();
            
        }

        Guid ISessionContext.UserId
        {
            get { throw new NotImplementedException(); }
        }

        WorkTrackDataContext ISessionContext.CreateDataContext()
        {
            throw new NotImplementedException();
        }
    }
}

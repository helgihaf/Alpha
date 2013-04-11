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
using System.Windows.Shapes;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.Wpf
{
    /// <summary>
    /// Interaction logic for TrackWorkSimpleWindow.xaml
    /// </summary>
    public partial class TrackWorkSimpleWindow : Window
    {
        private ISessionContext sessionContext;
        private WorkTrackDataContext dataContext;

        private TrackWorkSimpleWindow()
        {
            InitializeComponent();
        }

        public TrackWorkSimpleWindow(ISessionContext sessionContext)
            : this()
        {
            this.sessionContext = sessionContext;
            dataContext = sessionContext.CreateDataContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sessionContext == null)
            {
                return;
            }

            using (new BusyScope(this))
            {
                comboBoxText.Items.Clear();
                var entries =
                    (from entry in dataContext.WorkEntries
                     where entry.User == sessionContext.UserId
                     orderby entry.Start descending
                     select entry.Text).Distinct().Take(10);

                foreach (var entry in entries)
                {
                    comboBoxText.Items.Add(entry);
                }

                comboBoxText.Focus();
                comboBoxText.IsEditable = true;
            }
            UpdateActions();
        }

        private void UpdateActions()
        {
            buttonOk.IsEnabled = comboBoxText.Text.Length > 0;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            using (new BusyScope(this, buttonOk, buttonCancel))
            {
                WorkEntry entry = new WorkEntry
                {
                    Id = Guid.NewGuid(),
                    User = sessionContext.UserId,
                    Start = DateTime.Now,
                    Text = comboBoxText.Text
                };
                dataContext.AddWorkEntry(entry);
                dataContext.SubmitChanges();
                Close();
            }
        }
    }
}

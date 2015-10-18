using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marson.LogEyes.Client.WinForms
{
    public partial class MainForm : Form
    {
        private LogFile logFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        //private void CreateLineIndexes(string fileName)
        //{
        //    var list = new List<int>();
        //    using (var reader = new StreamReader(fileName))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            list.Add(line.Length);
        //        }
        //    }
        //    MessageBox.Show(list.Count.ToString());
        //}

        private void OpenFile(string fileName)
        {
            if (logFile != null)
            {
                logFile.Dispose();
            }
            logFile = new LogFile(fileName);
            logControl.LogFile = logFile;
            ShowTail();
        }

        private void ShowTail()
        {
            logControl.ShowTail();
        }
    }
}

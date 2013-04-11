using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;
using Knightrunner.WorkTrack.UI;

namespace TestUIClient
{
    public partial class Form1 : Form, ISessionContext
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public WorkTrackDataContext CreateDataContext()
        {
            return new WorkTrackDataContext("Data Source=lap-helgih2;Initial Catalog=WorkTrack;Integrated Security=SSPI");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseLoginDialog dialog = new DatabaseLoginDialog();
            dialog.ConnectionInfo = new DatabaseConnectionInfo
            {
                ServerName = "(local)",
                DatabaseName = "WorkTrack"
            };
            
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = dialog.ConnectionInfo.ConnectionString;
            }
        }


            //var symmetric =
            //    new Knightrunner.Library.Cryptography.Symmetric();

            //var key = symmetric.CreateSecretKey();
            //for (int i = 0; i < key.Length; i++)
            //{
            //    if (i > 0)
            //    {
            //        textBox1.AppendText(", ");
            //    }
            //    textBox1.AppendText(string.Format("0x{0:x}", key[i]));
            //}

    }
}

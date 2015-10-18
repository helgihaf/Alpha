using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marson.LogEyes.Client.WinForms
{
    public partial class LogControl : UserControl
    {
        private LogFile logFile;

        public LogControl()
        {
            InitializeComponent();
        }

        internal void ShowTail()
        {
            textBox.Lines = logFile.GetTailLines().ToArray();
            textBox.Select(textBox.TextLength - 1, 0);
            textBox.ScrollToCaret();
        }

        private int GetNumberOfVisibleLines()
        {
            return textBox.ClientSize.Height / (textBox.Font.Height - 1);
        }

        public LogFile LogFile
        {
            get
            {
                return logFile;
            }
            set
            {
                logFile = value;
                textBox.Clear();
            }
        }

    }
}

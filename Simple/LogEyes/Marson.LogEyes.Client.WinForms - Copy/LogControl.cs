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
            int numLinesDisplayed = GetNumberOfVisibleLines();
            var lines = logFile.GetTailLines(numLinesDisplayed).ToArray();
            int index = lines.Length - numLinesDisplayed;
            if (index < 0)
            {
                index = 0;
            }

            textBox.Clear();
            for (; index < lines.Length; index++)
            {
                textBox.AppendText(lines[index] + Environment.NewLine);
            }
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
                vScrollBar1.Maximum = Convert.ToInt32(logFile.LineCount);
            }
        }

    }
}

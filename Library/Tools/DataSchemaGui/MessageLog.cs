using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataSchemaGui
{
    public partial class MessageLog : UserControl
    {
        public MessageLog()
        {
            InitializeComponent();
        }

        public void WriteLine(string line)
        {
            textBoxLog.AppendText(line + Environment.NewLine);
        }


        public void Write(string message)
        {
            textBoxLog.AppendText(message);
        }


        public void Clear()
        {
            textBoxLog.Clear();
        }
    }
}

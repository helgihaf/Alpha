using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Knightrunner.WorkTrack.UI.Controls
{
    public partial class DateTimeEditControl : UserControl
    {
        private bool suppressChangeEvent;

        public DateTimeEditControl()
        {
            InitializeComponent();
            timePicker.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
        }


        public DateTime Value
        {
            get
            {
                return JoinDateTime(datePicker.Value, timePicker.Value);
            }
            set
            {
                suppressChangeEvent = true;
                datePicker.Value = value.Date;
                suppressChangeEvent = false;
                timePicker.Value = value;
            }
        }


        public event EventHandler ValueChanged;

        private DateTime JoinDateTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!suppressChangeEvent)
            {
                OnValueChanged();
            }
        }

        private void timePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!suppressChangeEvent)
            {
                OnValueChanged();
            }
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

    }
}

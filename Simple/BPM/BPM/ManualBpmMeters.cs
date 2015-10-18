using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPM
{
    public partial class ManualBpmMeters : UserControl
    {
        private const int Average1Count = 4;
        private const int Average2Count = 16;
        private const int MaxHistory = 300;

        private List<DateTime> hits = new List<DateTime>();
        private Dictionary<int, double> exactBpmDictionary = new Dictionary<int, double>();
        private Dictionary<int, double> average1BpmDictionary = new Dictionary<int, double>();
        private Dictionary<int, double> average2BpmDictionary = new Dictionary<int, double>();

        public ManualBpmMeters()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            labelBpmExact.Text = string.Empty;
            labelBpmAverage1.Text = string.Empty;
            labelBpmAverage2.Text = string.Empty;
            hits.Clear();
            exactBpmDictionary.Clear();
            average1BpmDictionary.Clear();
            average2BpmDictionary.Clear();
            UpdateChart();
            buttonHit.Focus();
        }

        private void buttonHit_Click(object sender, EventArgs e)
        {
            Hit();
        }

        private void Hit()
        {
            var now = DateTime.Now;

            if (hits.Count >= 1)
            {
                var bpm = CalculateBpm(1, now);
                labelBpmExact.Text = FormatBpm(bpm);
                exactBpmDictionary[hits.Count] = bpm;
            }

            if (hits.Count >= Average1Count)
            {
                var bpm = CalculateBpm(Average1Count, now);
                labelBpmAverage1.Text = FormatBpm(bpm);
                average1BpmDictionary[hits.Count] = bpm;
            }

            if (hits.Count >= Average2Count)
            {
                var bpm = CalculateBpm(Average2Count, now);
                labelBpmAverage2.Text = FormatBpm(bpm);
                average2BpmDictionary[hits.Count] = bpm;
            }

            hits.Add(now);
            UpdateChart();
        }

        private double CalculateBpm(int count, DateTime nowHit)
        {
            var startHit = hits[hits.Count - count];
            var totalSeconds = (nowHit - startHit).TotalSeconds;

            return 60 * count / totalSeconds;
        }

        private void UpdateChart()
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            int startIndex = Math.Max(hits.Count - MaxHistory, 0);
            for (int x = startIndex; x < hits.Count; x++)
            {
                double y;
                if (exactBpmDictionary.TryGetValue(x, out y))
                {
                    chart.Series[0].Points.AddXY(x, y);
                }
                if (average1BpmDictionary.TryGetValue(x, out y))
                {
                    chart.Series[1].Points.AddXY(x, y);
                }
                if (average2BpmDictionary.TryGetValue(x, out y))
                {
                    chart.Series[2].Points.AddXY(x, y);
                }
            }
        }

        private string FormatBpm(double bpm)
        {
            return bpm.ToString("#.0");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

    }
}

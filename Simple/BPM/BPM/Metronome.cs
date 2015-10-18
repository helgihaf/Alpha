using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace BPM
{
    public partial class Metronome : UserControl
    {
        private const int minBpm = 4;
        private const int maxBpm = 500;

        private const int bigStep = 10;
        private const int smallStep = 1;

        private double currentBmp = 110;
        private int beat;

        private SoundPlayer soundFirst;
        private SoundPlayer soundSecond;
        private Panel[] panels;

        public Metronome()
        {
            InitializeComponent();
            soundFirst = new SoundPlayer("Sounds\\sound1.wav");
            soundSecond = new SoundPlayer("Sounds\\sound2.wav");
            textBoxValue.Text = currentBmp.ToString();
            panels = new Panel[]
            {
                panel1,
                panel2,
                panel3,
                panel4,
            };
        }

        private void textBoxValue_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !GetTextBoxBmpValue().HasValue;
        }

        private void textBoxValue_Validated(object sender, EventArgs e)
        {
            TrySetBmpValue();
        }

        private void TrySetBmpValue(bool force = false)
        {
            double? bpmValue = GetTextBoxBmpValue();
            if (bpmValue != null && (force || bpmValue.Value != currentBmp))
            {
                SetBpm(bpmValue.Value);
            }
        }

        private void SetBpm(double value)
        {
            beat = 0;
            currentBmp = value;
            var savedEnabled = timer.Enabled;
            timer.Enabled = false;
            timer.Interval = Convert.ToInt32(60000 / value);
            timer.Enabled = savedEnabled;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            PlayBeat();
        }

        private void PlayBeat()
        {
            if (beat == 0)
            {
                soundFirst.Play();
            }
            else
            {
                soundSecond.Play();
            }

            for (int i = 0; i < panels.Length; i++)
            {
                if (i != beat)
                {
                    panels[i].BackColor = this.BackColor;
                }
                else
                {
                    panels[i].BackColor = Color.Orange;
                }
            }
            beat = (beat + 1) % 4;
        }

        private void buttonOnOff_Click(object sender, EventArgs e)
        {
            bool switchingOn = buttonOnOff.Checked;
            if (switchingOn)
            {
                TrySetBmpValue(true);
                PlayBeat();
            }
            timer.Enabled = switchingOn;
        }

        private void textBoxValue_Leave(object sender, EventArgs e)
        {
            TrySetBmpValue();
        }

        private double? GetTextBoxBmpValue()
        {
            double value;
            if (double.TryParse(textBoxValue.Text, out value))
            {
                if (value >= minBpm && value <= maxBpm)
                {
                    return value;
                }
            }

            return null;
        }

        private double AdjustMinMax(double newBpm)
        {
            if (newBpm < minBpm)
                newBpm = minBpm;

            if (newBpm > maxBpm)
                newBpm = maxBpm;

            return newBpm;
        }

        private void ManualAdjust(int step)
        {
            var newBpm = AdjustMinMax(currentBmp + step);
            textBoxValue.Text = newBpm.ToString();
            SetBpm(newBpm);
        }
        
        private void buttonMinusBig_Click(object sender, EventArgs e)
        {
            ManualAdjust(-bigStep);
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            ManualAdjust(-smallStep);
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            ManualAdjust(smallStep);
        }

        private void buttonPlusBig_Click(object sender, EventArgs e)
        {
            ManualAdjust(bigStep);
        }
    }
}

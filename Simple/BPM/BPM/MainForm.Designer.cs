namespace BPM
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageManualBpm = new System.Windows.Forms.TabPage();
            this.tabPageMetronome = new System.Windows.Forms.TabPage();
            this.manualBpmMeters1 = new BPM.ManualBpmMeters();
            this.metronome = new BPM.Metronome();
            this.tabControl.SuspendLayout();
            this.tabPageManualBpm.SuspendLayout();
            this.tabPageMetronome.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageManualBpm);
            this.tabControl.Controls.Add(this.tabPageMetronome);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(570, 369);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageManualBpm
            // 
            this.tabPageManualBpm.Controls.Add(this.manualBpmMeters1);
            this.tabPageManualBpm.Location = new System.Drawing.Point(4, 22);
            this.tabPageManualBpm.Name = "tabPageManualBpm";
            this.tabPageManualBpm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManualBpm.Size = new System.Drawing.Size(562, 343);
            this.tabPageManualBpm.TabIndex = 0;
            this.tabPageManualBpm.Text = "Manual BPM";
            this.tabPageManualBpm.UseVisualStyleBackColor = true;
            // 
            // tabPageMetronome
            // 
            this.tabPageMetronome.Controls.Add(this.metronome);
            this.tabPageMetronome.Location = new System.Drawing.Point(4, 22);
            this.tabPageMetronome.Name = "tabPageMetronome";
            this.tabPageMetronome.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMetronome.Size = new System.Drawing.Size(562, 343);
            this.tabPageMetronome.TabIndex = 1;
            this.tabPageMetronome.Text = "Metronome";
            this.tabPageMetronome.UseVisualStyleBackColor = true;
            // 
            // manualBpmMeters1
            // 
            this.manualBpmMeters1.BackColor = System.Drawing.Color.Transparent;
            this.manualBpmMeters1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manualBpmMeters1.Location = new System.Drawing.Point(3, 3);
            this.manualBpmMeters1.Name = "manualBpmMeters1";
            this.manualBpmMeters1.Size = new System.Drawing.Size(556, 337);
            this.manualBpmMeters1.TabIndex = 0;
            // 
            // metronome
            // 
            this.metronome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metronome.Location = new System.Drawing.Point(3, 3);
            this.metronome.Name = "metronome";
            this.metronome.Size = new System.Drawing.Size(556, 337);
            this.metronome.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 369);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Knightrunner Simple BPM";
            this.tabControl.ResumeLayout(false);
            this.tabPageManualBpm.ResumeLayout(false);
            this.tabPageMetronome.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageManualBpm;
        private System.Windows.Forms.TabPage tabPageMetronome;
        private ManualBpmMeters manualBpmMeters1;
        private Metronome metronome;

    }
}


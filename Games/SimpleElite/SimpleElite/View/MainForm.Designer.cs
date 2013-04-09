namespace SimpleElite.View
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.controlPanel = new SimpleElite.View.ControlPanel();
            this.spacePanel = new SimpleElite.View.SpacePanel();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.Color.Black;
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlPanel.ForeColor = System.Drawing.Color.White;
            this.controlPanel.Location = new System.Drawing.Point(0, 361);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(784, 200);
            this.controlPanel.TabIndex = 1;
            // 
            // spacePanel
            // 
            this.spacePanel.BackColor = System.Drawing.Color.Black;
            this.spacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spacePanel.ForeColor = System.Drawing.Color.White;
            this.spacePanel.Location = new System.Drawing.Point(0, 0);
            this.spacePanel.Name = "spacePanel";
            this.spacePanel.Size = new System.Drawing.Size(784, 561);
            this.spacePanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.spacePanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SpacePanel spacePanel;
        private ControlPanel controlPanel;
        private System.Windows.Forms.Timer timer;
    }
}


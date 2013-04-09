namespace SimpleElite.View
{
    partial class ControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.leftControlPanel = new SimpleElite.View.LeftControlPanel();
            this.rightControlPanel = new SimpleElite.View.RightControlPanel();
            this.SuspendLayout();
            // 
            // leftControlPanel
            // 
            this.leftControlPanel.BackColor = System.Drawing.Color.Black;
            this.leftControlPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftControlPanel.ForeColor = System.Drawing.Color.White;
            this.leftControlPanel.Location = new System.Drawing.Point(0, 0);
            this.leftControlPanel.Name = "leftControlPanel";
            this.leftControlPanel.Size = new System.Drawing.Size(259, 200);
            this.leftControlPanel.TabIndex = 0;
            // 
            // rightControlPanel
            // 
            this.rightControlPanel.BackColor = System.Drawing.Color.Black;
            this.rightControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightControlPanel.ForeColor = System.Drawing.Color.White;
            this.rightControlPanel.Location = new System.Drawing.Point(541, 0);
            this.rightControlPanel.Name = "rightControlPanel";
            this.rightControlPanel.Size = new System.Drawing.Size(259, 200);
            this.rightControlPanel.TabIndex = 1;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.rightControlPanel);
            this.Controls.Add(this.leftControlPanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(800, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private LeftControlPanel leftControlPanel;
        private RightControlPanel rightControlPanel;
    }
}

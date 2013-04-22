namespace PrototypeClient
{
    partial class TimelineControl
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.verticalClockAxis1 = new PrototypeClient.VerticalClockAxis();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(824, 100);
            this.panelTop.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 100);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(824, 252);
            this.panelMain.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.verticalClockAxis1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 1200);
            this.panel1.TabIndex = 1;
            // 
            // verticalClockAxis1
            // 
            this.verticalClockAxis1.Dock = System.Windows.Forms.DockStyle.Left;
            this.verticalClockAxis1.LeftMargin = 16;
            this.verticalClockAxis1.Location = new System.Drawing.Point(0, 0);
            this.verticalClockAxis1.Name = "verticalClockAxis1";
            this.verticalClockAxis1.Size = new System.Drawing.Size(81, 1200);
            this.verticalClockAxis1.TabIndex = 0;
            this.verticalClockAxis1.TopMargin = 16;
            this.verticalClockAxis1.Zoom = 1D;
            // 
            // TimelineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.Name = "TimelineControl";
            this.Size = new System.Drawing.Size(824, 352);
            this.panelMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VerticalClockAxis verticalClockAxis1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel1;

    }
}

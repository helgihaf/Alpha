namespace SimpleElite.View
{
    partial class MeasureBar
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
            this.panelValue = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelValue
            // 
            this.panelValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelValue.BackColor = System.Drawing.Color.White;
            this.panelValue.Location = new System.Drawing.Point(0, 3);
            this.panelValue.Name = "panelValue";
            this.panelValue.Size = new System.Drawing.Size(110, 24);
            this.panelValue.TabIndex = 0;
            // 
            // MeasureBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.panelValue);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MeasureBar";
            this.Size = new System.Drawing.Size(150, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelValue;
    }
}

namespace Knightrunner.SimpleWar.Editor
{
    partial class TerrainPalletItem
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
            this.panelColor = new System.Windows.Forms.Panel();
            this.labelText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(12, 16);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(32, 32);
            this.panelColor.TabIndex = 0;
            this.panelColor.Click += new System.EventHandler(this.panelColor_Click);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(58, 26);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(50, 13);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "labelText";
            this.labelText.Click += new System.EventHandler(this.labelText_Click);
            // 
            // TerrainPalletItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.panelColor);
            this.Name = "TerrainPalletItem";
            this.Size = new System.Drawing.Size(148, 62);
            this.Click += new System.EventHandler(this.TerrainPalletItem_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TerrainPalletItem_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Label labelText;
    }
}

namespace Knightrunner.WorkTrack.UI.Dialogs
{
    partial class TrackWorkSimpleDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxText = new System.Windows.Forms.ComboBox();
            this.okCancelControl = new Knightrunner.WorkTrack.UI.Controls.OKCancelControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Text:";
            // 
            // comboBoxText
            // 
            this.comboBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxText.FormattingEnabled = true;
            this.comboBoxText.Location = new System.Drawing.Point(96, 12);
            this.comboBoxText.Name = "comboBoxText";
            this.comboBoxText.Size = new System.Drawing.Size(342, 21);
            this.comboBoxText.TabIndex = 35;
            // 
            // okCancelControl
            // 
            this.okCancelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okCancelControl.Location = new System.Drawing.Point(0, 51);
            this.okCancelControl.Name = "okCancelControl";
            this.okCancelControl.Size = new System.Drawing.Size(450, 53);
            this.okCancelControl.TabIndex = 36;
            this.okCancelControl.ButtonOKClick += new System.EventHandler(this.okCancelControl_ButtonOKClick);
            // 
            // TrackWorkSimpleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 104);
            this.Controls.Add(this.okCancelControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxText);
            this.Name = "TrackWorkSimpleDialog";
            this.Text = "What are you doing?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxText;
        private Controls.OKCancelControl okCancelControl;
    }
}
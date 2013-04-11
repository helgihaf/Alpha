namespace Knightrunner.WorkTrack.UI.Dialogs
{
    partial class TrackWorkDialog
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
            this.okCancelControl = new Knightrunner.WorkTrack.UI.Controls.OKCancelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.comboBoxActivity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxText = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimeStart = new Knightrunner.WorkTrack.UI.Controls.DateTimeEditControl();
            this.dateTimeEnd = new Knightrunner.WorkTrack.UI.Controls.DateTimeEditControl();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxEnd = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okCancelControl
            // 
            this.okCancelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okCancelControl.Location = new System.Drawing.Point(0, 157);
            this.okCancelControl.Name = "okCancelControl";
            this.okCancelControl.Size = new System.Drawing.Size(440, 53);
            this.okCancelControl.TabIndex = 11;
            this.okCancelControl.ButtonOKClick += new System.EventHandler(this.okCancelControl_ButtonOKClick);
            this.okCancelControl.ButtonCancelClick += new System.EventHandler(this.okCancelControl_ButtonCancelClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project:";
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(121, 12);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(307, 21);
            this.comboBoxProject.TabIndex = 1;
            this.comboBoxProject.SelectedValueChanged += new System.EventHandler(this.comboBoxProject_SelectedValueChanged);
            // 
            // comboBoxActivity
            // 
            this.comboBoxActivity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxActivity.FormattingEnabled = true;
            this.comboBoxActivity.Location = new System.Drawing.Point(121, 39);
            this.comboBoxActivity.Name = "comboBoxActivity";
            this.comboBoxActivity.Size = new System.Drawing.Size(307, 21);
            this.comboBoxActivity.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Activity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Text:";
            // 
            // comboBoxText
            // 
            this.comboBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxText.FormattingEnabled = true;
            this.comboBoxText.Location = new System.Drawing.Point(121, 66);
            this.comboBoxText.Name = "comboBoxText";
            this.comboBoxText.Size = new System.Drawing.Size(307, 21);
            this.comboBoxText.TabIndex = 5;
            this.comboBoxText.TextChanged += new System.EventHandler(this.comboBoxText_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Start:";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(142, 93);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(284, 21);
            this.dateTimeStart.TabIndex = 7;
            this.dateTimeStart.Value = new System.DateTime(2011, 6, 16, 16, 4, 18, 0);
            this.dateTimeStart.ValueChanged += new System.EventHandler(this.dateTimeStart_ValueChanged);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(142, 120);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(284, 21);
            this.dateTimeEnd.TabIndex = 10;
            this.dateTimeEnd.Value = new System.DateTime(2011, 6, 16, 16, 4, 18, 0);
            this.dateTimeEnd.ValueChanged += new System.EventHandler(this.dateTimeEnd_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "End:";
            // 
            // checkBoxEnd
            // 
            this.checkBoxEnd.AutoSize = true;
            this.checkBoxEnd.Location = new System.Drawing.Point(121, 123);
            this.checkBoxEnd.Name = "checkBoxEnd";
            this.checkBoxEnd.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnd.TabIndex = 9;
            this.checkBoxEnd.UseVisualStyleBackColor = true;
            this.checkBoxEnd.CheckedChanged += new System.EventHandler(this.checkBoxEnd_CheckedChanged);
            // 
            // TrackWorkDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 210);
            this.Controls.Add(this.checkBoxEnd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.dateTimeStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxText);
            this.Controls.Add(this.comboBoxActivity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okCancelControl);
            this.Name = "TrackWorkDialog";
            this.Text = "What are you doing?";
            this.Load += new System.EventHandler(this.TrackWorkDialog_Load);
            this.Shown += new System.EventHandler(this.TrackWorkDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.OKCancelControl okCancelControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.ComboBox comboBoxActivity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxText;
        private System.Windows.Forms.Label label4;
        private Controls.DateTimeEditControl dateTimeStart;
        private Controls.DateTimeEditControl dateTimeEnd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxEnd;
    }
}
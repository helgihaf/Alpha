namespace Knightrunner.WorkTrack.WinForms.Controls
{
    partial class DatabaseLoginControl
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
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxRembemberPassword = new System.Windows.Forms.CheckBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAuthentication = new System.Windows.Forms.ComboBox();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDatabase.Location = new System.Drawing.Point(129, 128);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(307, 20);
            this.textBoxDatabase.TabIndex = 39;
            this.textBoxDatabase.TextChanged += new System.EventHandler(this.textBoxDatabase_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "&Database:";
            // 
            // checkBoxRembemberPassword
            // 
            this.checkBoxRembemberPassword.AutoSize = true;
            this.checkBoxRembemberPassword.Location = new System.Drawing.Point(144, 105);
            this.checkBoxRembemberPassword.Name = "checkBoxRembemberPassword";
            this.checkBoxRembemberPassword.Size = new System.Drawing.Size(125, 17);
            this.checkBoxRembemberPassword.TabIndex = 37;
            this.checkBoxRembemberPassword.Text = "Re&member password";
            this.checkBoxRembemberPassword.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Location = new System.Drawing.Point(144, 79);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(292, 20);
            this.textBoxPassword.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "&Password:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserName.Location = new System.Drawing.Point(144, 53);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(292, 20);
            this.textBoxUserName.TabIndex = 34;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "&User name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "&Authentication:";
            // 
            // comboBoxAuthentication
            // 
            this.comboBoxAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuthentication.FormattingEnabled = true;
            this.comboBoxAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.comboBoxAuthentication.Location = new System.Drawing.Point(129, 26);
            this.comboBoxAuthentication.Name = "comboBoxAuthentication";
            this.comboBoxAuthentication.Size = new System.Drawing.Size(307, 21);
            this.comboBoxAuthentication.TabIndex = 32;
            this.comboBoxAuthentication.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuthentication_SelectedIndexChanged);
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerName.Location = new System.Drawing.Point(129, 0);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(307, 20);
            this.textBoxServerName.TabIndex = 30;
            this.textBoxServerName.TextChanged += new System.EventHandler(this.textBoxServerName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "&Server name:";
            // 
            // DatabaseLoginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDatabase);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxRembemberPassword);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxAuthentication);
            this.Controls.Add(this.textBoxServerName);
            this.Controls.Add(this.label1);
            this.Name = "DatabaseLoginControl";
            this.Size = new System.Drawing.Size(439, 151);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxRembemberPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAuthentication;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label label1;
    }
}

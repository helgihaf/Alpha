namespace Knightrunner.WorkTrack.WinForms.Dialogs
{
    partial class ConnectionWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionWizard));
            Knightrunner.WorkTrack.WinForms.DatabaseConnectionInfo databaseConnectionInfo3 = new Knightrunner.WorkTrack.WinForms.DatabaseConnectionInfo();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonNewDatabase = new System.Windows.Forms.RadioButton();
            this.radioButtonConnect = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.databaseLoginControl = new Knightrunner.WorkTrack.WinForms.Controls.DatabaseLoginControl();
            this.panel2.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // radioButtonNewDatabase
            // 
            resources.ApplyResources(this.radioButtonNewDatabase, "radioButtonNewDatabase");
            this.radioButtonNewDatabase.Checked = true;
            this.radioButtonNewDatabase.Name = "radioButtonNewDatabase";
            this.radioButtonNewDatabase.TabStop = true;
            this.radioButtonNewDatabase.UseVisualStyleBackColor = true;
            // 
            // radioButtonConnect
            // 
            resources.ApplyResources(this.radioButtonConnect, "radioButtonConnect");
            this.radioButtonConnect.Name = "radioButtonConnect";
            this.radioButtonConnect.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.Controls.Add(this.labelTitle);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.Name = "labelTitle";
            // 
            // buttonFinish
            // 
            resources.ApplyResources(this.buttonFinish, "buttonFinish");
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
            this.panelBottom.Controls.Add(this.buttonBack);
            this.panelBottom.Controls.Add(this.buttonNext);
            this.panelBottom.Controls.Add(this.buttonCancel);
            this.panelBottom.Controls.Add(this.buttonFinish);
            resources.ApplyResources(this.panelBottom, "panelBottom");
            this.panelBottom.Name = "panelBottom";
            // 
            // databaseLoginControl
            // 
            resources.ApplyResources(this.databaseLoginControl, "databaseLoginControl");
            databaseConnectionInfo3.DatabaseName = "";
            databaseConnectionInfo3.EncryptedPassword = null;
            databaseConnectionInfo3.Password = null;
            databaseConnectionInfo3.RememberPassword = false;
            databaseConnectionInfo3.ServerName = "";
            databaseConnectionInfo3.UserName = null;
            databaseConnectionInfo3.UseWindowsAuthentication = true;
            this.databaseLoginControl.ConnectionInfo = databaseConnectionInfo3;
            this.databaseLoginControl.Name = "databaseLoginControl";
            this.databaseLoginControl.ValidationStatusChanged += new System.EventHandler<Knightrunner.WorkTrack.WinForms.Controls.ValidationStatusEventArgs>(this.databaseLoginControl_ValidationStatusChanged);
            // 
            // ConnectionWizard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.databaseLoginControl);
            this.Controls.Add(this.radioButtonConnect);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.radioButtonNewDatabase);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Name = "ConnectionWizard";
            this.Load += new System.EventHandler(this.ConnectionWizard_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonConnect;
        private System.Windows.Forms.RadioButton radioButtonNewDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Panel panelBottom;
        private Controls.DatabaseLoginControl databaseLoginControl;
    }
}
namespace DataSchemaGui
{
    partial class ProjectView
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.titlePanel = new Knightrunner.Library.Controls.TitlePanel();
            this.projectExplorer = new DataSchemaGui.ProjectExplorer();
            this.messageLog = new DataSchemaGui.MessageLog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).BeginInit();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.projectExplorer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainerInner);
            this.splitContainer.Size = new System.Drawing.Size(871, 608);
            this.splitContainer.SplitterDistance = 290;
            this.splitContainer.TabIndex = 1;
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerInner.Name = "splitContainerInner";
            this.splitContainerInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInner.Panel1
            // 
            this.splitContainerInner.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainerInner.Panel1.Controls.Add(this.titlePanel);
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.messageLog);
            this.splitContainerInner.Size = new System.Drawing.Size(577, 608);
            this.splitContainerInner.SplitterDistance = 469;
            this.splitContainerInner.TabIndex = 0;
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.SystemColors.Window;
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(577, 469);
            this.titlePanel.TabIndex = 0;
            this.titlePanel.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // projectExplorer
            // 
            this.projectExplorer.Project = null;
            this.projectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectExplorer.Location = new System.Drawing.Point(0, 0);
            this.projectExplorer.Name = "projectExplorer";
            this.projectExplorer.Size = new System.Drawing.Size(290, 608);
            this.projectExplorer.TabIndex = 0;
            // 
            // messageLog
            // 
            this.messageLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLog.Location = new System.Drawing.Point(0, 0);
            this.messageLog.Name = "messageLog";
            this.messageLog.Size = new System.Drawing.Size(577, 135);
            this.messageLog.TabIndex = 0;
            // 
            // DataSchemaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "DataSchemaView";
            this.Size = new System.Drawing.Size(871, 608);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).EndInit();
            this.splitContainerInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private ProjectExplorer projectExplorer;
        private System.Windows.Forms.SplitContainer splitContainerInner;
        private MessageLog messageLog;
        private Knightrunner.Library.Controls.TitlePanel titlePanel;
    }
}

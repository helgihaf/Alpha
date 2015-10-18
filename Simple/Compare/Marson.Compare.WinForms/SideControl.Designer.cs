using System;
using Marson.Compare.Core;

namespace Marson.Compare.WinForms
{
    partial class SideControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SideControl));
            this.treeView = new Marson.Compare.WinForms.LinkableTreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.textBoxDirectory = new System.Windows.Forms.ToolStripTextBox();
            this.buttonBrowse = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowLines = false;
            this.treeView.ShowPlusMinus = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(613, 334);
            this.treeView.TabIndex = 1;
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Blank.png");
            this.imageList.Images.SetKeyName(1, "DiamondBlack.png");
            this.imageList.Images.SetKeyName(2, "DiamondBlue.png");
            this.imageList.Images.SetKeyName(3, "DiamondGray.png");
            this.imageList.Images.SetKeyName(4, "DiamondRed.png");
            this.imageList.Images.SetKeyName(5, "FolderBlack.png");
            this.imageList.Images.SetKeyName(6, "FolderBlue.png");
            this.imageList.Images.SetKeyName(7, "FolderBlueGray.png");
            this.imageList.Images.SetKeyName(8, "FolderBlueRed.png");
            this.imageList.Images.SetKeyName(9, "FolderGray.png");
            this.imageList.Images.SetKeyName(10, "FolderRed.png");
            this.imageList.Images.SetKeyName(11, "FolderRedGray.png");
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBoxDirectory,
            this.buttonBrowse});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(613, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.Resize += new System.EventHandler(this.toolStrip1_Resize);
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxDirectory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.textBoxDirectory.AutoSize = false;
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(200, 25);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.Image")));
            this.buttonBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(23, 22);
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // SideControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolStrip);
            this.Name = "SideControl";
            this.Size = new System.Drawing.Size(613, 359);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Marson.Compare.WinForms.LinkableTreeView treeView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripTextBox textBoxDirectory;
        private System.Windows.Forms.ToolStripButton buttonBrowse;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

using System;
using Marson.Compare.Core;

namespace Marson.Compare.WinForms
{
    partial class SideOld
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.treeView = new Aga.Controls.Tree.TreeViewAdv();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(419, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.BackColor = System.Drawing.SystemColors.Window;
            this.treeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeView.DefaultToolTipProvider = null;
            this.treeView.DisplayDraggingNodes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Model = null;
            this.treeView.Name = "treeView";
            this.treeView.NodeControls.Add(this.nodeTextBox1);
            this.treeView.SelectedNode = null;
            this.treeView.ShowLines = false;
            this.treeView.ShowPlusMinus = false;
            this.treeView.Size = new System.Drawing.Size(419, 520);
            this.treeView.TabIndex = 2;
            this.treeView.Expanded += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeView_Expanded);
            // 
            // nodeTextBox1
            // 
            this.nodeTextBox1.DataPropertyName = "Text";
            this.nodeTextBox1.IncrementalSearchEnabled = true;
            this.nodeTextBox1.LeftMargin = 3;
            this.nodeTextBox1.ParentColumn = null;
            // 
            // Side
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Side";
            this.Size = new System.Drawing.Size(419, 545);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Aga.Controls.Tree.TreeViewAdv treeView;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
    }
}

namespace Marson.Compare.WinForms
{
    partial class DirCompareView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sideRight = new Marson.Compare.WinForms.SideControl();
            this.sideLeft = new Marson.Compare.WinForms.SideControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.sideRight, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.sideLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 451);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sideRight
            // 
            this.sideRight.DirPath = "";
            this.sideRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideRight.Location = new System.Drawing.Point(441, 3);
            this.sideRight.Name = "sideRight";
            this.sideRight.Size = new System.Drawing.Size(393, 445);
            this.sideRight.TabIndex = 2;
            this.sideRight.ExpandingNode += new System.EventHandler<Marson.Compare.WinForms.EntryEventArgs>(this.sideRight_ExpandingNode);
            this.sideRight.CollapsingNode += new System.EventHandler<Marson.Compare.WinForms.EntryEventArgs>(this.sideRight_CollapsingNode);
            this.sideRight.SelectedNode += new System.EventHandler<Marson.Compare.WinForms.EntryEventArgs>(this.sideRight_SelectedNode);
            // 
            // sideLeft
            // 
            this.sideLeft.DirPath = "";
            this.sideLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideLeft.Location = new System.Drawing.Point(3, 3);
            this.sideLeft.Name = "sideLeft";
            this.sideLeft.Size = new System.Drawing.Size(392, 445);
            this.sideLeft.TabIndex = 0;
            this.sideLeft.ExpandingNode += new System.EventHandler<Marson.Compare.WinForms.EntryEventArgs>(this.sideLeft_ExpandingNode);
            this.sideLeft.CollapsingNode += new System.EventHandler<Marson.Compare.WinForms.EntryEventArgs>(this.sideLeft_CollapsingNode);
            this.sideLeft.SelectedNode += new System.EventHandler<Marson.Compare.WinForms.EntryEventArgs>(this.sideLeft_SelectedNode);
            this.sideLeft.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sideLeft_Scroll);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonGo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(401, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(34, 445);
            this.panel1.TabIndex = 1;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(0, 3);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(34, 23);
            this.buttonGo.TabIndex = 0;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // DirCompareView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DirCompareView";
            this.Size = new System.Drawing.Size(837, 451);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SideControl sideRight;
        private SideControl sideLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGo;
    }
}

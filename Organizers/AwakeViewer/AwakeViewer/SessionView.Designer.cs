namespace AwakeViewer
{
    partial class SessionView
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
            this.listView = new System.Windows.Forms.ListView();
            this.chStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.chWeekday = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chStart,
            this.chWeekday,
            this.chEnd,
            this.chDuration,
            this.chCategory,
            this.chText});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(691, 429);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_KeyDown);
            // 
            // chStart
            // 
            this.chStart.Text = "Start";
            this.chStart.Width = 120;
            // 
            // chEnd
            // 
            this.chEnd.Text = "End";
            this.chEnd.Width = 120;
            // 
            // chDuration
            // 
            this.chDuration.Text = "Duration";
            this.chDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chDuration.Width = 80;
            // 
            // chCategory
            // 
            this.chCategory.Text = "Category";
            this.chCategory.Width = 100;
            // 
            // chText
            // 
            this.chText.Text = "Text";
            this.chText.Width = 200;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonMerge);
            this.panelBottom.Controls.Add(this.buttonDelete);
            this.panelBottom.Controls.Add(this.buttonNew);
            this.panelBottom.Controls.Add(this.buttonEdit);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 429);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(691, 37);
            this.panelBottom.TabIndex = 1;
            // 
            // buttonMerge
            // 
            this.buttonMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMerge.Image = global::AwakeViewer.Properties.Resources.Merge;
            this.buttonMerge.Location = new System.Drawing.Point(556, 6);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(24, 24);
            this.buttonMerge.TabIndex = 3;
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.buttonMerge_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Image = global::AwakeViewer.Properties.Resources.MinusImage;
            this.buttonDelete.Location = new System.Drawing.Point(662, 6);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNew.Image = global::AwakeViewer.Properties.Resources.PlusImage;
            this.buttonNew.Location = new System.Drawing.Point(632, 6);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(24, 24);
            this.buttonNew.TabIndex = 1;
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Image = global::AwakeViewer.Properties.Resources.Edit;
            this.buttonEdit.Location = new System.Drawing.Point(602, 6);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(24, 24);
            this.buttonEdit.TabIndex = 0;
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // chWeekday
            // 
            this.chWeekday.Text = "Day of Week";
            this.chWeekday.Width = 40;
            // 
            // SessionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.panelBottom);
            this.Name = "SessionView";
            this.Size = new System.Drawing.Size(691, 466);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.ColumnHeader chStart;
        private System.Windows.Forms.ColumnHeader chEnd;
        private System.Windows.Forms.ColumnHeader chDuration;
        private System.Windows.Forms.ColumnHeader chCategory;
        private System.Windows.Forms.ColumnHeader chText;
        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.ColumnHeader chWeekday;
    }
}

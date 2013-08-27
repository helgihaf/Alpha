namespace SimpleSearch
{
	partial class SearchTextForm
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
			this.panelTop = new System.Windows.Forms.Panel();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.checkBoxMultiText = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxSeperator = new System.Windows.Forms.TextBox();
			this.textBoxText = new System.Windows.Forms.TextBox();
			this.panelTop.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.textBoxSeperator);
			this.panelTop.Controls.Add(this.label1);
			this.panelTop.Controls.Add(this.checkBoxMultiText);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(406, 44);
			this.panelTop.TabIndex = 0;
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.buttonCancel);
			this.panelBottom.Controls.Add(this.buttonOk);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 215);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(406, 49);
			this.panelBottom.TabIndex = 2;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(238, 14);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(319, 14);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// checkBoxMultiText
			// 
			this.checkBoxMultiText.AutoSize = true;
			this.checkBoxMultiText.Location = new System.Drawing.Point(12, 12);
			this.checkBoxMultiText.Name = "checkBoxMultiText";
			this.checkBoxMultiText.Size = new System.Drawing.Size(144, 17);
			this.checkBoxMultiText.TabIndex = 0;
			this.checkBoxMultiText.Text = "Each line is a search text";
			this.checkBoxMultiText.UseVisualStyleBackColor = true;
			this.checkBoxMultiText.CheckedChanged += new System.EventHandler(this.checkBoxMultiText_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(269, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Seperator character";
			// 
			// textBoxSeperator
			// 
			this.textBoxSeperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSeperator.Location = new System.Drawing.Point(376, 9);
			this.textBoxSeperator.MaxLength = 1;
			this.textBoxSeperator.Name = "textBoxSeperator";
			this.textBoxSeperator.Size = new System.Drawing.Size(18, 20);
			this.textBoxSeperator.TabIndex = 1;
			this.textBoxSeperator.Text = ";";
			// 
			// textBoxText
			// 
			this.textBoxText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxText.Location = new System.Drawing.Point(0, 44);
			this.textBoxText.Multiline = true;
			this.textBoxText.Name = "textBoxText";
			this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxText.Size = new System.Drawing.Size(406, 171);
			this.textBoxText.TabIndex = 1;
			this.textBoxText.WordWrap = false;
			// 
			// SearchTextForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(406, 264);
			this.Controls.Add(this.textBoxText);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panelTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SearchTextForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Search Text";
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.panelBottom.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxSeperator;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxMultiText;
		private System.Windows.Forms.TextBox textBoxText;
	}
}
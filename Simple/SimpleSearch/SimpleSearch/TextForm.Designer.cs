namespace SimpleSearch
{
	partial class TextForm
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
			this.MainTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// MainTextBox
			// 
			this.MainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTextBox.Location = new System.Drawing.Point(0, 0);
			this.MainTextBox.Multiline = true;
			this.MainTextBox.Name = "MainTextBox";
			this.MainTextBox.ReadOnly = true;
			this.MainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MainTextBox.Size = new System.Drawing.Size(284, 264);
			this.MainTextBox.TabIndex = 0;
			this.MainTextBox.WordWrap = false;
			// 
			// TextForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.MainTextBox);
			this.Name = "TextForm";
			this.Text = "TextForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.TextBox MainTextBox;

	}
}
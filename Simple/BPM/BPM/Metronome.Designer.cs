namespace BPM
{
    partial class Metronome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Metronome));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonOnOff = new System.Windows.Forms.ToolStripButton();
            this.buttonMinusBig = new System.Windows.Forms.ToolStripButton();
            this.buttonMinus = new System.Windows.Forms.ToolStripButton();
            this.textBoxValue = new System.Windows.Forms.ToolStripTextBox();
            this.buttonPlus = new System.Windows.Forms.ToolStripButton();
            this.buttonPlusBig = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonOnOff,
            this.buttonMinusBig,
            this.buttonMinus,
            this.textBoxValue,
            this.buttonPlus,
            this.buttonPlusBig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(595, 67);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonOnOff
            // 
            this.buttonOnOff.CheckOnClick = true;
            this.buttonOnOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonOnOff.Image = ((System.Drawing.Image)(resources.GetObject("buttonOnOff.Image")));
            this.buttonOnOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOnOff.Name = "buttonOnOff";
            this.buttonOnOff.Size = new System.Drawing.Size(66, 64);
            this.buttonOnOff.Text = "On";
            this.buttonOnOff.Click += new System.EventHandler(this.buttonOnOff_Click);
            // 
            // buttonMinusBig
            // 
            this.buttonMinusBig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonMinusBig.Image = ((System.Drawing.Image)(resources.GetObject("buttonMinusBig.Image")));
            this.buttonMinusBig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMinusBig.Name = "buttonMinusBig";
            this.buttonMinusBig.Size = new System.Drawing.Size(50, 64);
            this.buttonMinusBig.Text = "--";
            this.buttonMinusBig.Click += new System.EventHandler(this.buttonMinusBig_Click);
            // 
            // buttonMinus
            // 
            this.buttonMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonMinus.Image = ((System.Drawing.Image)(resources.GetObject("buttonMinus.Image")));
            this.buttonMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMinus.Name = "buttonMinus";
            this.buttonMinus.Size = new System.Drawing.Size(37, 64);
            this.buttonMinus.Text = "-";
            this.buttonMinus.Click += new System.EventHandler(this.buttonMinus_Click);
            // 
            // textBoxValue
            // 
            this.textBoxValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(100, 67);
            this.textBoxValue.Text = "0";
            this.textBoxValue.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxValue.Leave += new System.EventHandler(this.textBoxValue_Leave);
            this.textBoxValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxValue_Validating);
            this.textBoxValue.Validated += new System.EventHandler(this.textBoxValue_Validated);
            // 
            // buttonPlus
            // 
            this.buttonPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonPlus.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlus.Image")));
            this.buttonPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPlus.Name = "buttonPlus";
            this.buttonPlus.Size = new System.Drawing.Size(46, 64);
            this.buttonPlus.Text = "+";
            this.buttonPlus.ToolTipText = "+";
            this.buttonPlus.Click += new System.EventHandler(this.buttonPlus_Click);
            // 
            // buttonPlusBig
            // 
            this.buttonPlusBig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonPlusBig.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlusBig.Image")));
            this.buttonPlusBig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPlusBig.Name = "buttonPlusBig";
            this.buttonPlusBig.Size = new System.Drawing.Size(68, 64);
            this.buttonPlusBig.Text = "++";
            this.buttonPlusBig.Click += new System.EventHandler(this.buttonPlusBig_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(55, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 125);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.panel4);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(429, 90);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 80);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(89, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 80);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(175, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 80);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(261, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(80, 80);
            this.panel4.TabIndex = 3;
            // 
            // Metronome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Metronome";
            this.Size = new System.Drawing.Size(595, 375);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonMinusBig;
        private System.Windows.Forms.ToolStripButton buttonOnOff;
        private System.Windows.Forms.ToolStripButton buttonMinus;
        private System.Windows.Forms.ToolStripTextBox textBoxValue;
        private System.Windows.Forms.ToolStripButton buttonPlus;
        private System.Windows.Forms.ToolStripButton buttonPlusBig;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}

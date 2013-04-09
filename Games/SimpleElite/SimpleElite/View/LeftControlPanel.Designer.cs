namespace SimpleElite.View
{
    partial class LeftControlPanel
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
            this.measureBarAltitute = new SimpleElite.View.MeasureBar();
            this.measureBarLaserTemperature = new SimpleElite.View.MeasureBar();
            this.measureBarCabinTemperature = new SimpleElite.View.MeasureBar();
            this.measureBarFuel = new SimpleElite.View.MeasureBar();
            this.measureBarRearShield = new SimpleElite.View.MeasureBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.measureBarFrontShield = new SimpleElite.View.MeasureBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel1.Controls.Add(this.measureBarAltitute, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.measureBarLaserTemperature, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.measureBarCabinTemperature, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.measureBarFuel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.measureBarRearShield, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.measureBarFrontShield, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(259, 188);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // measureBarAltitute
            // 
            this.measureBarAltitute.BackColor = System.Drawing.Color.Red;
            this.measureBarAltitute.BarType = SimpleElite.View.BarType.Bar;
            this.measureBarAltitute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureBarAltitute.ForeColor = System.Drawing.Color.White;
            this.measureBarAltitute.Location = new System.Drawing.Point(78, 134);
            this.measureBarAltitute.Name = "measureBarAltitute";
            this.measureBarAltitute.Size = new System.Drawing.Size(177, 19);
            this.measureBarAltitute.TabIndex = 11;
            this.measureBarAltitute.Value = 0;
            // 
            // measureBarLaserTemperature
            // 
            this.measureBarLaserTemperature.BackColor = System.Drawing.Color.Red;
            this.measureBarLaserTemperature.BarType = SimpleElite.View.BarType.Bar;
            this.measureBarLaserTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureBarLaserTemperature.ForeColor = System.Drawing.Color.White;
            this.measureBarLaserTemperature.Location = new System.Drawing.Point(78, 108);
            this.measureBarLaserTemperature.Name = "measureBarLaserTemperature";
            this.measureBarLaserTemperature.Size = new System.Drawing.Size(177, 19);
            this.measureBarLaserTemperature.TabIndex = 10;
            this.measureBarLaserTemperature.Value = 0;
            // 
            // measureBarCabinTemperature
            // 
            this.measureBarCabinTemperature.BackColor = System.Drawing.Color.Red;
            this.measureBarCabinTemperature.BarType = SimpleElite.View.BarType.Bar;
            this.measureBarCabinTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureBarCabinTemperature.ForeColor = System.Drawing.Color.White;
            this.measureBarCabinTemperature.Location = new System.Drawing.Point(78, 82);
            this.measureBarCabinTemperature.Name = "measureBarCabinTemperature";
            this.measureBarCabinTemperature.Size = new System.Drawing.Size(177, 19);
            this.measureBarCabinTemperature.TabIndex = 9;
            this.measureBarCabinTemperature.Value = 0;
            // 
            // measureBarFuel
            // 
            this.measureBarFuel.BackColor = System.Drawing.Color.Red;
            this.measureBarFuel.BarType = SimpleElite.View.BarType.Bar;
            this.measureBarFuel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureBarFuel.ForeColor = System.Drawing.Color.White;
            this.measureBarFuel.Location = new System.Drawing.Point(78, 56);
            this.measureBarFuel.Name = "measureBarFuel";
            this.measureBarFuel.Size = new System.Drawing.Size(177, 19);
            this.measureBarFuel.TabIndex = 8;
            this.measureBarFuel.Value = 0;
            // 
            // measureBarRearShield
            // 
            this.measureBarRearShield.BackColor = System.Drawing.Color.Red;
            this.measureBarRearShield.BarType = SimpleElite.View.BarType.Bar;
            this.measureBarRearShield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureBarRearShield.ForeColor = System.Drawing.Color.White;
            this.measureBarRearShield.Location = new System.Drawing.Point(78, 30);
            this.measureBarRearShield.Name = "measureBarRearShield";
            this.measureBarRearShield.Size = new System.Drawing.Size(177, 19);
            this.measureBarRearShield.TabIndex = 7;
            this.measureBarRearShield.Value = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "FS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "RS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "FU";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "CT";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "LT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "AL";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // measureBarFrontShield
            // 
            this.measureBarFrontShield.BackColor = System.Drawing.Color.Red;
            this.measureBarFrontShield.BarType = SimpleElite.View.BarType.Bar;
            this.measureBarFrontShield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureBarFrontShield.ForeColor = System.Drawing.Color.White;
            this.measureBarFrontShield.Location = new System.Drawing.Point(78, 4);
            this.measureBarFrontShield.Name = "measureBarFrontShield";
            this.measureBarFrontShield.Size = new System.Drawing.Size(177, 19);
            this.measureBarFrontShield.TabIndex = 6;
            this.measureBarFrontShield.Value = 50;
            // 
            // LeftControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "LeftControlPanel";
            this.Size = new System.Drawing.Size(259, 188);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private MeasureBar measureBarFrontShield;
        private MeasureBar measureBarAltitute;
        private MeasureBar measureBarLaserTemperature;
        private MeasureBar measureBarCabinTemperature;
        private MeasureBar measureBarFuel;
        private MeasureBar measureBarRearShield;
    }
}

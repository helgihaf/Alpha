namespace BPM
{
    partial class ManualBpmMeters
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelBpmAverage2 = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelBpmAverage1 = new System.Windows.Forms.Label();
            this.labelBpmExact = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonHit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBpmAverage2
            // 
            this.labelBpmAverage2.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBpmAverage2.Location = new System.Drawing.Point(123, -1);
            this.labelBpmAverage2.Name = "labelBpmAverage2";
            this.labelBpmAverage2.Size = new System.Drawing.Size(100, 52);
            this.labelBpmAverage2.TabIndex = 11;
            this.labelBpmAverage2.Text = "label1";
            this.labelBpmAverage2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(6, 60);
            this.chart.Name = "chart";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series7.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series8.BorderWidth = 3;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "Series2";
            series8.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series9.BorderWidth = 3;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Legend = "Legend1";
            series9.Name = "Series3";
            this.chart.Series.Add(series7);
            this.chart.Series.Add(series8);
            this.chart.Series.Add(series9);
            this.chart.Size = new System.Drawing.Size(582, 316);
            this.chart.TabIndex = 10;
            this.chart.Text = "chart1";
            // 
            // labelBpmAverage1
            // 
            this.labelBpmAverage1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBpmAverage1.Location = new System.Drawing.Point(281, -1);
            this.labelBpmAverage1.Name = "labelBpmAverage1";
            this.labelBpmAverage1.Size = new System.Drawing.Size(100, 52);
            this.labelBpmAverage1.TabIndex = 9;
            this.labelBpmAverage1.Text = "label1";
            this.labelBpmAverage1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelBpmExact
            // 
            this.labelBpmExact.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBpmExact.Location = new System.Drawing.Point(439, -1);
            this.labelBpmExact.Name = "labelBpmExact";
            this.labelBpmExact.Size = new System.Drawing.Size(100, 52);
            this.labelBpmExact.TabIndex = 8;
            this.labelBpmExact.Text = "label1";
            this.labelBpmExact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(6, 31);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "&Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonHit
            // 
            this.buttonHit.Location = new System.Drawing.Point(6, 2);
            this.buttonHit.Name = "buttonHit";
            this.buttonHit.Size = new System.Drawing.Size(75, 23);
            this.buttonHit.TabIndex = 6;
            this.buttonHit.Text = "&Hit";
            this.buttonHit.UseVisualStyleBackColor = true;
            this.buttonHit.Click += new System.EventHandler(this.buttonHit_Click);
            // 
            // ManualBpmMeters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelBpmAverage2);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.labelBpmAverage1);
            this.Controls.Add(this.labelBpmExact);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonHit);
            this.Name = "ManualBpmMeters";
            this.Size = new System.Drawing.Size(595, 375);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelBpmAverage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Label labelBpmAverage1;
        private System.Windows.Forms.Label labelBpmExact;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonHit;
    }
}

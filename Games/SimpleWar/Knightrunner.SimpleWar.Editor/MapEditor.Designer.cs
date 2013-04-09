namespace Knightrunner.SimpleWar.Editor
{
    partial class MapEditor
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
            this.mapControl = new Knightrunner.SimpleWar.Editor.MapControl();
            this.terrainPallet = new Knightrunner.SimpleWar.Editor.TerrainPallet();
            this.SuspendLayout();
            // 
            // mapControl
            // 
            this.mapControl.AutoScroll = true;
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.Location = new System.Drawing.Point(150, 0);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(658, 580);
            this.mapControl.TabIndex = 1;
            this.mapControl.LocationSelected += new System.EventHandler<Knightrunner.SimpleWar.Editor.LocationSelectedEventArgs>(this.mapControl_LocationSelected);
            // 
            // terrainPallet
            // 
            this.terrainPallet.Dock = System.Windows.Forms.DockStyle.Left;
            this.terrainPallet.Location = new System.Drawing.Point(0, 0);
            this.terrainPallet.Name = "terrainPallet";
            this.terrainPallet.Size = new System.Drawing.Size(150, 580);
            this.terrainPallet.TabIndex = 0;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.terrainPallet);
            this.Name = "MapEditor";
            this.Size = new System.Drawing.Size(808, 580);
            this.ResumeLayout(false);

        }

        #endregion

        private TerrainPallet terrainPallet;
        private MapControl mapControl;
    }
}

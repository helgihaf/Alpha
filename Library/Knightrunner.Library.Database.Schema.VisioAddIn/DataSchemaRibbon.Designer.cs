namespace Knightrunner.Library.Database.Schema.VisioAddIn
{
    partial class DataSchemaRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public DataSchemaRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonGenerateData = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonGenerateData);
            this.group1.Label = "Data Schema";
            this.group1.Name = "group1";
            // 
            // buttonGenerateData
            // 
            this.buttonGenerateData.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonGenerateData.Image = global::Knightrunner.Library.Database.Schema.VisioAddIn.Properties.Resources.Document_Configuration;
            this.buttonGenerateData.Label = "Generate Data";
            this.buttonGenerateData.Name = "buttonGenerateData";
            this.buttonGenerateData.ShowImage = true;
            this.buttonGenerateData.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonGenerateData_Click);
            // 
            // DataSchemaRibbon
            // 
            this.Name = "DataSchemaRibbon";
            this.RibbonType = "Microsoft.Visio.Drawing";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.DataSchemaRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonGenerateData;
    }

    partial class ThisRibbonCollection
    {
        internal DataSchemaRibbon DataSchemaRibbon
        {
            get { return this.GetRibbon<DataSchemaRibbon>(); }
        }
    }
}

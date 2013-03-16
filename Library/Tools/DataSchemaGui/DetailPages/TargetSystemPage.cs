using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Database.Schema;

namespace DataSchemaGui.DetailPages
{
    public partial class TargetSystemPage : BasePage
    {
        public TargetSystemPage()
        {
            InitializeComponent();
            Caption = "Target system";
        }

        private TargetSystem TargetSystem
        {
            get { return (TargetSystem)DataObject; }
        }

        protected override void LoadDataObject()
        {
            textBoxName.Text = TargetSystem.Name;

            IsDataChanged = false;
        }


        private void CheckDataChanged()
        {
            if (IsLoading)
                return;

            IsDataChanged =
                string.Compare(textBoxName.Text, TargetSystem.Name) != 0;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            CheckDataChanged();
        }

        private void textBoxCodeGenerator_TextChanged(object sender, EventArgs e)
        {
            CheckDataChanged();
        }


    }
}

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
    public partial class DataSchemaPage : BasePage
    {
        public DataSchemaPage()
        {
            InitializeComponent();
            Caption = "Data schema";
        }


        private DataSchema DataSchema
        {
            get { return (DataSchema)DataObject; }
        }

        protected override void LoadDataObject()
        {
            textBoxName.Text = DataSchema.Name;
            textBoxPrimaryKeyFormat.Text = DataSchema.NameFormats.PrimaryKeyFormatString;
            textBoxForeignKeyFormat.Text = DataSchema.NameFormats.ForeignKeyFormatString;
            textBoxIndexFormat.Text = DataSchema.NameFormats.IndexFormatString;

            IsDataChanged = false;
        }


        private void CheckDataChanged()
        {
            if (IsLoading)
                return;

            IsDataChanged = 
                string.Compare(textBoxName.Text, DataSchema.Name) != 0 ||
                string.Compare(textBoxPrimaryKeyFormat.Text, DataSchema.NameFormats.PrimaryKeyFormatString) != 0 ||
                string.Compare(textBoxForeignKeyFormat.Text, DataSchema.NameFormats.ForeignKeyFormatString) != 0 ||
                string.Compare(textBoxIndexFormat.Text, DataSchema.NameFormats.IndexFormatString) != 0;
        }


        public override bool SaveData()
        {
            if (!base.SaveData())
            {
                return false;
            }

            if (textBoxName.TextLength == 0)
            {
                errorProvider.SetError(textBoxName, "Name cannot be empty");
                return false;
            }

            DataSchema.Name = textBoxName.Text;
            DataSchema.NameFormats.PrimaryKeyFormatString = textBoxPrimaryKeyFormat.Text;
            DataSchema.NameFormats.ForeignKeyFormatString = textBoxForeignKeyFormat.Text;
            DataSchema.NameFormats.IndexFormatString = textBoxIndexFormat.Text;

            return true;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxName, string.Empty);
            CheckDataChanged();
        }

        private void textBoxPrimaryKeyFormat_TextChanged(object sender, EventArgs e)
        {
            CheckDataChanged();
        }

        private void textBoxForeignKeyFormat_TextChanged(object sender, EventArgs e)
        {
            CheckDataChanged();
        }

        private void textBoxIndexFormat_TextChanged(object sender, EventArgs e)
        {
            CheckDataChanged();
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace Knightrunner.Library.Database.Schema.VisioAddIn
{
    public partial class DataSchemaRibbon
    {
        private void DataSchemaRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void buttonGenerateData_Click(object sender, RibbonControlEventArgs e)
        {
            using (DataSchemaForm form = new DataSchemaForm())
            {
                form.ShowDialog();
            }
        }
    }
}

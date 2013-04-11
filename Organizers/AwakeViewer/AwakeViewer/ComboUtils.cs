using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwakeViewer
{
    internal class ComboUtils
    {
        public static void PopulateComboBoxItems<T>(ComboBox comboBox)
        {
            Type type = typeof(T);

            comboBox.Items.Clear();
            foreach (int value in Enum.GetValues(type))
            {
                string text = Enum.GetName(type, value);
                comboBox.Items.Add
                (
                    new DataSelector { Id = value, Text = text }
                );
            }
        }

        public static void SetComboSelection(ComboBox comboBox, int value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (((DataSelector)comboBox.Items[i]).Id == value)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public static int GetComboSelection(ComboBox comboBox)
        {
            int result = -1;
            DataSelector dataSelector = comboBox.SelectedItem as DataSelector;
            if (dataSelector != null)
            {
                result = dataSelector.Id;
            }

            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Core;
using System.Resources;

namespace Knightrunner.Library.WinForms
{
    public static class ComboUtils
    {
        public static void EnumPopulate(ComboBox comboBox, Type enumType, ResourceManager resourceManager)
        {
            foreach (var item in EnumUtilities.EnumDisplayValues(enumType, resourceManager))
            {
                comboBox.Items.Add(item);
            }
        }

        public static void EnumSetValue(ComboBox comboBox, int value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (((EnumValue)comboBox.Items[i]).Value == value)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            comboBox.SelectedIndex = -1;
        }


        public static int EnumGetSelectedValue(ComboBox comboBox)
        {
            int result = -1;
            var index = comboBox.SelectedIndex;
            if (index >= 0)
            {
                result = ((EnumValue)comboBox.Items[index]).Value;
            }

            return result;
        }
    }

}

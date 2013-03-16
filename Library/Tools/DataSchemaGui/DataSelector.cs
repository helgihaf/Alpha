using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataSchemaGui
{
    public class DataSelector<TId, TData>
    {
        public TId Id { get; set; }
        public TData Object { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static void SetComboSelection(ComboBox comboBox, TData obj)
        {
            foreach (DataSelector<TId, TData> selector in comboBox.Items)
            {
                if (object.Equals(selector.Object, obj))
                {
                    comboBox.SelectedItem = selector;
                    return;
                }
            }
        }

    }
}

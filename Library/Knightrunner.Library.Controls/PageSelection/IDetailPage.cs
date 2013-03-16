using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Controls.PageSelection
{
    public interface IDetailPage
    {
        object DataObject { get; set; }
        
        void Show();
        void Hide();

        bool IsDataChanged { get; }
        bool SaveData();

        string Caption { get; }


        event EventHandler<EventArgs> DataChanged;

    }
}

using Marson.Compare.Core;
using System;

namespace Marson.Compare.WinForms
{
    public class EntryEventArgs : EventArgs
    {
        public string NodePath { get; set; }
        public Entry Entry { get; set; }
    }
}
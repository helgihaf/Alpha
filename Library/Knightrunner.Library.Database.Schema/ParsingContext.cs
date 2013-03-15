using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Knightrunner.Library.Database.Schema
{
    public class ParsingContext
    {
        public enum EntryKind
        {
            Info,
            Warning,
            Error,
        }
        
        public class Entry
        {
            public Entry()
            {
                DateTime = DateTime.Now;
            }

            public EntryKind EntryKind { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string Text { get; set; }
            public DateTime DateTime { get; private set; }

            public override string ToString()
            {
                return ToString(false);
            }

            private string ToString(bool includeTimeStamp)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}{1,-8} {2}{3}{4}",
                    includeTimeStamp ? DateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture) + " " : string.Empty,
                    EntryKind.ToString() + ":",
                    TableName != null ? TableName : string.Empty,
                    ColumnName != null ? "." + ColumnName + " " : " ",
                    Text
                    );
            }
        }

        private List<Entry> entries = new List<Entry>();

        public ParsingContext()
        {
        }


        public void AddInfo(string text)
        {
            Add(EntryKind.Info, null, null, text);
        }

        public void AddInfo(string tableName, string text)
        {
            Add(EntryKind.Info, tableName, null, text);
        }

        public void AddInfo(string tableName, string columnName, string text)
        {
            Add(EntryKind.Info, tableName, columnName, text);
        }

        public void AddWarning(string text)
        {
            Add(EntryKind.Warning, null, null, text);
        }

        public void AddWarning(string tableName, string text)
        {
            Add(EntryKind.Warning, tableName, null, text);
        }

        public void AddWarning(string tableName, string columnName, string text)
        {
            Add(EntryKind.Warning, tableName, columnName, text);
        }

        public void AddError(string text)
        {
            Add(EntryKind.Error, null, null, text);
        }

        public void AddError(string tableName, string text)
        {
            Add(EntryKind.Error, tableName, null, text);
        }

        public void AddError(string tableName, string columnName, string text)
        {
            Add(EntryKind.Error, tableName, columnName, text);
        }

        public void Add(EntryKind kind, string text)
        {
            Add(kind, null, null, text);
        }

        public void Add(EntryKind kind, string tableName, string text)
        {
            Add(kind, tableName, null, text);
        }

        public void Add(EntryKind kind, string tableName, string columnName, string text)
        {
            entries.Add(new Entry { EntryKind = kind, TableName = tableName, ColumnName = columnName, Text = text });
        }

        public ReadOnlyCollection<Entry> Entries
        {
            get
            {
                return entries.AsReadOnly();
            }
        }

        public void Clear()
        {
            entries.Clear();
        }
    }
}

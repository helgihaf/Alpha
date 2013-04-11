using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Knightrunner.WorkTrack.WinForms.Controls
{
    public class ExtendedListView : ListView
    {
        #region Win32 Interop
        private const int LVS_EX_SUBITEMIMAGES = 0x2;
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        private const int LVM_GETCOLUMNW = 0x1000 + 95;
        private const int LVM_SETCOLUMNW = 0x1000 + 96;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private extern static IntPtr SendMessageW(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class LVCOLUMN
        {
            public uint mask;
            public int fmt;
            public int cx;
            public IntPtr pszText;
            public int cchTextMax;
            public int iSubItem;
            public int iOrder;
            public int iImage;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct LVITEM
        {
            public int mask;
            public int iItem;
            public int subItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
        }


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref LVITEM lParam);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
        #endregion

        private int sortColumn;
        private bool sortedBackwards;


        public ExtendedListView()
        {
            this.sortColumn = -1;
            this.sortedBackwards = false;
        }

        public int SortColumn
        {
            get { return sortColumn; }
            set
            {
                if (sortColumn != -1)
                {
                    ColumnImageToRight(sortColumn);
                }

                sortColumn = value;

                ColumnImageToRight(sortColumn);
            }
        }

        public bool SortedBackwards
        {
            get { return sortedBackwards; }
            set { sortedBackwards = value; }
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ListView_SetExtendedListViewStyleEx(this.Handle, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES);

            if (this.SortColumn != -1)
            {
                this.ColumnImageToRight(SortColumn);
            }
        }


        private void ListView_SetExtendedListViewStyleEx(IntPtr hwnd, int mask,
        int style)
        {
            SendMessage(hwnd, LVM_SETEXTENDEDLISTVIEWSTYLE, new IntPtr(mask), new IntPtr(style));
        }


        private void ColumnImageToRight(int index)
        {
            if (!this.IsHandleCreated) return;
            if (Columns.Count == 0) return;
            if (index >= this.Columns.Count) throw new ArgumentOutOfRangeException("Column index out of range");

            IntPtr buf = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LVCOLUMN)) + 100);
            LVCOLUMN lvc = new LVCOLUMN();
            lvc.mask = 0xffff;
            Marshal.StructureToPtr(lvc, buf, false);
            IntPtr retval = SendMessageW(this.Handle, LVM_GETCOLUMNW, (IntPtr)index, buf);
            lvc = (LVCOLUMN)Marshal.PtrToStructure(buf, typeof(LVCOLUMN));
            lvc.fmt |= 0x1000;
            lvc.pszText = Marshal.StringToHGlobalUni(this.Columns[index].Text);
            Marshal.StructureToPtr(lvc, buf, false);
            retval = SendMessageW(this.Handle, LVM_SETCOLUMNW, (IntPtr)index, buf);
            Marshal.FreeHGlobal(lvc.pszText);
            Marshal.FreeHGlobal(buf);
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                OnDoubleClick(EventArgs.Empty);
            }

            return base.ProcessDialogKey(keyData);
        }


    }
}

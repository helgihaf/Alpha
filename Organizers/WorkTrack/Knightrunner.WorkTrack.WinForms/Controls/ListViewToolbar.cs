using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Knightrunner.WorkTrack.WinForms.Controls
{
    public class ListViewToolbar : ToolStrip
    {
        private ToolStripButton addButton;
        private ToolStripButton editButton;
        private ToolStripButton deleteButton;

        public ListViewToolbar()
        {
            addButton = CreateButton("Add", "BindingNavigator.AddNew.bmp");
            editButton = CreateButton("Edit", Properties.Resources.Pencil);
            deleteButton = CreateButton("Delete", "BindingNavigator.Delete.bmp");

            this.Items.AddRange(new ToolStripItem[] { this.editButton, this.addButton, this.deleteButton });
        }

        public ToolStripButton AddButton
        {
            get { return addButton; }
        }

        public ToolStripButton EditButton
        {
            get { return editButton; }
        }

        public ToolStripButton DeleteButton
        {
            get { return deleteButton; }
        }

        private ToolStripButton CreateButton(string text, string bitmapResourceName)
        {
            var bitmap = new Bitmap(typeof(BindingNavigator), bitmapResourceName);
            bitmap.MakeTransparent(Color.Magenta);
            return CreateButton(text, bitmap);
        }

        private ToolStripButton CreateButton(string text, Image image)
        {
            var button = new ToolStripButton();
            button.Text = text;
            button.Image = image;
            button.RightToLeftAutoMirrorImage = true;
            button.DisplayStyle = ToolStripItemDisplayStyle.Image;

            return button;
        }

    }
}

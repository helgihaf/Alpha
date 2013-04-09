using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knightrunner.SimpleWar.Editor
{
    public partial class MainForm : Form
    {
        private string fixedTitle;

        public MainForm()
        {
            InitializeComponent();
            fixedTitle = this.Text;
            openFileDialog.Filter = saveFileDialog.Filter;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var newMapDialog = new NewMapDialog())
            {
                if (newMapDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    var map = new Map(newMapDialog.Value.Width, newMapDialog.Value.Height);
                    SetCurrentMap(map);
                }
            }
        }

        private void SetCurrentMap(Map map)
        {
            mapEditor.Visible = false;
            mapEditor.LoadMap(map);
            mapEditor.Visible = true;
            UpdateTitle();
        }


        private void SetCurrentMap(string mapFilePath)
        {
            mapEditor.Visible = false;
            mapEditor.LoadMap(mapFilePath);
            mapEditor.Visible = true;
            UpdateTitle();
        }


        private void mapEditor_IsChangedChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            string mapName = mapEditor.FilePath != null ? mapEditor.FilePath : "(untitled)";
            string isChangedText = mapEditor.IsChanged ? " *" : string.Empty;
            Text = string.Format("{0} - {1}{2}", fixedTitle, mapName, isChangedText);
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                mapEditor.FilePath = saveFileDialog.FileName;
                mapEditor.SaveMap();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapEditor.FilePath != null)
            {
                mapEditor.SaveMap();
            }
            else
            {
                SaveAs();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                SetCurrentMap(openFileDialog.FileName);
            }
        }
    }
}

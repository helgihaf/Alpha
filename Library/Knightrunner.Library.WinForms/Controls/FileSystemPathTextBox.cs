using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knightrunner.Library.WinForms.Controls
{
    public class FileSystemPathTextBox : Control
    {
        private const FileSystemPathType DefaultPathType = FileSystemPathType.ExistingFile;

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBrowse;
        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private FileSystemPathType pathType = DefaultPathType;

        public FileSystemPathTextBox()
        {
            InitializeComponent();
        }

 
        private void InitializeComponent()
        {
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(0, 0);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(256, 20);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.textBoxPath.AutoCompleteSource = AutoCompleteSource.FileSystem;
            this.textBoxPath.TextChanged += TextBoxPathTextChanged;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(256, -1);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 22);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += ButtonBrowseClick;
            // 
            // FileSystemPathTextBox
            // 
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxPath);
            this.Size = new System.Drawing.Size(280, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TextBoxPathTextChanged(object sender, EventArgs e)
        {
            if (PathChanged != null)
            {
                PathChanged(this, EventArgs.Empty);
            }
        }

        private void ButtonBrowseClick(object sender, EventArgs e)
        {
            switch (pathType)
            {
                case FileSystemPathType.ExistingFile:
                    OpenFileDialog.FileName = textBoxPath.Text;
                    if (OpenFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxPath.Text = OpenFileDialog.FileName;
                    }
                    break;
                case FileSystemPathType.Folder:
                    FolderBrowserDialog.SelectedPath = textBoxPath.Text;
                    if (FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxPath.Text = FolderBrowserDialog.SelectedPath;
                    }
                    break;
                case FileSystemPathType.NewFile:
                    SaveFileDialog.FileName = textBoxPath.Text;
                    if (SaveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxPath.Text = SaveFileDialog.FileName;
                    }
                    break;
            }
        }


        [DefaultValue(DefaultPathType)]
        public FileSystemPathType PathType
        {
            get
            {
                return pathType;
            }
            set
            {
                this.pathType = value;
                switch (pathType)
                {
                    case FileSystemPathType.ExistingFile:
                    case FileSystemPathType.NewFile:
                        textBoxPath.AutoCompleteMode = AutoCompleteMode.Suggest;
                        textBoxPath.AutoCompleteSource = AutoCompleteSource.FileSystem;
                        break;
                    case FileSystemPathType.Folder:
                        textBoxPath.AutoCompleteMode = AutoCompleteMode.Suggest;
                        textBoxPath.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
                        break;
                }
            }
        }

        public string Path
        {
            get
            {
                return textBoxPath.Text;
            }
            set
            {
                textBoxPath.Text = value;
            }
        }

        public OpenFileDialog OpenFileDialog
        {
            get { return openFileDialog; }
        }
        public FolderBrowserDialog FolderBrowserDialog
        {
            get { return folderBrowserDialog; }
        }
        public SaveFileDialog SaveFileDialog
        {
            get { return saveFileDialog; }
        }

        public event EventHandler<EventArgs> PathChanged;
    }

    public enum FileSystemPathType
    {
        ExistingFile,
        Folder,
        NewFile,
    }



}

using Marson.Vault.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marson.Vault.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool dataIsChanged;
        private Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        private Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
        
        private SecureString lastPassword;
        private string lastFilePath;

        public MainWindow()
        {
            InitializeComponent();
            openFileDialog.DefaultExt = ".vault";
            openFileDialog.Filter = "Vault files (*.vault)|*.vault|All files|*.*";
            saveFileDialog.DefaultExt = openFileDialog.DefaultExt;
            saveFileDialog.Filter = openFileDialog.Filter;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateActions();
        }

        private void UpdateActions()
        {
            bool hasData = textBox.Text.Length > 0;
            saveButton.IsEnabled = hasData && dataIsChanged;
            saveAsButton.IsEnabled = hasData;
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSaveChanges() == false)
            {
                return;
            }

            var result = openFileDialog.ShowDialog(this);
            if (result.HasValue && result.Value)
            {
                var passwordDialog = new PasswordDialog();
                if (passwordDialog.ShowDialog() == true)
                {
                    LoadText(openFileDialog.FileName, passwordDialog.Password);
                }
            }
        }

        /// <summary>
        /// Returns true if we don't have to worry about saving changes, either because there are no changes
        /// to save or the user has already saved them.
        /// </summary>
        /// <returns></returns>
        private bool CheckSaveChanges()
        {
            if (!dataIsChanged)
            {
                return true;
            }

            var result =
                MessageBox.Show(this, "The current text has changed. Do you wish to save changes?", "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return SaveCurrent();
            }
            else if (result == MessageBoxResult.No)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SaveCurrent(bool forcePrompts = false)
        {
            var effectiveFilePath = lastFilePath;
            var effectivePassword = lastPassword;

            if (forcePrompts || effectiveFilePath == null)
            {
                if (saveFileDialog.ShowDialog(this) != true)
                {
                    return false;
                }
                effectiveFilePath = saveFileDialog.FileName;
            }

            if (forcePrompts || effectivePassword == null)
            {
                var passwordDialog = new PasswordDialog();
                if (passwordDialog.ShowDialog() != true)
                {
                    return false;
                }
                effectivePassword = passwordDialog.Password;
            }

            IVaultContent content = new VaultContent() { Text = textBox.Text };
            VaultSerializer.Serialize(effectiveFilePath, content, effectivePassword);

            lastFilePath = effectiveFilePath;
            lastPassword = effectivePassword;
            dataIsChanged = false;
            UpdateActions();

            return true;
        }

        private void LoadText(string filePath, SecureString password)
        {
            IVaultContent content;
            try
            {
                content = VaultSerializer.Deserialize(filePath, password);
            }
            catch (CryptographicException)
            {
                ShowError("Invalid password.");
                return;
            }
            catch (FormatException)
            {
                ShowError("Invalid file format.");
                return;
            }
            textBox.Text = content.Text;
            lastFilePath = filePath;
            lastPassword = password;

            dataIsChanged = false;
            UpdateActions();
        }

        private void ShowError(string text)
        {
            MessageBox.Show(this, text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataIsChanged = true;
            UpdateActions();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrent();
        }

        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrent(forcePrompts: true);
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckSaveChanges())
            {
                return;
            }

            textBox.Text = null;
            dataIsChanged = false;
            lastFilePath = null;
            lastPassword = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CheckSaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModSync
{
    public partial class AddGameDialog : Form
    {
        public string GameName { get; private set; }
        public DirectoryInfo GameLocation { get; private set; }
        public string ModFolderName { get; private set; }

        public AddGameDialog()
        {
            InitializeComponent();
        }

        private void ButtonSelectGameDirectory_Click(object sender, EventArgs e)
        {
            HandleGameLocationSelected(GameLocationDialog.ShowDialog(this));
        }

        private void HandleGameLocationSelected(DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.OK)
            {
                TextBoxGameLocation.Text = GameLocationDialog.SelectedPath;
            }
        }

        private void ButtonConfirmAddGame_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(TextBoxGameName.Text)) {
                ShowErrorDialog("Game name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBoxGameLocation.Text))
            {
                ShowErrorDialog("Game location cannot be empty.");
                return;
            }

            GameLocation = new DirectoryInfo(TextBoxGameLocation.Text);
            if (!GameLocation.Exists)
            {
                ShowErrorDialog("Game location does not exist.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBoxModFolderName.Text))
            {
                ShowErrorDialog("Mod folder name cannot be empty.");
                return;
            }

            GameName = TextBoxGameName.Text.Trim();
            if (SettingsManager.Settings.Games.ContainsKey(GameName))
            {
                ShowErrorDialog($"Game {GameName} already exists.");
                return;
            }

            GameName = TextBoxGameName.Text.Trim();
            ModFolderName = TextBoxModFolderName.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ShowErrorDialog(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GameLocationDialog_HelpRequest(object sender, EventArgs e)
        {

        }

        private void TextBoxModFolderName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

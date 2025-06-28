using ModSyncApp;
using ModSyncLib;

namespace ModSync
{
    public partial class FormGameList : System.Windows.Forms.Form
    {
        string? SelectedGame { get; set; }

        public FormGameList()
        {
            InitializeComponent();
        }

        private void ButtonAddGame_Click(object sender, EventArgs e)
        {
            using (var addGameDialog = new AddGameDialog())
            {
                var result = addGameDialog.ShowDialog(this);
                switch (result)
                {
                    case DialogResult.OK:
                        HandleAddGameDialogResult(addGameDialog);
                        break;
                }
                addGameDialog.Close();
            }
        }

        private void HandleAddGameDialogResult(AddGameDialog dialog)
        {
            var settings = SettingsManager.Settings;
            var gameConfig = new GameConfig
            {
                GameName = dialog.GameName,
                GameDirectory = dialog.GameLocation.FullName,
                ModFolderName = dialog.ModFolderName,
            };
            settings.Games.Add(gameConfig.GameName, gameConfig);
            SettingsManager.Settings = settings;
            UpdateGameList();

            GameListBox.SelectedItem = gameConfig.GameName;
        }

        private void GameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedGame = GameListBox.SelectedItem?.ToString();
            var selected = SelectedGame != null;
            ButtonRemoveGame.Enabled = selected;

            UpdateModPackList();
        }

        private void UpdateModPackList()
        {
            GroupBoxModPack.Enabled = SelectedGame != null;
            ModPackListBox.Items.Clear();
        }

        private void ButtonRemoveGame_Click(object sender, EventArgs e)
        {
            var selectedGame = GameListBox.SelectedItem?.ToString();
            if (selectedGame == null)
            {
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete {selectedGame} from your list of games?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            var settings = SettingsManager.Settings;
            settings.Games.Remove(selectedGame);
            SettingsManager.Settings = settings;
            UpdateGameList();
        }

        private void FormGameList_Load(object sender, EventArgs e)
        {
            UpdateGameList();
        }

        private void UpdateGameList()
        {
            var settings = SettingsManager.Settings;
            GameListBox.Items.Clear();
            foreach (var game in settings.Games.Keys.Order())
            {
                GameListBox.Items.Add(game);
            }

            if (GameListBox.Items.Count < 1)
            {
                ButtonRemoveGame.Enabled = false;
                GroupBoxModPack.Enabled = false;
            }
        }

        private void ButtonVerifyLocalFiles_Click(object sender, EventArgs e)
        {

        }

        private void ButtonDownloadAndSync_Click(object sender, EventArgs e)
        {

        }

        private void GroupBoxModPack_Enter(object sender, EventArgs e)
        {

        }

        private void ButtonRemoveModPack_Click(object sender, EventArgs e)
        {

        }

        private void ButtonNewModPack_Click(object sender, EventArgs e)
        {
            using (var addModPackDialog = new AddModPackDialog())
            {
                var result = addModPackDialog.ShowDialog(this);
                switch (result)
                {
                    case DialogResult.OK:
                        HandleAddModPackDialogResult(addModPackDialog);
                        break;
                }
                addModPackDialog.Close();
            }
        }

        private void HandleAddModPackDialogResult(AddModPackDialog dialog)
        {
            
        }
    }
}

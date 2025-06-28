using ModSyncApp;
using ModSyncLib;

namespace ModSync
{
    public partial class FormGameList : System.Windows.Forms.Form
    {
        string? SelectedGame { get; set; }
        Dictionary<string, string> SelectedGameModPack { get; set; } = new Dictionary<string, string>();

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
            ModPackListBox.Enabled = false;
            if (SelectedGame == null)
            {
                return;
            }

            ModPackListBox.Enabled = true;
            var settings = SettingsManager.Settings;
            var gameModPacks = settings.ModPacks.ContainsKey(SelectedGame) ? settings.ModPacks[SelectedGame] : new List<ModPack>();
            foreach (var modPack in gameModPacks.OrderBy(x => x.Name.ToLowerInvariant()))
            {
                ModPackListBox.Items.Add(modPack.Name);
            }
            if (SelectedGameModPack.ContainsKey(SelectedGame) && ModPackListBox.Items.Contains(SelectedGameModPack[SelectedGame]))
            {
                ModPackListBox.SelectedItem = SelectedGameModPack[SelectedGame];
            }
        }

        private void ButtonRemoveGame_Click(object sender, EventArgs e)
        {
            var selectedGame = GameListBox.SelectedItem?.ToString();
            if (selectedGame == null)
            {
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete {selectedGame} from your list of games? All stored mod packs for this game will also be removed.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            var settings = SettingsManager.Settings;
            settings.Games.Remove(selectedGame);
            settings.ModPacks.Remove(selectedGame);
            SettingsManager.Settings = settings;
            UpdateGameList();
            UpdateModPackList();
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

        private void ButtonDownloadAndSync_Click(object sender, EventArgs e)
        {

        }

        private void GroupBoxModPack_Enter(object sender, EventArgs e)
        {

        }

        private void ButtonRemoveModPack_Click(object sender, EventArgs e)
        {
            if (SelectedGame == null)
            {
                return;
            }

            var selectedModPack = ModPackListBox.SelectedItem?.ToString();
            if (selectedModPack == null)
            {
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete the \"{selectedModPack}\" mod pack?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            var settings = SettingsManager.Settings;
            settings.ModPacks[SelectedGame].RemoveAll(x => x.Name.Trim().ToLowerInvariant() == selectedModPack.Trim().ToLowerInvariant());
            SettingsManager.Settings = settings;
            ButtonRemoveModPack.Enabled = false;
            ButtonDownloadAndSync.Enabled = false;
            UpdateModPackList();
        }

        private void ButtonNewModPack_Click(object sender, EventArgs e)
        {
            using (var addModPackDialog = new AddModPackDialog())
            {
                var result = addModPackDialog.ShowDialog(this);
                switch (result)
                {
                    case DialogResult.OK:
                        HandleAddModPack(addModPackDialog.ModPack);
                        break;
                }
                addModPackDialog.Close();
            }
        }

        private void HandleAddModPack(ModPack modPack)
        {
            if (SelectedGame == null)
            {
                return;
            }

            var modPackName = modPack.Name.Trim();
            var settings = SettingsManager.Settings;
            if (!settings.ModPacks.ContainsKey(SelectedGame))
            {
                settings.ModPacks[SelectedGame] = new List<ModPack>();
            }
            if (settings.ModPacks[SelectedGame].Any(x => x.Name.Trim().ToLowerInvariant() == modPackName.ToLowerInvariant()))
            {
                ShowErrorDialog($"Mod pack \"{modPackName}\" already exists for game {SelectedGame}");
                return;
            }

            settings.ModPacks[SelectedGame].Add(modPack);
            SettingsManager.Settings = settings;

            SelectedGameModPack[SelectedGame] = modPackName;
            UpdateModPackList();
        }

        private void ShowErrorDialog(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ModPackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedGame == null)
            {
                return;
            }

            var selectedModPackName = ModPackListBox.SelectedItem?.ToString();
            ButtonRemoveModPack.Enabled = selectedModPackName != null;
            ButtonDownloadAndSync.Enabled = selectedModPackName != null;

            if (selectedModPackName != null)
            {
                SelectedGameModPack[SelectedGame] = selectedModPackName;
            }
        }
    }
}

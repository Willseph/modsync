using ModSyncApp;
using ModSyncLib;
using Newtonsoft.Json;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.ComponentModel;

namespace ModSync
{
    public partial class FormGameList : System.Windows.Forms.Form
    {
        static DirectoryInfo DownloadFolder
        {
            get => new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "archives"));
        }

        string? SelectedGame { get => GameListBox.SelectedItem?.ToString(); }
        string? SelectedModPack { get => ModPackListBox.SelectedItem?.ToString(); }
        BackgroundWorker downloadModPackWorker;

        public FormGameList()
        {
            InitializeComponent();
            downloadModPackWorker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
            };
            downloadModPackWorker.DoWork += DownloadModPackWorker_DoWork;
            downloadModPackWorker.ProgressChanged += DownloadModPackWorker_ProgressChanged;
            downloadModPackWorker.RunWorkerCompleted += DownloadModPackWorker_RunWorkerCompleted;
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
            var gameModPacks = settings.ModPacks.ContainsKey(SelectedGame) ? settings.ModPacks[SelectedGame] : new List<ReferencedModPack>();
            foreach (var modPack in gameModPacks.OrderBy(x => x.Name.ToLowerInvariant()))
            {
                ModPackListBox.Items.Add(modPack.Name);
            }

            ButtonRemoveModPack.Enabled = SelectedModPack != null;
            ButtonDownloadAndSync.Enabled = SelectedModPack != null;
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
            if (SelectedGame == null || SelectedModPack == null) return;

            var settings = SettingsManager.Settings;
            if (!settings.Games.ContainsKey(SelectedGame) || !settings.ModPacks.ContainsKey(SelectedGame))
            {
                ShowErrorDialog($"Could not find selected game {SelectedGame} in settings. Try removing and adding the game again.");
            }

            var game = settings.Games[SelectedGame];
            var modPack = settings.ModPacks[SelectedGame].FirstOrDefault(x => x.Name == SelectedModPack);
            if (modPack == null)
            {
                ShowErrorDialog($"Could not find selected mod pack \"{SelectedModPack}\" for {SelectedGame} in settings. Try removing and adding the mod pack again.");
            }

            var modFolder = new DirectoryInfo(Path.Combine(game.GameDirectory, game.ModFolderName));
            if (modFolder.Exists)
            {
                var result = MessageBox.Show($"This action will overwrite the contents of the mod directory for {game.GameName} at: {modFolder}\n\nAre you sure you want to continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            if (downloadModPackWorker.IsBusy)
            {
                ShowErrorDialog("Mod pack downloader is stuck. Try restarting the program.");
                return;
            }

            GroupBoxGameList.Enabled = false;
            GroupBoxModPack.Enabled = false;
            ProgressBar.Visible = true;
            LabelProgressStatus.Visible = true;

            var payload = new DownloadModPackWorkerPayload
            {
                Game = game,
                ModPack = modPack,
                ModFolder = modFolder,
            };
            downloadModPackWorker.RunWorkerAsync(payload);
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

        private void HandleAddModPack(ReferencedModPack modPack)
        {
            if (SelectedGame == null)
            {
                return;
            }

            var settings = SettingsManager.Settings;
            if (!settings.ModPacks.ContainsKey(SelectedGame))
            {
                settings.ModPacks[SelectedGame] = new List<ReferencedModPack>();
            }
            if (settings.ModPacks[SelectedGame].Any(x => x.ModPackUrl == modPack.ModPackUrl))
            {
                ShowErrorDialog($"Mod pack URL \"{modPack.ModPackUrl}\" already exists for game {SelectedGame}");
                return;
            }

            settings.ModPacks[SelectedGame].Add(modPack);
            SettingsManager.Settings = settings;

            UpdateModPackList();
        }

        private void ShowErrorDialog(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ModPackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRemoveModPack.Enabled = SelectedModPack != null;
            ButtonDownloadAndSync.Enabled = SelectedModPack != null;
        }

        private void DownloadModPackWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            GroupBoxGameList.Enabled = true;
            GroupBoxModPack.Enabled = true;
            ProgressBar.Visible = false;
            LabelProgressStatus.Visible = false;
            var previousSelected = SelectedModPack;
            UpdateModPackList();
            if(previousSelected != null && ModPackListBox.Items.Contains(previousSelected)) {
                ModPackListBox.SelectedItem = previousSelected;
            }

            if (e.Error != null)
            {
                ShowErrorDialog(e.Error.GetBaseException().Message);
                return;
            }

            MessageBox.Show($"Mod pack \"{previousSelected}\" successfully applied to {SelectedGame} files!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DownloadModPackWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            var state = e.UserState as ModPackDownloaderProgress;
            if (state == null)
            {
                return;
            }

            ProgressBar.Style = state.ProgressIndeterminate ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            if (!state.ProgressIndeterminate)
            {
                ProgressBar.Value = e.ProgressPercentage;
            }

            LabelProgressStatus.Text = state.Status ?? "";
        }

        private void DownloadModPackWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            var payload = e.Argument as DownloadModPackWorkerPayload ?? throw new Exception("Failed to start background worker.");

            // Re-fetching mod pack and updating cache
            var updatedPack = RefetchModPack(payload.ModPack, sender as BackgroundWorker);
            var settings = SettingsManager.Settings;
            foreach(var game in settings.ModPacks.Keys)
            {
                settings.ModPacks[game].RemoveAll(x => x.ModPackUrl == payload.ModPack.ModPackUrl);
                settings.ModPacks[game].Add(updatedPack);
            }
            SettingsManager.Settings = settings;

            var downloader = new ModPackDownloader(DownloadFolder);
            var downloadModPackTask = downloader.DownloadModPack(updatedPack, sender as BackgroundWorker);
            downloadModPackTask.Wait();

            if (!downloadModPackTask.IsCompletedSuccessfully)
            {
                throw downloadModPackTask.Exception ?? new Exception("An unknown error occurred while trying to sync this mod pack.");
            }

            var archive = downloadModPackTask.Result ?? throw new Exception("An unknown error occurred while trying to sync this mod pack.");

            UnpackToModFolder(archive, payload.Game, sender as BackgroundWorker);
        }

        private static ReferencedModPack RefetchModPack(ReferencedModPack modPack, BackgroundWorker? worker)
        {
            worker?.ReportProgress(0, new ModPackDownloaderProgress
            {
                ProgressIndeterminate = true,
                Status = "Fetching latest version of mod pack...",
            });

            var url = modPack.ModPackUrl;
            using var client = new HttpClient();
            var fetchTask = client.GetStringAsync(url);
            fetchTask.Wait();
            var json = fetchTask.Result ?? throw new Exception("Could not fetch up-to-date mod pack data.");
            var fetchedModPack = JsonConvert.DeserializeObject<ModPack>(json) ?? throw new Exception("Could not fetch up-to-date mod pack data.");
            ValidateModPack(fetchedModPack);
            fetchedModPack.Name = fetchedModPack.Name.Trim();
            fetchedModPack.CreatorName = fetchedModPack.CreatorName.Trim();
            fetchedModPack.RemoteUri = fetchedModPack.RemoteUri.Trim();
            return ReferencedModPack.FromModPack(url, fetchedModPack);
        }

        private static void ValidateModPack(ModPack modPack)
        {
            if (string.IsNullOrWhiteSpace(modPack.Name)
                || modPack.CreatorName == null
                || string.IsNullOrWhiteSpace(modPack.RemoteUri))
            {
                throw new Exception("Could not fetch up-to-date mod pack data.");
            }

            try
            {
                var uri = new Uri(modPack.RemoteUri);
            }
            catch
            {
                throw new Exception("Could not fetch up-to-date mod pack data.");
            }
        }

        private void UnpackToModFolder(FileInfo archive, GameConfig gameConfig, BackgroundWorker? worker)
        {
            var modFolder = new DirectoryInfo(Path.Combine(gameConfig.GameDirectory, gameConfig.ModFolderName));
            worker?.ReportProgress(0, new ModPackDownloaderProgress
            {
                ProgressIndeterminate = true,
                Status = $"Unpacking archive to {gameConfig.GameName} mod folder...",
            });

            if(!modFolder.Exists)
            {
                Directory.CreateDirectory(modFolder.FullName);
            }

            using Stream stream = File.OpenRead(archive.FullName);
            using var reader = ReaderFactory.Open(stream);
            reader.WriteAllToDirectory(modFolder.FullName, new ExtractionOptions {
                Overwrite = true,
                ExtractFullPath = true,
            });

            worker?.ReportProgress(100, new ModPackDownloaderProgress
            {
                ProgressIndeterminate = false,
                Status = $"Complete",
            });
        }
    }

    class DownloadModPackWorkerPayload
    {
        public GameConfig Game { get; set; }
        public ReferencedModPack ModPack { get; set; }
        public DirectoryInfo ModFolder { get; set; }
    }
}

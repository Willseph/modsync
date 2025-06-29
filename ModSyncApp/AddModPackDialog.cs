using ModSyncLib;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ModSyncApp
{
    public partial class AddModPackDialog : Form
    {
        public ModPack ModPack { get; private set; }
        BackgroundWorker worker;

        public AddModPackDialog()
        {
            InitializeComponent();
        }

        private void ButtonConfirmAddModPack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxModPackUrl.Text))
            {
                ShowErrorDialog("You must enter a valid mod pack URL.");
                return;
            }

            ProgressBarDownloadModPack.Visible = true;
            ButtonConfirmAddModPack.Enabled = false;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(TextBoxModPackUrl.Text.Trim());
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBarDownloadModPack.Visible = false;
            ButtonConfirmAddModPack.Enabled = true;

            if (e.Error != null)
            {
                ShowErrorDialog(e.Error.GetBaseException().Message);
                return;
            }

            ModPack = e.Result as ModPack;
            if (ModPack == null)
            {
                ShowErrorDialog("Could not fetch valid mod pack at provided URL.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                var uri = e.Argument?.ToString();
                if (uri == null)
                {
                    throw new Exception("Null URI");
                }

                var task = FetchModPack(uri);
                task.Wait(10000);

                if (task.IsFaulted)
                {
                    throw task.Exception?.GetBaseException() ?? new Exception("Unknown error");
                }

                if (task.Result == null)
                {
                    throw new Exception("Could not fetch valid mod pack at provided URL.");
                }

                e.Result = task.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<ModPack> FetchModPack(string uri)
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(uri);
            if (json == null)
            {
                throw new Exception("Error fetching JSON data at provided URL.");
            }
            var modPack = JsonConvert.DeserializeObject<ModPack>(json);
            if (modPack == null)
            {
                throw new Exception("Invalid mod pack data at provided URL.");
            }

            ValidateModPack(modPack);
            modPack.Name = modPack.Name.Trim();
            modPack.CreatorName = modPack.CreatorName.Trim();
            modPack.RemoteUri = modPack.RemoteUri.Trim();
            return modPack;
        }

        private void ValidateModPack(ModPack modPack)
        {
            if (string.IsNullOrWhiteSpace(modPack.Name)
                || modPack.CreatorName == null
                || string.IsNullOrWhiteSpace(modPack.RemoteUri)
                || string.IsNullOrWhiteSpace(modPack.FileHash))
            {
                throw new Exception("Invalid mod pack data at provided URL.");
            }

            try
            {
                var uri = new Uri(modPack.RemoteUri);
                if(!uri.IsFile)
                {
                    throw new Exception("Mod pack remote URI must be a file.");
                }
            }
            catch
            {
                throw new Exception("Invalid mod pack data at provided URL.");
            }
        }

        private void ShowErrorDialog(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

namespace ModSyncApp
{
    partial class AddModPackDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            TextBoxModPackUrl = new TextBox();
            ButtonConfirmAddModPack = new Button();
            ProgressBarDownloadModPack = new ProgressBar();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 0;
            label1.Text = "Modpack URL:";
            // 
            // TextBoxModPackUrl
            // 
            TextBoxModPackUrl.Location = new Point(12, 27);
            TextBoxModPackUrl.Name = "TextBoxModPackUrl";
            TextBoxModPackUrl.Size = new Size(530, 23);
            TextBoxModPackUrl.TabIndex = 1;
            // 
            // ButtonConfirmAddModPack
            // 
            ButtonConfirmAddModPack.Location = new Point(434, 66);
            ButtonConfirmAddModPack.Name = "ButtonConfirmAddModPack";
            ButtonConfirmAddModPack.Size = new Size(108, 23);
            ButtonConfirmAddModPack.TabIndex = 2;
            ButtonConfirmAddModPack.Text = "Add Mod Pack";
            ButtonConfirmAddModPack.UseVisualStyleBackColor = true;
            ButtonConfirmAddModPack.Click += ButtonConfirmAddModPack_Click;
            // 
            // ProgressBarDownloadModPack
            // 
            ProgressBarDownloadModPack.Location = new Point(12, 66);
            ProgressBarDownloadModPack.Name = "ProgressBarDownloadModPack";
            ProgressBarDownloadModPack.Size = new Size(369, 23);
            ProgressBarDownloadModPack.Style = ProgressBarStyle.Marquee;
            ProgressBarDownloadModPack.TabIndex = 3;
            ProgressBarDownloadModPack.Visible = false;
            // 
            // AddModPackDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 101);
            Controls.Add(ProgressBarDownloadModPack);
            Controls.Add(ButtonConfirmAddModPack);
            Controls.Add(TextBoxModPackUrl);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddModPackDialog";
            Text = "AddModPackDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TextBoxModPackUrl;
        private Button ButtonConfirmAddModPack;
        private ProgressBar ProgressBarDownloadModPack;
    }
}
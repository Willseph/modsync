namespace ModSync
{
    partial class AddGameDialog
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
            TextBoxGameName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            TextBoxGameLocation = new TextBox();
            ButtonSelectGameDirectory = new Button();
            label3 = new Label();
            TextBoxModFolderName = new TextBox();
            ButtonConfirmAddGame = new Button();
            GameLocationDialog = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // TextBoxGameName
            // 
            TextBoxGameName.Location = new Point(120, 12);
            TextBoxGameName.Name = "TextBoxGameName";
            TextBoxGameName.Size = new Size(284, 23);
            TextBoxGameName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 1;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 2;
            label2.Text = "Game location:";
            // 
            // TextBoxGameLocation
            // 
            TextBoxGameLocation.Location = new Point(120, 41);
            TextBoxGameLocation.Name = "TextBoxGameLocation";
            TextBoxGameLocation.Size = new Size(203, 23);
            TextBoxGameLocation.TabIndex = 3;
            TextBoxGameLocation.TextChanged += TextBoxModFolderName_TextChanged;
            // 
            // ButtonSelectGameDirectory
            // 
            ButtonSelectGameDirectory.Location = new Point(329, 41);
            ButtonSelectGameDirectory.Name = "ButtonSelectGameDirectory";
            ButtonSelectGameDirectory.Size = new Size(75, 23);
            ButtonSelectGameDirectory.TabIndex = 4;
            ButtonSelectGameDirectory.Text = "Select";
            ButtonSelectGameDirectory.UseVisualStyleBackColor = true;
            ButtonSelectGameDirectory.Click += ButtonSelectGameDirectory_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(102, 15);
            label3.TabIndex = 5;
            label3.Text = "Mod folder name:";
            // 
            // TextBoxModFolderName
            // 
            TextBoxModFolderName.Location = new Point(120, 70);
            TextBoxModFolderName.Name = "TextBoxModFolderName";
            TextBoxModFolderName.Size = new Size(284, 23);
            TextBoxModFolderName.TabIndex = 6;
            TextBoxModFolderName.Text = "Mods";
            // 
            // ButtonConfirmAddGame
            // 
            ButtonConfirmAddGame.Location = new Point(329, 108);
            ButtonConfirmAddGame.Name = "ButtonConfirmAddGame";
            ButtonConfirmAddGame.Size = new Size(75, 23);
            ButtonConfirmAddGame.TabIndex = 7;
            ButtonConfirmAddGame.Text = "Add Game";
            ButtonConfirmAddGame.UseVisualStyleBackColor = true;
            ButtonConfirmAddGame.Click += ButtonConfirmAddGame_Click;
            // 
            // GameLocationDialog
            // 
            GameLocationDialog.RootFolder = Environment.SpecialFolder.ProgramFilesX86;
            GameLocationDialog.ShowNewFolderButton = false;
            GameLocationDialog.HelpRequest += GameLocationDialog_HelpRequest;
            // 
            // AddGameDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(416, 143);
            Controls.Add(ButtonConfirmAddGame);
            Controls.Add(TextBoxModFolderName);
            Controls.Add(label3);
            Controls.Add(ButtonSelectGameDirectory);
            Controls.Add(TextBoxGameLocation);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TextBoxGameName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddGameDialog";
            Text = "Add Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TextBoxGameName;
        private Label label1;
        private Label label2;
        private TextBox TextBoxGameLocation;
        private Button ButtonSelectGameDirectory;
        private Label label3;
        private TextBox TextBoxModFolderName;
        private Button ButtonConfirmAddGame;
        private FolderBrowserDialog GameLocationDialog;
    }
}
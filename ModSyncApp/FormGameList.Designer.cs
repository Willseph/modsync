namespace ModSync
{
    partial class FormGameList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GameListBox = new ListBox();
            ButtonAddGame = new Button();
            ButtonRemoveModPack = new Button();
            GroupBoxGameList = new GroupBox();
            ButtonRemoveGame = new Button();
            GroupBoxModPack = new GroupBox();
            ButtonDownloadAndSync = new Button();
            ModPackListBox = new ListBox();
            ButtonNewModPack = new Button();
            ProgressBar = new ProgressBar();
            LabelProgressStatus = new Label();
            GroupBoxGameList.SuspendLayout();
            GroupBoxModPack.SuspendLayout();
            SuspendLayout();
            // 
            // GameListBox
            // 
            GameListBox.FormattingEnabled = true;
            GameListBox.ItemHeight = 15;
            GameListBox.Location = new Point(6, 22);
            GameListBox.Name = "GameListBox";
            GameListBox.Size = new Size(180, 229);
            GameListBox.Sorted = true;
            GameListBox.TabIndex = 0;
            GameListBox.SelectedIndexChanged += GameListBox_SelectedIndexChanged;
            // 
            // ButtonAddGame
            // 
            ButtonAddGame.Location = new Point(192, 22);
            ButtonAddGame.Name = "ButtonAddGame";
            ButtonAddGame.Size = new Size(98, 23);
            ButtonAddGame.TabIndex = 2;
            ButtonAddGame.Text = "Add Game";
            ButtonAddGame.UseVisualStyleBackColor = true;
            ButtonAddGame.Click += ButtonAddGame_Click;
            // 
            // ButtonRemoveModPack
            // 
            ButtonRemoveModPack.Enabled = false;
            ButtonRemoveModPack.Location = new Point(335, 51);
            ButtonRemoveModPack.Name = "ButtonRemoveModPack";
            ButtonRemoveModPack.Size = new Size(156, 23);
            ButtonRemoveModPack.TabIndex = 3;
            ButtonRemoveModPack.Text = "Remove Mod Pack";
            ButtonRemoveModPack.UseVisualStyleBackColor = true;
            ButtonRemoveModPack.Click += ButtonRemoveModPack_Click;
            // 
            // GroupBoxGameList
            // 
            GroupBoxGameList.Controls.Add(ButtonRemoveGame);
            GroupBoxGameList.Controls.Add(GameListBox);
            GroupBoxGameList.Controls.Add(ButtonAddGame);
            GroupBoxGameList.Location = new Point(12, 12);
            GroupBoxGameList.Name = "GroupBoxGameList";
            GroupBoxGameList.Size = new Size(296, 260);
            GroupBoxGameList.TabIndex = 5;
            GroupBoxGameList.TabStop = false;
            GroupBoxGameList.Text = "Game list";
            // 
            // ButtonRemoveGame
            // 
            ButtonRemoveGame.Enabled = false;
            ButtonRemoveGame.Location = new Point(192, 51);
            ButtonRemoveGame.Name = "ButtonRemoveGame";
            ButtonRemoveGame.Size = new Size(98, 23);
            ButtonRemoveGame.TabIndex = 3;
            ButtonRemoveGame.Text = "Remove Game";
            ButtonRemoveGame.UseVisualStyleBackColor = true;
            ButtonRemoveGame.Click += ButtonRemoveGame_Click;
            // 
            // GroupBoxModPack
            // 
            GroupBoxModPack.Controls.Add(ButtonDownloadAndSync);
            GroupBoxModPack.Controls.Add(ModPackListBox);
            GroupBoxModPack.Controls.Add(ButtonNewModPack);
            GroupBoxModPack.Controls.Add(ButtonRemoveModPack);
            GroupBoxModPack.Enabled = false;
            GroupBoxModPack.Location = new Point(327, 12);
            GroupBoxModPack.Name = "GroupBoxModPack";
            GroupBoxModPack.Size = new Size(497, 260);
            GroupBoxModPack.TabIndex = 6;
            GroupBoxModPack.TabStop = false;
            GroupBoxModPack.Text = "Mod packs";
            GroupBoxModPack.Enter += GroupBoxModPack_Enter;
            // 
            // ButtonDownloadAndSync
            // 
            ButtonDownloadAndSync.Enabled = false;
            ButtonDownloadAndSync.Location = new Point(335, 80);
            ButtonDownloadAndSync.Name = "ButtonDownloadAndSync";
            ButtonDownloadAndSync.Size = new Size(156, 23);
            ButtonDownloadAndSync.TabIndex = 5;
            ButtonDownloadAndSync.Text = "Download and Sync";
            ButtonDownloadAndSync.UseVisualStyleBackColor = true;
            ButtonDownloadAndSync.Click += ButtonDownloadAndSync_Click;
            // 
            // ModPackListBox
            // 
            ModPackListBox.Enabled = false;
            ModPackListBox.FormattingEnabled = true;
            ModPackListBox.ItemHeight = 15;
            ModPackListBox.Location = new Point(6, 22);
            ModPackListBox.Name = "ModPackListBox";
            ModPackListBox.Size = new Size(323, 229);
            ModPackListBox.Sorted = true;
            ModPackListBox.TabIndex = 0;
            ModPackListBox.SelectedIndexChanged += ModPackListBox_SelectedIndexChanged;
            ModPackListBox.LostFocus += ModPackListBox_SelectedIndexChanged;
            // 
            // ButtonNewModPack
            // 
            ButtonNewModPack.Location = new Point(335, 22);
            ButtonNewModPack.Name = "ButtonNewModPack";
            ButtonNewModPack.Size = new Size(156, 23);
            ButtonNewModPack.TabIndex = 2;
            ButtonNewModPack.Text = "Add Mod Pack";
            ButtonNewModPack.UseVisualStyleBackColor = true;
            ButtonNewModPack.Click += ButtonNewModPack_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(12, 300);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(812, 23);
            ProgressBar.Style = ProgressBarStyle.Marquee;
            ProgressBar.TabIndex = 7;
            ProgressBar.Visible = false;
            // 
            // LabelProgressStatus
            // 
            LabelProgressStatus.AutoSize = true;
            LabelProgressStatus.Location = new Point(12, 282);
            LabelProgressStatus.Name = "LabelProgressStatus";
            LabelProgressStatus.Size = new Size(0, 15);
            LabelProgressStatus.TabIndex = 8;
            LabelProgressStatus.Visible = false;
            // 
            // FormGameList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 335);
            Controls.Add(LabelProgressStatus);
            Controls.Add(ProgressBar);
            Controls.Add(GroupBoxModPack);
            Controls.Add(GroupBoxGameList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormGameList";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "ModSync";
            Load += FormGameList_Load;
            GroupBoxGameList.ResumeLayout(false);
            GroupBoxModPack.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox GameListBox;
        private Button ButtonAddGame;
        private Button ButtonRemoveModPack;
        private GroupBox GroupBoxGameList;
        private GroupBox GroupBoxModPack;
        private ListBox ModPackListBox;
        private Button ButtonNewModPack;
        private Button ButtonRemoveGame;
        private Button ButtonDownloadAndSync;
        private ProgressBar ProgressBar;
        private Label LabelProgressStatus;
    }
}
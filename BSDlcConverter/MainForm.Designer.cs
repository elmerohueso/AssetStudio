namespace DlcConverter
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dlcFolderBox = new System.Windows.Forms.TextBox();
            this.dlcFolderBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.sharedAssetsBrowse = new System.Windows.Forms.Button();
            this.sharedAssetsBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.activityBar = new System.Windows.Forms.ProgressBar();
            this.statusMessage = new System.Windows.Forms.Label();
            this.outputBrowseButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Beat Saber DLC Folder";
            // 
            // dlcFolderBox
            // 
            this.dlcFolderBox.Location = new System.Drawing.Point(135, 10);
            this.dlcFolderBox.Name = "dlcFolderBox";
            this.dlcFolderBox.Size = new System.Drawing.Size(200, 20);
            this.dlcFolderBox.TabIndex = 1;
            this.dlcFolderBox.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber\\DLC";
            // 
            // dlcFolderBrowse
            // 
            this.dlcFolderBrowse.Location = new System.Drawing.Point(341, 8);
            this.dlcFolderBrowse.Name = "dlcFolderBrowse";
            this.dlcFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.dlcFolderBrowse.TabIndex = 2;
            this.dlcFolderBrowse.Text = "Browse...";
            this.dlcFolderBrowse.UseVisualStyleBackColor = true;
            this.dlcFolderBrowse.Click += new System.EventHandler(this.dlcFolderBrowse_Click);
            // 
            // sharedAssetsBrowse
            // 
            this.sharedAssetsBrowse.Location = new System.Drawing.Point(341, 34);
            this.sharedAssetsBrowse.Name = "sharedAssetsBrowse";
            this.sharedAssetsBrowse.Size = new System.Drawing.Size(75, 23);
            this.sharedAssetsBrowse.TabIndex = 5;
            this.sharedAssetsBrowse.Text = "Browse...";
            this.sharedAssetsBrowse.UseVisualStyleBackColor = true;
            this.sharedAssetsBrowse.Click += new System.EventHandler(this.sharedAssetsBrowse_Click);
            // 
            // sharedAssetsBox
            // 
            this.sharedAssetsBox.Location = new System.Drawing.Point(135, 36);
            this.sharedAssetsBox.Name = "sharedAssetsBox";
            this.sharedAssetsBox.Size = new System.Drawing.Size(200, 20);
            this.sharedAssetsBox.TabIndex = 4;
            this.sharedAssetsBox.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber\\Beat Saber_Data\\sharedas" +
    "sets0.assets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "sharedassets0.assets";
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(341, 88);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 15;
            this.goButton.Text = "GO";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // activityBar
            // 
            this.activityBar.Location = new System.Drawing.Point(16, 88);
            this.activityBar.Margin = new System.Windows.Forms.Padding(2);
            this.activityBar.MarqueeAnimationSpeed = 0;
            this.activityBar.Name = "activityBar";
            this.activityBar.Size = new System.Drawing.Size(93, 13);
            this.activityBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.activityBar.TabIndex = 20;
            this.activityBar.Visible = false;
            // 
            // statusMessage
            // 
            this.statusMessage.AutoSize = true;
            this.statusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusMessage.Location = new System.Drawing.Point(113, 88);
            this.statusMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(88, 12);
            this.statusMessage.TabIndex = 19;
            this.statusMessage.Text = "Progress Messages";
            this.statusMessage.Visible = false;
            // 
            // outputBrowseButton
            // 
            this.outputBrowseButton.Location = new System.Drawing.Point(341, 60);
            this.outputBrowseButton.Name = "outputBrowseButton";
            this.outputBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.outputBrowseButton.TabIndex = 23;
            this.outputBrowseButton.Text = "Browse...";
            this.outputBrowseButton.UseVisualStyleBackColor = true;
            this.outputBrowseButton.Click += new System.EventHandler(this.outputBrowseButton_Click);
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(135, 62);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(200, 20);
            this.outputBox.TabIndex = 22;
            this.outputBox.Text = "D:\\Users\\Brendan\\Downloads\\Beat Saber\\export test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Output Folder";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 117);
            this.Controls.Add(this.outputBrowseButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activityBar);
            this.Controls.Add(this.statusMessage);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.sharedAssetsBrowse);
            this.Controls.Add(this.sharedAssetsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dlcFolderBrowse);
            this.Controls.Add(this.dlcFolderBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "BS DLC Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dlcFolderBox;
        private System.Windows.Forms.Button dlcFolderBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button sharedAssetsBrowse;
        private System.Windows.Forms.TextBox sharedAssetsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ProgressBar activityBar;
        private System.Windows.Forms.Label statusMessage;
        private System.Windows.Forms.Button outputBrowseButton;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Label label3;
    }
}
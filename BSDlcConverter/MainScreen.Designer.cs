namespace BSDlcConverter
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.statusMessage = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.activityBar = new System.Windows.Forms.ProgressBar();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.outputFolderBrowse = new System.Windows.Forms.Button();
            this.outputFolderBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sharedAssetsBrowse = new System.Windows.Forms.Button();
            this.sharedAssetsBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dlcFolderBrowse = new System.Windows.Forms.Button();
            this.dlcFolderBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statusMessage
            // 
            this.statusMessage.AutoSize = true;
            this.statusMessage.Location = new System.Drawing.Point(131, 90);
            this.statusMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(116, 15);
            this.statusMessage.TabIndex = 38;
            this.statusMessage.Text = "Progress Messages";
            this.statusMessage.Visible = false;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(340, 85);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 37;
            this.goButton.Text = "GO";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // activityBar
            // 
            this.activityBar.Location = new System.Drawing.Point(34, 90);
            this.activityBar.Margin = new System.Windows.Forms.Padding(2);
            this.activityBar.MarqueeAnimationSpeed = 0;
            this.activityBar.Name = "activityBar";
            this.activityBar.Size = new System.Drawing.Size(93, 13);
            this.activityBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.activityBar.TabIndex = 39;
            this.activityBar.Visible = false;
            // 
            // outputFolderBrowse
            // 
            this.outputFolderBrowse.Location = new System.Drawing.Point(340, 56);
            this.outputFolderBrowse.Name = "outputFolderBrowse";
            this.outputFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.outputFolderBrowse.TabIndex = 30;
            this.outputFolderBrowse.Text = "Browse...";
            this.outputFolderBrowse.UseVisualStyleBackColor = true;
            this.outputFolderBrowse.Click += new System.EventHandler(this.outputBrowseButton_Click);
            // 
            // outputFolderBox
            // 
            this.outputFolderBox.Location = new System.Drawing.Point(134, 58);
            this.outputFolderBox.Name = "outputFolderBox";
            this.outputFolderBox.Size = new System.Drawing.Size(200, 20);
            this.outputFolderBox.TabIndex = 29;
            this.outputFolderBox.Text = "C:\\ExportedDLC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Output Folder";
            // 
            // sharedAssetsBrowse
            // 
            this.sharedAssetsBrowse.Location = new System.Drawing.Point(340, 30);
            this.sharedAssetsBrowse.Name = "sharedAssetsBrowse";
            this.sharedAssetsBrowse.Size = new System.Drawing.Size(75, 23);
            this.sharedAssetsBrowse.TabIndex = 27;
            this.sharedAssetsBrowse.Text = "Browse...";
            this.sharedAssetsBrowse.UseVisualStyleBackColor = true;
            this.sharedAssetsBrowse.Click += new System.EventHandler(this.sharedAssetsBrowse_Click);
            // 
            // sharedAssetsBox
            // 
            this.sharedAssetsBox.Location = new System.Drawing.Point(134, 32);
            this.sharedAssetsBox.Name = "sharedAssetsBox";
            this.sharedAssetsBox.Size = new System.Drawing.Size(200, 20);
            this.sharedAssetsBox.TabIndex = 26;
            this.sharedAssetsBox.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber\\Beat Saber_Data\\sharedas" +
    "sets0.assets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "sharedassets0.assets";
            // 
            // dlcFolderBrowse
            // 
            this.dlcFolderBrowse.Location = new System.Drawing.Point(340, 4);
            this.dlcFolderBrowse.Name = "dlcFolderBrowse";
            this.dlcFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.dlcFolderBrowse.TabIndex = 24;
            this.dlcFolderBrowse.Text = "Browse...";
            this.dlcFolderBrowse.UseVisualStyleBackColor = true;
            this.dlcFolderBrowse.Click += new System.EventHandler(this.dlcFolderBrowse_Click);
            // 
            // dlcFolderBox
            // 
            this.dlcFolderBox.Location = new System.Drawing.Point(134, 6);
            this.dlcFolderBox.Name = "dlcFolderBox";
            this.dlcFolderBox.Size = new System.Drawing.Size(200, 20);
            this.dlcFolderBox.TabIndex = 23;
            this.dlcFolderBox.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber\\DLC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Beat Saber DLC Folder";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 116);
            this.Controls.Add(this.statusMessage);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.activityBar);
            this.Controls.Add(this.outputFolderBrowse);
            this.Controls.Add(this.outputFolderBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sharedAssetsBrowse);
            this.Controls.Add(this.sharedAssetsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dlcFolderBrowse);
            this.Controls.Add(this.dlcFolderBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.Text = "BS DLC Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusMessage;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ProgressBar activityBar;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button outputFolderBrowse;
        private System.Windows.Forms.TextBox outputFolderBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sharedAssetsBrowse;
        private System.Windows.Forms.TextBox sharedAssetsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dlcFolderBrowse;
        private System.Windows.Forms.TextBox dlcFolderBox;
        private System.Windows.Forms.Label label1;
    }
}
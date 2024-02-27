namespace SongDefinitionHelper
{
    partial class MainForm
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
            this.btnLoad = new Button();
            this.songList = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtInternalName = new TextBox();
            this.txtSongName = new TextBox();
            this.label7 = new Label();
            this.txtNameOverride = new TextBox();
            this.label6 = new Label();
            this.txtSongSubName = new TextBox();
            this.label3 = new Label();
            this.txtAuthorName = new TextBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtSongPack = new TextBox();
            this.txtBpm = new TextBox();
            this.btnSave = new Button();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.tableLayoutPanel3 = new TableLayoutPanel();
            this.tableLayoutPanel4 = new TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLoad.Location = new Point(326, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(51, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += this.btnLoad_Click;
            // 
            // songList
            // 
            this.songList.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
            this.songList.Dock = DockStyle.Fill;
            this.songList.Location = new Point(10, 10);
            this.songList.Name = "songList";
            this.songList.Size = new Size(373, 444);
            this.songList.TabIndex = 1;
            this.songList.UseCompatibleStateImageBehavior = false;
            this.songList.View = View.Details;
            this.songList.SelectedIndexChanged += this.songList_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 340;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtInternalName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSongName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtNameOverride, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtSongSubName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtAuthorName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtSongPack, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtBpm, 1, 4);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Margin = new Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.Size = new Size(380, 421);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = DockStyle.Left;
            this.label1.Location = new Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(82, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Internal Name";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = DockStyle.Left;
            this.label2.Location = new Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new Size(69, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Song Name";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtInternalName
            // 
            this.txtInternalName.Dock = DockStyle.Fill;
            this.txtInternalName.Location = new Point(101, 3);
            this.txtInternalName.Name = "txtInternalName";
            this.txtInternalName.ReadOnly = true;
            this.txtInternalName.Size = new Size(276, 23);
            this.txtInternalName.TabIndex = 5;
            // 
            // txtSongName
            // 
            this.txtSongName.Dock = DockStyle.Fill;
            this.txtSongName.Location = new Point(101, 32);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new Size(276, 23);
            this.txtSongName.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = DockStyle.Left;
            this.label7.Location = new Point(3, 174);
            this.label7.Name = "label7";
            this.label7.Size = new Size(87, 29);
            this.label7.TabIndex = 11;
            this.label7.Text = "Name Override";
            this.label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNameOverride
            // 
            this.txtNameOverride.Dock = DockStyle.Fill;
            this.txtNameOverride.Location = new Point(101, 177);
            this.txtNameOverride.Name = "txtNameOverride";
            this.txtNameOverride.Size = new Size(276, 23);
            this.txtNameOverride.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = DockStyle.Left;
            this.label6.Location = new Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new Size(92, 29);
            this.label6.TabIndex = 10;
            this.label6.Text = "Song Sub Name";
            this.label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSongSubName
            // 
            this.txtSongSubName.Dock = DockStyle.Fill;
            this.txtSongSubName.Location = new Point(101, 61);
            this.txtSongSubName.Name = "txtSongSubName";
            this.txtSongSubName.Size = new Size(276, 23);
            this.txtSongSubName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = DockStyle.Left;
            this.label3.Location = new Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new Size(79, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Author Name";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtAuthorName
            // 
            this.txtAuthorName.Dock = DockStyle.Fill;
            this.txtAuthorName.Location = new Point(101, 90);
            this.txtAuthorName.Name = "txtAuthorName";
            this.txtAuthorName.Size = new Size(276, 23);
            this.txtAuthorName.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = DockStyle.Left;
            this.label5.Location = new Point(3, 145);
            this.label5.Name = "label5";
            this.label5.Size = new Size(62, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Song Pack";
            this.label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = DockStyle.Left;
            this.label4.Location = new Point(3, 116);
            this.label4.Name = "label4";
            this.label4.Size = new Size(32, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "BPM";
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSongPack
            // 
            this.txtSongPack.Dock = DockStyle.Fill;
            this.txtSongPack.Location = new Point(101, 148);
            this.txtSongPack.Name = "txtSongPack";
            this.txtSongPack.Size = new Size(276, 23);
            this.txtSongPack.TabIndex = 9;
            // 
            // txtBpm
            // 
            this.txtBpm.Dock = DockStyle.Fill;
            this.txtBpm.Location = new Point(101, 119);
            this.txtBpm.Name = "txtBpm";
            this.txtBpm.Size = new Size(276, 23);
            this.txtBpm.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnSave.Location = new Point(269, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(51, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += this.btnSave_Click;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.songList, 0, 0);
            this.tableLayoutPanel2.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Location = new Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new Padding(7);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new Size(773, 464);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Dock = DockStyle.Fill;
            this.tableLayoutPanel3.Location = new Point(386, 7);
            this.tableLayoutPanel3.Margin = new Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            this.tableLayoutPanel3.Size = new Size(380, 450);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnLoad, 1, 0);
            this.tableLayoutPanel4.Dock = DockStyle.Bottom;
            this.tableLayoutPanel4.Location = new Point(0, 421);
            this.tableLayoutPanel4.Margin = new Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new Size(380, 29);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(773, 464);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += this.Form1_Load;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Button btnLoad;
        private ListView songList;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtInternalName;
        private TextBox txtSongName;
        private TextBox txtAuthorName;
        private TextBox txtBpm;
        private TextBox txtSongPack;
        private Label label6;
        private Label label7;
        private TextBox txtSongSubName;
        private TextBox txtNameOverride;
        private ColumnHeader columnHeader1;
        private Button btnSave;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
    }
}

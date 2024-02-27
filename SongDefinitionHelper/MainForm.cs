using BSDlcConverter.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SongDefinitionHelper
{
    public partial class MainForm : Form
    {
        private SongPackDefinitions songDefinitions;
        private string songDefinitionsPath = string.Empty;
        public MainForm()
        {
            InitializeComponent();
        }

        public Song? GetSelectedSong()
        {
            if (songList.SelectedItems.Count == 0)
            {
                return null;
            }

            var selectedItem = songList.SelectedItems[0];

            if (selectedItem.Tag is Song song)
            {
                return song;
            }

            return null;
        }

        private void songList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableChangeEvents();

            if (songList.SelectedItems.Count > 0)
            {
                var song = GetSelectedSong();

                txtInternalName.Text = song.internalName;
                txtSongName.Text = song.songName;
                txtAuthorName.Text = song.songAuthorName;
                txtBpm.Text = song.beatsPerMinute;
                txtSongPack.Text = song.songPack;
                txtSongSubName.Text = song.songSubName;
                txtNameOverride.Text = song.nameOverride;
                tableLayoutPanel1.Enabled = true;

                EnableChangeEvents();
            }
            else
            {
                txtInternalName.Text = string.Empty;
                txtSongName.Text = string.Empty;
                txtAuthorName.Text = string.Empty;
                txtBpm.Text = string.Empty;
                txtSongPack.Text = string.Empty;
                txtSongSubName.Text = string.Empty;
                txtNameOverride.Text = string.Empty;
                tableLayoutPanel1.Enabled = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dlcFiles = new List<string>();

            using (var openDefinitionsDialog = new OpenFileDialog() { FileName = Program.songDefinitionsPath, Title = "Select SongPackDefinitions.json", Filter = "SongPackDefinitions.json|SongPackDefinitions.json" })
            {
                songList.Items.Clear();
                if (openDefinitionsDialog.FileName == string.Empty || !File.Exists(openDefinitionsDialog.FileName))
                {
                    if ((string.IsNullOrWhiteSpace(Program.songDefinitionsPath) || openDefinitionsDialog.ShowDialog() != DialogResult.OK) || openDefinitionsDialog.FileName == string.Empty || !File.Exists(openDefinitionsDialog.FileName))
                    {
                        throw new FileNotFoundException(openDefinitionsDialog.FileName);
                    }
                }


                songDefinitionsPath = openDefinitionsDialog.FileName;

                using (var definitionFile = File.OpenRead(songDefinitionsPath))
                {
                    songDefinitions = JsonSerializer.Deserialize<SongPackDefinitions>(definitionFile);
                }

                if (songDefinitions == null)
                {
                    throw new FileFormatException(songDefinitionsPath);
                }
            }

            foreach (var song in songDefinitions.songs)
            {
                var item = new ListViewItem(song.internalName)
                {
                    Tag = song
                };

                songList.Items.Add(item);
            }

            using (var browseDlcDialog = new FolderBrowserDialog() { SelectedPath = Program.dlcPath, Description = "Select the DLC Beat Saber DLC Levels folder." })
            {
                if (browseDlcDialog.SelectedPath == string.Empty || !Directory.Exists(browseDlcDialog.SelectedPath))
                {
                    if ((string.IsNullOrWhiteSpace(Program.dlcPath) || browseDlcDialog.ShowDialog() != DialogResult.OK) || browseDlcDialog.SelectedPath == string.Empty || !Directory.Exists(browseDlcDialog.SelectedPath))
                    {
                        throw new DirectoryNotFoundException(browseDlcDialog.SelectedPath);
                    }
                }


                foreach (var file in Directory.GetDirectories(browseDlcDialog.SelectedPath, "*", SearchOption.TopDirectoryOnly))
                {
                    var fileName = Path.GetFileName(file);
                    dlcFiles.Add(fileName);

                    if (!songDefinitions.songs.Where(song => song.internalName == fileName).Any())
                    {
                        songList.Items.Add(new ListViewItem(fileName)
                        {
                            Tag = new Song()
                            {
                                internalName = fileName,
                                songName = fileName,
                                songAuthorName = null,
                                beatsPerMinute = "0",
                                songPack = null,
                                songSubName = null,
                                nameOverride = null
                            },
                            ForeColor = Color.Red
                        });
                    }
                }
            }
        }

        private void EnableChangeEvents()
        {
            txtInternalName.TextChanged += txtInternalName_TextChanged;
            txtSongName.TextChanged += txtSongName_TextChanged;
            txtAuthorName.TextChanged += txtAuthorName_TextChanged;
            txtBpm.TextChanged += txtBpm_TextChanged;
            txtSongPack.TextChanged += txtSongPack_TextChanged;
            txtSongSubName.TextChanged += txtSongSubName_TextChanged;
            txtNameOverride.TextChanged += txtNameOverride_TextChanged;
        }

        private void DisableChangeEvents()
        {
            txtInternalName.TextChanged -= txtInternalName_TextChanged;
            txtSongName.TextChanged -= txtSongName_TextChanged;
            txtAuthorName.TextChanged -= txtAuthorName_TextChanged;
            txtBpm.TextChanged -= txtBpm_TextChanged;
            txtSongPack.TextChanged -= txtSongPack_TextChanged;
            txtSongSubName.TextChanged -= txtSongSubName_TextChanged;
            txtNameOverride.TextChanged -= txtNameOverride_TextChanged;
        }

        private void txtInternalName_TextChanged(object sender, EventArgs e)
        {
            // This shouldn't ever happen.
            throw new NotImplementedException();

            GetSelectedSong().internalName = txtInternalName.Text;
        }

        private void txtSongName_TextChanged(object sender, EventArgs e)
        {
            GetSelectedSong().songName = txtSongName.Text;
        }

        private void txtAuthorName_TextChanged(object sender, EventArgs e)
        {
            GetSelectedSong().songAuthorName = txtAuthorName.Text;
        }

        private void txtBpm_TextChanged(object sender, EventArgs e)
        {
            GetSelectedSong().beatsPerMinute = txtBpm.Text;
        }

        private void txtSongPack_TextChanged(object sender, EventArgs e)
        {
            GetSelectedSong().songPack = txtSongPack.Text;
        }

        private void txtSongSubName_TextChanged(object sender, EventArgs e)
        {
            GetSelectedSong().songSubName = txtSongSubName.Text;
        }

        private void txtNameOverride_TextChanged(object sender, EventArgs e)
        {
            GetSelectedSong().nameOverride = txtNameOverride.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnLoad_Click(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var songs = new List<Song>();

            foreach (ListViewItem item in songList.Items)
            {
                if (item.Tag is Song song)
                {
                    songs.Add(song);
                }
            }

            songDefinitions.songs = songs.ToArray();

            using (var saveDialog = new SaveFileDialog() { Filter = "*.json|*.json", FileName = songDefinitionsPath })
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    songDefinitions.songs = songs.ToArray();
                    using (var file = File.Create(saveDialog.FileName))
                    {
                        var options = new JsonSerializerOptions()
                        {
                            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                            WriteIndented = true,
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        };

                        JsonSerializer.Serialize(file, songDefinitions, options);
                        file.Write(Encoding.ASCII.GetBytes("\r\n"));
                    }
                }
            }
        }
    }
}

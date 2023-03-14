using DlcConverter.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSDlcConverter
{
    public partial class MainScreen : Form
    {
        public static string tempPath;
        public static string outputPath;
        public static string spriteFolder;
        public static string audioClipFolder;
        public static string monoBehaviourFolder;
        public MainScreen()
        {
            InitializeComponent();
        }
        private void dlcFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                dlcFolderBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void sharedAssetsBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "sharedassets0.assets | sharedassets0.assets";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                sharedAssetsBox.Text = openFileDialog.FileName;
        }

        private void outputBrowseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                outputFolderBox.Text = folderBrowserDialog.SelectedPath;
        }
        private async void goButton_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls.Cast<Control>().Where(c => c is Button || c is TextBox || c is Label))
                c.Enabled = false;
            Assembly assembly = Assembly.GetExecutingAssembly();
            outputPath = outputFolderBox.Text;
            tempPath = Path.Combine(outputPath, "_temp");
            spriteFolder = Path.Combine(tempPath, "Sprite");
            audioClipFolder = Path.Combine(tempPath, "AudioClip");
            monoBehaviourFolder = Path.Combine(tempPath, "MonoBehaviour");
            clearTemp();
            activityBar.Visible = true;
            statusMessage.Visible = true;
            string fileIn = sharedAssetsBox.Text;
            bool audio = false;
            bool json = false;
            bool sprite = true;
            var progressAmount = new Progress<int>(status =>
            {
                activityBar.Value = status;
            });
            var progressMessage = new Progress<string>(status =>
            {
                statusMessage.Text = status;
            });
            await Task.Run(() => AssetHelper.prepareAssets(fileIn, tempPath, audio, json, sprite, progressMessage, progressAmount));
            audio = true;
            json = true;
            sprite = false;
            string dlcFolder = dlcFolderBox.Text;
            var dlcFiles = Directory.EnumerateFiles(dlcFolder, "*.*", SearchOption.AllDirectories);
            int i = 0;
            foreach (string dlcFile in dlcFiles)
            {
                await Task.Run(() => AssetHelper.prepareAssets(dlcFile, tempPath, audio, json, sprite, progressMessage, null));
                i++;
                activityBar.Value = i * 100 / dlcFiles.Count();
            }
            statusMessage.Text = $"{dlcFiles.Count()} exported";
            statusMessage.Visible = true;
            await Task.Run(() => makeCustomSongs(progressMessage, progressAmount));
            clearTemp();
            foreach (Control c in this.Controls.Cast<Control>().Where(c => c is Button || c is TextBox || c is Label))
                c.Enabled = true;
            Process.Start("explorer.exe", outputPath);
            activityBar.Visible = false;
        }

        private static void makeCustomSongs(IProgress<string> progressMessage, IProgress<int> progressAmount)
        {
            List<SongModel> dlcToConvert = getAvailableDlc();
            progressMessage?.Report($"Found {dlcToConvert.Count}");
            int i = 1;
            foreach (SongModel song in dlcToConvert)
            {
                progressAmount?.Report(i * 100 / dlcToConvert.Count());
                progressMessage?.Report($"Converting \"{song.songName}\"");
                string tempFolder = stageSongFiles(song);
                zipSong(song, tempFolder);
                i++;
            }
            progressMessage?.Report($"{dlcToConvert.Count} songs converted");
        }

        private static List<SongModel> getAvailableDlc()
        {

            DirectoryInfo spriteDirectoryInfo = new DirectoryInfo(spriteFolder);
            DirectoryInfo audioClipDirectoryInfo = new DirectoryInfo(audioClipFolder);
            DirectoryInfo monoBehaviourDirectoryInfo = new DirectoryInfo(monoBehaviourFolder);
            SongPackDefinitions possibleDlc = getSongPackDefinitions();
            List<SongModel> availableDlc = new List<SongModel>();
            foreach (Song dlcSong in possibleDlc.songs)
            {
                Trace.WriteLine($"Looking for {dlcSong.internalName}");
                SongModel song = new SongModel();
                FileInfo songCoverFile = spriteDirectoryInfo.GetFiles().Where(file => file.Name == (dlcSong.nameOverride ?? dlcSong.internalName) + "Cover.jpg" || file.Name == dlcSong.internalName + ".jpg").LastOrDefault();
                if (songCoverFile == null)
                {
                    Trace.WriteLine($"Didn't find a cover file for {dlcSong.songName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    Trace.WriteLine($"Found cover {songCoverFile.FullName}");
                    song.coverPath = songCoverFile.FullName;
                }
                FileInfo songWavFile = audioClipDirectoryInfo.GetFiles().Where(file => file.Name == dlcSong.internalName + ".ogg").FirstOrDefault();
                if (songWavFile == null)
                {
                    Trace.WriteLine($"Didn't find a song file for {dlcSong.songName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    Trace.WriteLine($"Found song {songWavFile.FullName}");
                    song.songPath = songWavFile.FullName;
                }
                FileInfo levelDataFile = monoBehaviourDirectoryInfo.GetFiles().Where(file => file.Name == dlcSong.internalName + "BeatmapLevelData.json").FirstOrDefault();
                if (levelDataFile == null)
                {
                    Trace.WriteLine($"Didn't find a level data file for {dlcSong.songName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    Trace.WriteLine($"Found level data {levelDataFile.FullName}");
                    song.levelDataPath = levelDataFile.FullName;
                }
                List<FileInfo> songFiles = new List<FileInfo>();
                songFiles.AddRange(monoBehaviourDirectoryInfo.EnumerateFiles().Where(file => file.Name.StartsWith(dlcSong.internalName) && file.Name.EndsWith("BeatmapData.json")));
                if (songFiles.Count == 0)
                {
                    Trace.WriteLine($"Didn't find any beatmap files for {dlcSong.songName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    foreach (FileInfo songFile in songFiles)
                    {
                        Trace.WriteLine($"Found map {songFile.FullName}");
                        song.beatMapFiles.Add(songFile.FullName);
                    }
                }
                song.internalName = dlcSong.internalName;
                song.songName = dlcSong.songName;
                song.songSubName = dlcSong.songSubName;
                song.songAuthorName = dlcSong.songAuthorName;
                song.beatsPerMinute = double.Parse(dlcSong.beatsPerMinute);
                song.songPack = dlcSong.songPack;
                song.nameOverride = dlcSong.nameOverride;
                song.environmentName = getEnvironmentFromSongPack(song);
                FileInfo playlistCoverFile = spriteDirectoryInfo.GetFiles().Where(file => file.Name == getPlaylistCoverFromSongPack(song) + "Cover.jpg").LastOrDefault();
                if (playlistCoverFile != null)
                {
                    Trace.WriteLine($"Found playlist cover {songCoverFile.FullName}");
                    song.playlistCoverPath = playlistCoverFile.FullName;
                }
                availableDlc.Add(song);
            }
            return availableDlc;
        }
        private static SongPackDefinitions getSongPackDefinitions()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyPath = Path.GetDirectoryName(assembly.Location);
            string songPackDefinitionsPath = Path.Combine(assemblyPath, "SongPackDefinitions.json");
            SongPackDefinitions definitions = JsonConvert.DeserializeObject<SongPackDefinitions>(File.ReadAllText(songPackDefinitionsPath));
            return definitions;
        }
        private static string stageSongFiles(SongModel song)
        {
            string songTempFolder = Path.Combine(tempPath, song.internalName);
            Directory.CreateDirectory(songTempFolder);
            copySongCover(song, songTempFolder);
            moveAudio(song, songTempFolder);
            convertMoveBeatmaps(song, songTempFolder);
            createInfo(song, songTempFolder);
            return songTempFolder;
        }
        private static void clearTemp()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(tempPath);
            if (!Directory.Exists(directoryInfo.FullName))
                return;
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                dir.Delete(true);
            }
            Directory.Delete(tempPath);
        }

        private static void copySongCover(SongModel song, string songTempFolder)
        {
            Trace.WriteLine("Copying song cover");
            string destination = Path.Combine(songTempFolder, "cover.jpg");
            File.Copy(song.coverPath, destination);
        }
        private static void copyPlaylistCover(SongModel song, string playlistFolder)
        {
            string destination = Path.Combine(playlistFolder, "playlist.jpg");
            if (song.playlistCoverPath == null || File.Exists(destination))
                return;
            Trace.WriteLine("Copying playlist cover");
            File.Copy(song.playlistCoverPath, destination);
        }

        private static void moveAudio(SongModel song, string songTempFolder)
        {
            Trace.WriteLine("Moving song");
            string destination = Path.Combine(songTempFolder, "song.ogg");
            File.Move(song.songPath, destination);
        }

        private static void convertMoveBeatmaps(SongModel song, string songTempFolder)
        {
            Trace.WriteLine("Converting beatmaps");
            foreach (string beatMapFile in song.beatMapFiles)
            {
                string mapFileName = Path.GetFileName(beatMapFile).Replace(song.internalName, "");
                mapFileName = mapFileName.Replace("BeatmapData.json", ".dat");
                string mapPath = Path.Combine(songTempFolder, mapFileName);
                JObject jsonData = JObject.Parse(File.ReadAllText(beatMapFile));
                JObject mapData = JObject.Parse(jsonData["_jsonData"].ToString());
                if (mapData.ContainsKey("version"))
                    if (mapData["version"].ToString().StartsWith("3.2"))
                        mapData["version"] = "3.1.0";
                File.WriteAllText(mapPath, JsonConvert.SerializeObject(mapData));
            }
        }

        private static void createInfo(SongModel song, string songTempFolder)
        {
            Trace.WriteLine("Creating info.dat");
            JObject levelData = JObject.Parse(File.ReadAllText(song.levelDataPath));
            InfoModel info = levelData.ToObject<InfoModel>();
            string newFilename = Path.Combine(songTempFolder, "Info.dat");
            info._version = song.version;
            info._levelAuthorName = song.levelAuthorName;
            info._environmentName = song.environmentName;
            info._songName = song.songName;
            info._songSubName = song.songSubName;
            info._songAuthorName = song.songAuthorName;
            info._beatsPerMinute = (float)song.beatsPerMinute;

            Dictionary<int, string> difficulties = new Dictionary<int, string>(){
            {1, "Easy"},
            {3, "Normal"},
            {5, "Hard"},
            {7, "Expert"},
            {9, "ExpertPlus"}
        };
            foreach (_Difficultybeatmapsets difficultybeatmapset in info._difficultyBeatmapSets)
            {
                foreach (_Difficultybeatmaps difficultyBeatmap in difficultybeatmapset._difficultyBeatmaps)
                {
                    difficultyBeatmap._difficulty = difficulties[difficultyBeatmap._difficultyRank];
                    if (difficultybeatmapset._beatmapCharacteristicName == "Standard")
                        difficultyBeatmap._beatmapFilename = difficultyBeatmap._difficulty + ".dat";
                    else
                        difficultyBeatmap._beatmapFilename = difficultybeatmapset._beatmapCharacteristicName + difficultyBeatmap._difficulty + ".dat";
                }
            }
            File.WriteAllText(newFilename, JsonConvert.SerializeObject(info));
        }

        private static void zipSong(SongModel song, string songTempFolder)
        {
            Trace.WriteLine("Zipping song files");
            string songOutputFolder = Path.Combine(outputPath, song.songPack);
            Directory.CreateDirectory(songOutputFolder);
            copyPlaylistCover(song, songOutputFolder);
            string zipFile = Path.Combine(songOutputFolder, song.internalName + ".zip");
            if (File.Exists(zipFile))
                File.Delete(zipFile);
            string[] files = Directory.GetFiles(songTempFolder);
            bool isComplete = true;
            if (files.Where(x => x.EndsWith(".dat")).FirstOrDefault() == null)
                isComplete = false;
            if (files.Where(x => x.EndsWith(".jpg")).FirstOrDefault() == null)
                isComplete = false;
            if (files.Where(x => x.EndsWith(".ogg")).FirstOrDefault() == null)
                isComplete = false;
            using (ZipArchive archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                foreach (string file in files)
                {
                    archive.CreateEntryFromFile(file, Path.GetFileName(file));
                }
            }
            if (!isComplete)
                Trace.WriteLine("Song is likely missing files!");
        }
        private static string getPlaylistCoverFromSongPack(SongModel song)
        {
            string playlistCover = "";
            switch (song.songPack)
            {
                case "Billie Eilish":
                    playlistCover = "BillieEilish";
                    break;
                case "BTS":
                    playlistCover = "BTS";
                    break;
                case "Electronic Mixtape":
                    playlistCover = "EDM";
                    break;
                case "Fall Out Boy":
                    playlistCover = "FallOutBoy";
                    break;
                case "Green Day":
                    playlistCover = "GreenDay";
                    break;
                case "Imagine Dragons":
                    playlistCover = "ImagineDragons";
                    break;
                case "Interscope Mixtape":
                    playlistCover = "Interscope";
                    break;
                case "Lady Gaga":
                    playlistCover = "LadyGaga";
                    break;
                case "Linkin Park":
                    playlistCover = "LinkinPark";
                    break;
                case "Lizzo":
                    playlistCover = "Lizzo";
                    break;
                case "Monstercat Vol. 1":
                    playlistCover = "MonstercatVol1";
                    break;
                case "Monstercat X Rocket League":
                    playlistCover = "RocketLeague";
                    break;
                case "Panic! at the Disco":
                    playlistCover = "PanicAtTheDisco";
                    break;
                case "Skrillex":
                    playlistCover = "Skrillex";
                    break;
                case "Timbaland":
                    playlistCover = "Timbaland";
                    break;
                case "The Weeknd":
                    playlistCover = "TheWeeknd";
                    break;
                case "Rock Mixtape":
                    playlistCover = "RockMixtape";
                    break;
                default:
                    playlistCover = "BeatSaber";
                    break;
            }
            return playlistCover;
        }
        private static string getEnvironmentFromSongPack(SongModel song)
        {
            string environmentName = "";
            switch (song.songPack)
            {
                case "Billie Eilish":
                    environmentName = "BillieEnvironment";
                    break;
                case "BTS":
                    environmentName = "BTSEnvironment";
                    break;
                case "Electronic Mixtape":
                    environmentName = "EDMEnvironment";
                    break;
                case "Fall Out Boy":
                    environmentName = "PyroEnvironment";
                    break;
                case "Green Day":
                    environmentName = "GreenDayEnvironment";
                    break;
                case "Imagine Dragons":
                    environmentName = "DragonsEnvironment";
                    break;
                case "Interscope Mixtape":
                    environmentName = "InterscopeEnvironment";
                    break;
                case "Lady Gaga":
                    environmentName = "GagaEnvironment";
                    break;
                case "Linkin Park":
                    environmentName = "LinkinParkEnvironment";
                    break;
                case "Lizzo":
                    environmentName = "LizzoEnvironment";
                    break;
                case "Monstercat Vol. 1":
                    environmentName = "MonstercatEnvironment";
                    break;
                case "Monstercat X Rocket League":
                    environmentName = "RocketEnvironment";
                    break;
                case "Panic! at the Disco":
                    environmentName = "PanicEnvironment";
                    break;
                case "Skrillex":
                    environmentName = "SkrillexEnvironment";
                    break;
                case "Timbaland":
                    environmentName = "TimbalandEnvironment";
                    break;
                case "The Weeknd":
                    environmentName = "TheWeekndEnvironment";
                    break;
                case "Rock Mixtape":
                    environmentName = "RockMixtapeEnvironment";
                    break;
                default:
                    environmentName = "FitBeatEnvironment";
                    break;
            }
            return environmentName;
        }
    }
}

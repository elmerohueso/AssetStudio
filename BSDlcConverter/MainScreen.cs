using BSDlcConverter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSDlcConverter
{
    public partial class MainScreen : Form
    {
        public static readonly log4net.ILog mainLog = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string tempPath;
        public static string outputPath;
        public static string spriteFolder;
        public static string audioClipFolder;
        public static string monoBehaviourFolder;

        private static SongPackDefinitions songDefinitions = getSongPackDefinitions();
        public MainScreen()
        {
            mainLog.Debug("Starting up");
            InitializeComponent();
        }
        private void dlcFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            { }
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
            mainLog.Debug(
                $@"Starting conversion
                Beat Saber DLC Folder: {dlcFolderBox.Text}
                sharedassets0.assets: {sharedAssetsBox.Text}
                Output Folder: {outputFolderBox.Text}
                tempPath: {tempPath}
                spriteFolder: {spriteFolder}
                audioClipFolder: {audioClipFolder}
                monoBehaviourFolder: {monoBehaviourFolder}");
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
            mainLog.Debug("Conversion finished");
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
                mainLog.Debug($"Converting \"{song.songName}\"");
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
            List<SongModel> availableDlc = new List<SongModel>();
            foreach (Song dlcSong in songDefinitions.songs)
            {
                mainLog.Debug($"Looking for {dlcSong.internalName}");
                SongModel song = new SongModel();
                FileInfo songCoverFile = spriteDirectoryInfo.GetFiles().Where(file => file.Name.StartsWith(dlcSong.nameOverride ?? dlcSong.internalName) && file.Name.EndsWith(".jpg")).FirstOrDefault();
                if (songCoverFile == null)
                {
                    mainLog.Error($"Didn't find a cover file for {dlcSong.internalName}.");
                    //continue;
                }
                else
                {
                    mainLog.Debug($"Found cover {songCoverFile.FullName}");
                    song.coverPath = songCoverFile.FullName;
                }
                FileInfo songWavFile = audioClipDirectoryInfo.GetFiles().Where(file => file.Name == dlcSong.internalName + ".ogg").FirstOrDefault();
                if (songWavFile == null)
                {
                    mainLog.Error($"Didn't find a song file for {dlcSong.internalName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    mainLog.Debug($"Found song {songWavFile.FullName}");
                    song.songPath = songWavFile.FullName;
                }
                FileInfo levelDataFile = monoBehaviourDirectoryInfo.GetFiles().Where(file => file.Name == dlcSong.internalName + "BeatmapLevelData.json").FirstOrDefault();
                if (levelDataFile == null)
                {
                    mainLog.Error($"Didn't find a level data file for {dlcSong.internalName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    mainLog.Debug($"Found level data {levelDataFile.FullName}");
                    song.levelDataPath = levelDataFile.FullName;
                }
                List<FileInfo> songFiles = new List<FileInfo>();
                songFiles.AddRange(monoBehaviourDirectoryInfo.EnumerateFiles().Where(file => file.Name.StartsWith(dlcSong.internalName) && file.Name.EndsWith("BeatmapData.json")));
                if (songFiles.Count == 0)
                {
                    mainLog.Error($"Didn't find any beatmap files for {dlcSong.songName}. Song will be skipped.");
                    continue;
                }
                else
                {
                    foreach (FileInfo songFile in songFiles)
                    {
                        mainLog.Debug($"Found map {songFile.FullName}");
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
                    mainLog.Debug($"Found playlist cover {playlistCoverFile.FullName}");
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
            mainLog.Debug($"Reading Song Packs From {songPackDefinitionsPath}");
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
            string destination = Path.Combine(songTempFolder, "cover.jpg");
            mainLog.Debug($@"Copying song cover
                source: {song.coverPath}
                destination: {destination}");
            try
            {
                File.Copy(song.coverPath, destination);
            }
            catch (Exception ex)
            {
                mainLog.Error("Failed to copy image: " + ex);
            }
        }
        private static void copyPlaylistCover(SongModel song, string playlistFolder)
        {
            string destination = Path.Combine(playlistFolder, "playlist.jpg");
            if (song.playlistCoverPath == null || File.Exists(destination))
                return;

            mainLog.Debug($@"Copying playlist cover
                source: {song.playlistCoverPath}
                destination: {destination}");
            try
            {
                File.Copy(song.playlistCoverPath, destination);
            }
            catch (Exception ex)
            {
                mainLog.Error("Failed to copy image: " + ex);
            }
        }
        private static void moveAudio(SongModel song, string songTempFolder)
        {
            string destination = Path.Combine(songTempFolder, "song.ogg");
            mainLog.Debug($@"Moving song
                source: {song.songPath}
                destination: {destination}");
            try
            {
                File.Move(song.songPath, destination);
            }
            catch (Exception ex)
            {
                mainLog.Error("Failed to move song: " + ex);
            }
        }
        private static void convertMoveBeatmaps(SongModel song, string songTempFolder)
        {
            foreach (string beatMapFile in song.beatMapFiles)
            {
                string mapFileName = Path.GetFileName(beatMapFile).Replace(song.internalName, "");
                mapFileName = mapFileName.Replace("BeatmapData.json", ".dat");
                string mapPath = Path.Combine(songTempFolder, mapFileName);
                mainLog.Debug($@"Writing beatmap
                source: {beatMapFile}
                destination: {mapPath}");
                JObject jsonData = JObject.Parse(File.ReadAllText(beatMapFile));
                JObject mapData = JObject.Parse(jsonData["_jsonData"].ToString());
                if (mapData.ContainsKey("version"))
                    if (mapData["version"].ToString().StartsWith("3.2"))
                        mapData["version"] = "3.1.0";
                try
                {
                    File.WriteAllText(mapPath, JsonConvert.SerializeObject(mapData));
                }
                catch (Exception ex)
                {
                    mainLog.Error("Failed to write beatmap: " + ex);
                }
            }
        }
        private static void createInfo(SongModel song, string songTempFolder)
        {
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
            mainLog.Debug($"Writing \"{newFilename}\"");
            try
            {
                File.WriteAllText(newFilename, JsonConvert.SerializeObject(info));
            }
            catch (Exception ex)
            {
                mainLog.Error("Failed to write info.dat: " + ex.ToString());
            }
        }
        private static void zipSong(SongModel song, string songTempFolder)
        {
            mainLog.Debug($"Zipping files for {song.internalName}");
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
                mainLog.Error($"{zipFile} is likely missing files!");
            else
                mainLog.Debug($"{zipFile} should be complete");
        }
        private static string getPlaylistCoverFromSongPack(SongModel song, string defaultValue = "BeatSaber")
        {
            if (songDefinitions.songPacks.ContainsKey(song.songPack))
            {
                return songDefinitions.songPacks[song.songPack].cover ?? defaultValue;
            }

            return defaultValue;
        }
        private static string getEnvironmentFromSongPack(SongModel song, string defaultValue = "DefaultEnvironment")
        {
            if (songDefinitions.songPacks.ContainsKey(song.songPack))
            {
                return songDefinitions.songPacks[song.songPack].environment ?? defaultValue;
            }

            return defaultValue;
        }
    }
}

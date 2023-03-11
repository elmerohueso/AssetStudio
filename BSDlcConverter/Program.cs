using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssetStudio;
using FluentArgs;
using static System.Net.WebRequestMethods;
using Object = AssetStudio.Object;

namespace BSDlcConverter
{
    static class Program
    {
        public static AssetsManager assetsManager = new AssetsManager();
        public static List<AssetItem> exportableAssets = new List<AssetItem>();
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            string folderOut = @"D:\Users\Brendan\Downloads\Beat Saber\export test";
            string fileIn = @"C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Beat Saber_Data\sharedassets0.assets";
            bool audio = false;
            bool json = false;
            bool sprite = true;
            doWork(fileIn, folderOut, audio, json, sprite);
            fileIn = @"C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\DLC\Levels\Bones\bones";
            audio = true;
            json = true;
            sprite = false;
            doWork(fileIn, folderOut, audio, json, sprite);
            Console.WriteLine("Done");
            Process.Start("explorer.exe", folderOut);
            Console.ReadLine();
        }
        static void doWork(string filePath, string folderOut, bool audio, bool json, bool sprite)
        {

            assetsManager = new AssetsManager();
            Console.WriteLine($"Reading \"{filePath}\"");
            assetsManager.LoadFiles(filePath);
            createExportList(audio, json, sprite);
            /*Console.WriteLine("Will export the following items:");
            foreach(AssetItem asset in exportableAssets)
            {
                Console.WriteLine($"{asset.Text} : {asset.Type}");
            }*/
            ExportAssets(folderOut, exportableAssets);
        }
        static void createExportList(bool audio, bool json, bool sprite)
        {
            exportableAssets = new List<AssetItem>();
            Console.WriteLine($"Creating export list");
            string productName = "";
            var objectCount = assetsManager.assetsFileList.Sum(x => x.Objects.Count);
            var objectAssetItemDic = new Dictionary<Object, AssetItem>(objectCount);
            var containers = new List<(PPtr<Object>, string)>();
            int i = 0;
            foreach (var assetsFile in assetsManager.assetsFileList)
            {
                foreach (Object asset in assetsFile.Objects)
                {
                    var assetItem = new AssetItem(asset);
                    objectAssetItemDic.Add(asset, assetItem);
                    assetItem.UniqueID = " #" + i;
                    var exportable = false;
                    switch (asset)
                    {
                        case AudioClip m_AudioClip:
                            if (!string.IsNullOrEmpty(m_AudioClip.m_Source))
                                assetItem.FullSize = asset.byteSize + m_AudioClip.m_Size;
                            assetItem.Text = m_AudioClip.m_Name;
                            if (audio)
                                exportable = true;
                            break;
                        case Sprite _:
                            assetItem.Text = ((NamedObject)asset).m_Name;
                            if (sprite && assetItem.Text.Contains("Cover"))
                                exportable = true;
                            break;
                        case MonoBehaviour m_MonoBehaviour:
                            if (m_MonoBehaviour.m_Name == "" && m_MonoBehaviour.m_Script.TryGet(out var m_Script))
                                assetItem.Text = m_Script.m_ClassName;
                            else
                                assetItem.Text = m_MonoBehaviour.m_Name;
                            if (json)
                                exportable = true;
                            break;
                    }
                    if (assetItem.Text == "")
                    {
                        assetItem.Text = assetItem.TypeString + assetItem.UniqueID;
                    }
                    if (exportable)
                        exportableAssets.Add(assetItem);
                }
            }
        }
        static void ExportAssets(string savePath, List<AssetItem> toExportAssets)
        {
            int toExportCount = toExportAssets.Count;
            int exportedCount = 0;
            int i = 0;
            foreach (var asset in toExportAssets)
            {
                string exportPath = savePath;
                exportPath = Path.Combine(exportPath, asset.TypeString);
                exportPath += Path.DirectorySeparatorChar;
                Console.WriteLine($"[{exportedCount}/{toExportCount}] Processing {asset.Text} : {asset.Type}");
                try
                {
                    if (Exporter.ExportConvertFile(asset, exportPath))
                    {
                        exportedCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Export {asset.Type}:{asset.Text} error\r\n{ex.Message}\r\n{ex.StackTrace}");
                }
            }

            var statusText = exportedCount == 0 ? "Nothing exported." : $"Finished exporting {exportedCount} assets.";

            if (toExportCount > exportedCount)
            {
                statusText += $" {toExportCount - exportedCount} assets skipped (not extractable or files already exist)";
            }
            Console.WriteLine(statusText);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssetStudio;
using DlcConverter;
using FluentArgs;
using static System.Net.WebRequestMethods;
using Object = AssetStudio.Object;

namespace BSDlcConverter
{
    internal class AssetHelper
    {
        public static AssetsManager assetsManager = new AssetsManager();
        public static List<AssetItem> exportableAssets = new List<AssetItem>();
        public static void exportAssets(string filePath, string folderOut, bool audio, bool json, bool sprite, IProgress<string> progressMessage, IProgress<int> progressAmount)
        {

            assetsManager = new AssetsManager();
            progressMessage?.Report($"Reading \"{Path.GetFileName(filePath)}\"");
            assetsManager.LoadFiles(filePath);
            createExportList(audio, json, sprite);
            progressMessage?.Report($"Found {exportableAssets.Count} assets to export");
            ExportAssets(folderOut, exportableAssets, progressMessage, progressAmount);
        }
        public static void createExportList(bool audio, bool json, bool sprite)
        {
            exportableAssets = new List<AssetItem>();
            Trace.WriteLine($"Creating export list");
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
        public static void ExportAssets(string savePath, List<AssetItem> toExportAssets, IProgress<string> progressMessage, IProgress<int> progressAmount)
        {
            int toExportCount = toExportAssets.Count;
            int exportedCount = 0;
            int i = 0;
            foreach (var asset in toExportAssets)
            {
                string exportPath = savePath;
                exportPath = Path.Combine(exportPath, asset.TypeString);
                exportPath += Path.DirectorySeparatorChar;

                progressAmount?.Report(i*100/toExportCount);
                progressMessage?.Report($"{asset.Text} ({asset.Type})");
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
                i++;
            }
            progressMessage?.Report($"{exportedCount} of {toExportCount} items exported");
        }
    }
}


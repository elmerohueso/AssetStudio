using AssetStudio;
using Fmod5Sharp.FmodTypes;
using Fmod5Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSDlcConverter
{
    internal static class Exporter
    {
        public static AssemblyLoader assemblyLoader = new AssemblyLoader();
        public static bool ExportConvertFile(AssetItem item, string exportPath)
        {
            switch (item.Type)
            {
                case ClassIDType.AudioClip:
                    return ExportAudioClip(item, exportPath, true);
                case ClassIDType.MonoBehaviour:
                    return ExportMonoBehaviour(item, exportPath);
                case ClassIDType.Sprite:
                    return ExportSprite(item, exportPath);
                default:
                    return ExportRawFile(item, exportPath);
            }
        }
        public static bool ExportAudioClip(AssetItem item, string exportPath, bool convert)
        {
            var m_AudioClip = (AudioClip)item.Asset;
            var m_AudioData = m_AudioClip.m_AudioData.GetData();
            if (m_AudioData == null || m_AudioData.Length == 0)
                return false;
            if (convert)
            {
                string tempPath = Path.Combine(exportPath, "_temp");
                if (!TryExportFile(exportPath, item, ".ogg", out var exportFullPath))
                    return false;
                // BS audio assets are OGGs, so we can extract them directly from their FSB sound banks
                FmodSoundBank bank = FsbLoader.LoadFsbFromByteArray(m_AudioData);
                List<FmodSample> samples = bank.Samples;
                // This method fixes the missing OGG header so the file is usable
                var success = samples[0].RebuildAsStandardFileFormat(out var dataBytes, out var fileExtension);
                if (success)
                {
                    //Console.WriteLine($"Converting audio to \"{exportFullPath}\"");
                    File.WriteAllBytes(exportFullPath, dataBytes);
                }
                // Eventually add logging for these errors
                else
                    Console.WriteLine("Sound bank did not contain OGG data");
            }
            else
            {
                if (!TryExportFile(exportPath, item, ".fsb", out var exportFullPath))
                    return false;
                File.WriteAllBytes(exportFullPath, m_AudioData);
            }
            return true;
        }
        public static bool ExportMonoBehaviour(AssetItem item, string exportPath)
        {
            if (!TryExportFile(exportPath, item, ".json", out var exportFullPath))
                return false;
            var m_MonoBehaviour = (MonoBehaviour)item.Asset;
            var type = m_MonoBehaviour.ToType();
            if (type == null)
            {
                var m_Type = m_MonoBehaviour.ConvertToTypeTree(assemblyLoader);
                type = m_MonoBehaviour.ToType(m_Type);
            }
            var str = JsonConvert.SerializeObject(type, Formatting.Indented);
            File.WriteAllText(exportFullPath, str);
            return true;
        }
        public static bool ExportSprite(AssetItem item, string exportPath)
        {
            var type = ImageFormat.Jpeg;
            if (!TryExportFile(exportPath, item, ".jpg", out var exportFullPath))
                return false;
            var image = ((Sprite)item.Asset).GetImage();
            if (image != null)
            {
                using (image)
                {
                    using (var file = File.OpenWrite(exportFullPath))
                    {
                        image.WriteToStream(file, type);
                    }
                    return true;
                }
            }
            return false;
        }
        public static bool ExportRawFile(AssetItem item, string exportPath)
        {
            if (!TryExportFile(exportPath, item, ".dat", out var exportFullPath))
                return false;
            File.WriteAllBytes(exportFullPath, item.Asset.GetRawData());
            return true;
        }
        private static bool TryExportFile(string dir, AssetItem item, string extension, out string fullPath)
        {
            var fileName = FixFileName(item.Text);
            fullPath = Path.Combine(dir, fileName + extension);
            if (!File.Exists(fullPath))
            {
                Directory.CreateDirectory(dir);
                return true;
            }
            fullPath = Path.Combine(dir, fileName + item.UniqueID + extension);
            if (!File.Exists(fullPath))
            {
                Directory.CreateDirectory(dir);
                return true;
            }
            return false;
        }

        public static string FixFileName(string str)
        {
            return Path.GetInvalidFileNameChars().Aggregate(str, (current, c) => current.Replace(c, '_'));
        }
    }
}

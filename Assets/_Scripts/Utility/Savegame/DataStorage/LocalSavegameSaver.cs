using System;
using System.IO;
using System.Text;
using UnityEngine;
using Utility.Savegame;
using Utility.Singletons;
using Logger = DebugLogger.Logger;

namespace Utility.Savegame.DataStorage
{
    public class LocalSavegameSaver<T, TData> : SingletonModel<LocalSavegameSaver<T, TData>> where T : Savegame<TData>
    {
        public void Save(T savegame)
        {
            var folderPath = SavegamePaths.GetFolderPath();
            var filePath = SavegamePaths.GetFilePath();

            CreateFolder(folderPath);

            WriteJsonFile(JsonUtility.ToJson(savegame), filePath);
        }

        private void WriteJsonFile(string json, string filePath, bool retry = true)
        {
            if (json == null) throw new ArgumentNullException(nameof(json));
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));

            try
            {
                File.WriteAllText(filePath, json);
                ValidateJsonFile(json, filePath, retry);
            }
            catch (Exception e)
            {
                Logger.Critical("Savegame write error: " + e);
                if (retry)
                    WriteJsonFile(json, filePath, false);
            }
        }

        private static void ValidateJsonFile(string json, string path, bool retry)
        {
            var fileInfo = new FileInfo(path);
            var jsonSize = Encoding.UTF8.GetBytes(json);
            if (fileInfo.Length != jsonSize.Length)
            {
                var log = $"Savegame file size does not match expected size! Expected: {jsonSize.Length} Actual: {fileInfo.Length}\nRetry: {retry}";
                throw new ArgumentException(log, json);
            }
        }

        private static void CreateFolder(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
        }
    }
}
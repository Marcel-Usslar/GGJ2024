using System.IO;
using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public class FolderUtility
    {
        public static string CreateFolder(string parentFolder, string folderName)
        {
            var folderPath = Path.Combine(parentFolder, folderName);
            return AssetDatabase.IsValidFolder(folderPath)
                ? folderPath
                : AssetDatabase.CreateFolder(parentFolder, folderName);
        }

        public static void CopyFileToFolder(string filePath, string fileDestinationPath)
        {
            FileUtil.ReplaceFile(filePath, fileDestinationPath);
            var relativePath = GetRelativePath(fileDestinationPath);
            AssetDatabase.ImportAsset(relativePath, ImportAssetOptions.ForceUpdate);
        }
        public static void CopyFileToRelativeFolder(string filePath, string relativeDestinationPath)
        {
            FileUtil.ReplaceFile(filePath, GetAbsolutePath(relativeDestinationPath));
            AssetDatabase.ImportAsset(relativeDestinationPath, ImportAssetOptions.ForceUpdate);
        }

        public static string SelectFolder(string folder = null)
        {
            if (!string.IsNullOrEmpty(folder))
                return folder;

            do folder = UnityEditor.EditorUtility.OpenFolderPanel("Choose folder", folder, string.Empty);
            while (string.IsNullOrEmpty(folder) && DisplayInvalidFolderDialog());

            return folder;
        }

        private static bool DisplayInvalidFolderDialog()
        {
            return UnityEditor.EditorUtility.DisplayDialog("Invalid Folder Selection",
                "The folder you selected was invalid, please choose a valid folder!",
                "Select Folder", "Close");
        }

        public static string GetRelativePath(string path)
        {
            return "Assets" + path.Substring(Application.dataPath.Length);
        }
        public static string GetAbsolutePath(string relativePath)
        {
            return Application.dataPath + relativePath.Substring("Assets".Length);
        }
    }
}
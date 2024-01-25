namespace Utility.Savegame.DataStorage
{
    public class SavegamePaths
    {
        private const string K_editorSavegamePath = "savegame/";
        private const string K_filename = "savegame.json";

        public static string GetFolderPath()
        {
#if UNITY_EDITOR
            var path = K_editorSavegamePath;
#else
            var path = UnityEngine.Application.persistentDataPath;
#endif
            if (!path.EndsWith("/"))
                path += "/";

            return path;
        }

        public static string GetFilePath()
        {
            return GetFolderPath() + K_filename;
        }
    }
}
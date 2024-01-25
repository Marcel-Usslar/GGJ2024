using System;
using System.IO;
using Utility.Savegame.Migration;
using UnityEngine;
using Utility.Savegame;
using Utility.Singletons;
using Logger = DebugLogger.Logger;

namespace Utility.Savegame.DataStorage
{
    public class LocalSavegameLoader<T, TData> : SingletonModel<LocalSavegameLoader<T, TData>> where T : Savegame<TData>
    {
        private ISavegameFactory<T, TData> _savegameFactory;

        public void Setup(ISavegameFactory<T, TData> savegameFactory)
        {
            _savegameFactory = savegameFactory;
            Migrator.Instance.RegisterMigrations(savegameFactory.Migrations);
        }

        public T LoadSavegame()
        {
            try
            {
                return JsonUtility.FromJson<T>(LoadJson()) ?? _savegameFactory.Create();
            }
            catch
            {
                return _savegameFactory.Create();
            }
        }

        private string LoadJson()
        {
            var filePath = SavegamePaths.GetFilePath();
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return null;

            try
            {
                var fileText = File.ReadAllText(filePath);
                return Migrator.Instance.Migrate(fileText);
            }
            catch (Exception e)
            {
                Logger.Critical("Savegame read error: " + e);
                return null;
            }
        }
    }
}
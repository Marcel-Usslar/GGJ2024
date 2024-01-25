using System;

namespace Utility.Savegame.Migration
{
    public static class JsonMigrationExtensions
    {
        public static JsonMigrationObject WithField<T>(this JsonMigrationObject jsonMigrationObject, string propertyName, T value)
        {
            jsonMigrationObject.Add(propertyName, value);
            return jsonMigrationObject;
        }

        public static JsonMigrationObject AddNewSavegame(this JsonMigrationObject jsonMigrationObject, string propertyName)
        {
            var newSavegame = new JsonMigrationObject();
            jsonMigrationObject.Add(propertyName, newSavegame);
            return newSavegame;
        }

        public static JsonMigrationObject Get(this JsonMigrationObject jsonMigrationObject, string propertyName)
        {
            return jsonMigrationObject.Get<JsonMigrationObject>(propertyName);
        }
        
        public static void Rename(this JsonMigrationObject jsonMigrationObject, string existing, string renamed)
        {
            if (!jsonMigrationObject.Exists(existing))
                throw new InvalidOperationException($"Migration property {existing} was not found while trying to rename it.");

            var renamedObject = jsonMigrationObject.Get(existing);
            jsonMigrationObject.Add(renamed, renamedObject);
            jsonMigrationObject.Remove(existing);
        }
    }
}
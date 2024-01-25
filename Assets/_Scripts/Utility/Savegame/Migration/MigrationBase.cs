using DebugLogger;

namespace Utility.Savegame.Migration
{
    public abstract class MigrationBase : IMigration
    {
        private const string K_version = "Version";

        public abstract int MigrationVersion { get; }

        public JsonMigrationObject Migrate(JsonMigrationObject source)
        {
            if (source.Get<int>(K_version) >= MigrationVersion)
                return source;

            var migrated = MigrateVersion(source);
            source.SetOrAdd(K_version, MigrationVersion);

            Logger.Warning($"Executed migration version {MigrationVersion}.\nMigrated savegame:\n{migrated}.");

            return migrated;
        }

        protected abstract JsonMigrationObject MigrateVersion(JsonMigrationObject source);
    }
}
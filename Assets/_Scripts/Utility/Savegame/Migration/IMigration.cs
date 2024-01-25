namespace Utility.Savegame.Migration
{
    public interface IMigration
    {
        int MigrationVersion { get; }

        JsonMigrationObject Migrate(JsonMigrationObject source);
    }
}
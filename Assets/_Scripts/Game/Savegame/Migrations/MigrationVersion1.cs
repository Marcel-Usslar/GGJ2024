using Utility.Savegame.Migration;

namespace Game.Savegame.Migrations
{
    public class MigrationVersion1 : MigrationBase
    {
        public override int MigrationVersion => 1;

        protected override JsonMigrationObject MigrateVersion(JsonMigrationObject source)
        {
            return source;
        }
    }
}
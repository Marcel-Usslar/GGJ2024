using System.Collections.Generic;
using System.Linq;
using Utility.Singletons;

namespace Utility.Savegame.Migration
{
    public class Migrator : SingletonModel<Migrator>
    {
        private IDictionary<int, IMigration> _migrations;

        public void RegisterMigrations(IEnumerable<IMigration> migrations)
        {
            _migrations = migrations.ToDictionary(migration => migration.MigrationVersion);
        }

        public string Migrate(string json)
        {
            var migrationObject = new JsonMigrationObject(json);
            migrationObject = _migrations.OrderBy(pair => pair.Key).Select(pair => pair.Value)
                .Aggregate(migrationObject, (current, migration) => migration.Migrate(current));

            return migrationObject.ToString();
        }
    }
}
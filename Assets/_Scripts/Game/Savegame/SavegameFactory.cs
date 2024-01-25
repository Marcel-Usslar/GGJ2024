using System.Collections.Generic;
using System.Linq;
using Utility.Savegame.Migration;
using Game.Savegame.Migrations;
using Utility.Savegame;
using Utility.Singletons;

namespace Game.Savegame
{
    public class SavegameFactory : SingletonModel<SavegameFactory>, ISavegameFactory<Savegame, SavegameData>
    {
        private readonly List<IMigration> _migrations = new()
        {
            new MigrationVersion1(),
        };

        public IEnumerable<IMigration> Migrations => _migrations;

        public Savegame Create()
        {
            return new Savegame
            {
                Version = _migrations.Max(migration => migration.MigrationVersion),
                SavegameData = new SavegameData
                {

                }
            };
        }
    }
}
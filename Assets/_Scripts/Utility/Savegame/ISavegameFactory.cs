using System.Collections.Generic;
using Utility.Savegame.Migration;

namespace Utility.Savegame
{
    public interface ISavegameFactory<T, TData> where T : Savegame<TData>
    {
        IEnumerable<IMigration> Migrations { get; }

        T Create();
    }
}
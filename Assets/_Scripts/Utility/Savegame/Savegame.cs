using System;

namespace Utility.Savegame
{
    [Serializable]
    public class Savegame<T>
    {
        public int Version;
        public T SavegameData;
    }
}
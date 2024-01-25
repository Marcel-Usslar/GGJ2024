using Utility.Savegame.DataStorage;
using Utility.Singletons;

namespace Game.Savegame
{
    public class SavegameService : SingletonMonoBehaviour<SavegameService>
    {
        public Savegame Savegame { get; private set; }

        protected override void OnInitialize()
        {
            LocalSavegameLoader<Savegame, SavegameData>.Instance.Setup(SavegameFactory.Instance);
            Savegame = LocalSavegameLoader<Savegame, SavegameData>.Instance.LoadSavegame();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            LocalSavegameSaver<Savegame, SavegameData>.Instance.Save(Savegame);
        }
    }
}
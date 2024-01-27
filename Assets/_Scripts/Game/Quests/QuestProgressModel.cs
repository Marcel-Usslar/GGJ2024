using Game.LevelManagement;
using Game.Utility;
using Utility;
using Utility.Singletons;

namespace Game.Quests
{
    public class QuestProgressModel : SingletonModel<QuestProgressModel>
    {
        public int TotalQuests { get; private set; }
        public ReactiveProperty<int> CompletedQuests { get; } = new();

        public QuestProgressModel()
        {
            UpdateTotalQuests();
            LevelLoadingModel.Instance.OnLevelLoaded.RegisterCallback(UpdateTotalQuests);
        }

        private void UpdateTotalQuests()
        {
            TotalQuests = ConfigSingletonInstaller.Instance.QuestConfig.TotalQuests;
        }
    }
}
using System.Collections.Generic;
using Game.Utility;
using Utility;
using Utility.Singletons;

namespace Game.Quests
{
    public class QuestSystem : SingletonModel<QuestSystem>
    {
        private readonly List<string> _receivedQuests = new();
        private readonly List<string> _completedQuests = new();

        public IEnumerable<string> ReceivedQuests => _receivedQuests;
        public IEnumerable<string> CompletedQuests => _completedQuests;

        public CallbackHandler<string> OnQuestAccepted { get; } = new();
        public CallbackHandler<string> OnQuestCompleted { get; } = new();

        public bool HasQuest(string speaker)
        {
            var questConfig = ConfigSingletonInstaller.Instance.QuestConfig;

            if (!questConfig.HasQuest(speaker))
                return false;

            var questName = questConfig.GetQuestName(speaker);
            return !_receivedQuests.Contains(questName) && _completedQuests.Contains(questName);
        }

        public void AcceptQuest(string speaker)
        {
            var questConfig = ConfigSingletonInstaller.Instance.QuestConfig;
            var questName = questConfig.GetQuestName(speaker);

            _receivedQuests.Add(questName);
            OnQuestAccepted.Trigger(questName);
        }

        public void CompleteQuest(string quest)
        {
            if (!_receivedQuests.Contains(quest))
            {
                //Log quest not received
                return;
            }

            _receivedQuests.Remove(quest);
            _completedQuests.Add(quest);

            QuestProgressModel.Instance.CompletedQuests.Value += 1;
            OnQuestCompleted.Trigger(quest);
        }
    }
}
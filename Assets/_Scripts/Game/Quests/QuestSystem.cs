using System.Collections.Generic;
using Game.Speaker;
using Game.Utility;
using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.Quests
{
    public class QuestSystem : SingletonModel<QuestSystem>
    {
        private readonly List<QuestType> _receivedQuests = new();
        private readonly List<QuestType> _completedQuests = new();

        public IEnumerable<QuestType> ReceivedQuests => _receivedQuests;
        public IEnumerable<QuestType> CompletedQuests => _completedQuests;

        public CallbackHandler<QuestType> OnQuestAccepted { get; } = new();
        public CallbackHandler<QuestType> OnQuestCompleted { get; } = new();

        public bool HasQuest(SpeakerType speaker)
        {
            var questConfig = ConfigSingletonInstaller.Instance.QuestConfig;

            if (!questConfig.HasQuest(speaker))
                return false;

            var quest = questConfig.GetQuestType(speaker);
            return !_receivedQuests.Contains(quest) && !_completedQuests.Contains(quest);
        }

        public void AcceptQuest(QuestType quest)
        {
            if (_receivedQuests.Contains(quest))
            {
                Debug.LogError($"Quest {quest} was already started!");
                return;
            }
            if (_completedQuests.Contains(quest))
            {
                Debug.LogError($"Quest {quest} was already completed!");
                return;
            }

            _receivedQuests.Add(quest);
            OnQuestAccepted.Trigger(quest);
        }

        public void CompleteQuest(QuestType quest)
        {
            if (!_receivedQuests.Contains(quest))
            {
                Debug.LogError($"Quest {quest} was never started!");
                return;
            }

            _receivedQuests.Remove(quest);
            _completedQuests.Add(quest);

            QuestProgressModel.Instance.CompletedQuests.Value += 1;
            OnQuestCompleted.Trigger(quest);
        }
    }
}
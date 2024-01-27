using Game.Quests;
using UnityEngine;

namespace Game.Dialog
{
    public class QuestDialogStateView : DialogStateView
    {
        [SerializeField] private string _questName;

        private int _state;

        public override int State => _state;

        private void Start()
        {
            QuestSystem.Instance.OnQuestAccepted.RegisterCallback(TryForwardingState);
            QuestSystem.Instance.OnQuestCompleted.RegisterCallback(TryForwardingState);
        }

        private void OnDestroy()
        {
            QuestSystem.Instance.OnQuestAccepted.UnregisterCallback(TryForwardingState);
            QuestSystem.Instance.OnQuestCompleted.UnregisterCallback(TryForwardingState);
        }

        private void TryForwardingState(string questName)
        {
            if (questName != _questName)
                return;

            _state++;
        }
    }
}
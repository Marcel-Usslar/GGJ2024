using System.Linq;
using Game.Inventory;
using Game.Quests;
using UnityEngine;

namespace Game.Dialog
{
    public class ItemUsageDialogStateView : DialogStateView
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private QuestType _questType;

        private int _state;

        public override int State => _state;

        private void Start()
        {
            QuestSystem.Instance.OnQuestAccepted.RegisterCallback(TryForwardingState);
            QuestSystem.Instance.OnQuestCompleted.RegisterCallback(TryForwardingState);

            InventoryModel.Instance.OnItemReceived.RegisterCallback(TryForwardingState);
            InventoryModel.Instance.OnItemUsed.RegisterCallback(TryForwardingState);
            InventoryModel.Instance.OnItemRemoved.RegisterCallback(TryRevertingState);
        }

        private void OnDestroy()
        {
            QuestSystem.Instance.OnQuestAccepted.UnregisterCallback(TryForwardingState);
            QuestSystem.Instance.OnQuestCompleted.UnregisterCallback(TryForwardingState);

            InventoryModel.Instance.OnItemReceived.UnregisterCallback(TryForwardingState);
            InventoryModel.Instance.OnItemUsed.UnregisterCallback(TryForwardingState);
            InventoryModel.Instance.OnItemRemoved.UnregisterCallback(TryRevertingState);
        }

        private void TryForwardingState(QuestType questName)
        {
            if (questName != _questType)
                return;

            _state++;
        }

        private void TryForwardingState(InventoryItem item)
        {
            if (item != _item || !QuestSystem.Instance.ReceivedQuests.Contains(_questType))
                return;

            _state++;
        }

        private void TryRevertingState(InventoryItem item)
        {
            if (item != _item)
                return;

            _state--;
        }
    }
}
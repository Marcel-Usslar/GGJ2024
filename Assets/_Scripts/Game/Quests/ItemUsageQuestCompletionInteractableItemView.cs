using System.Linq;
using Game.Interaction;
using Game.Inventory;
using UnityEngine;

namespace Game.Quests
{
    public class ItemUsageQuestCompletionInteractableItemView : DialogInteractableItemView
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private QuestType _quest;

        public override void Interact()
        {
            TriggerDialog();

            if (!InventoryModel.Instance.HasInventoryItem(_item) || !QuestSystem.Instance.ReceivedQuests.Contains(_quest))
                return;

            InventoryModel.Instance.UseInteractableItem(_item);
            QuestSystem.Instance.CompleteQuest(_quest);
        }
    }
}
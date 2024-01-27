using Game.Dialog;
using Game.Quests;
using Game.Utility;
using UnityEngine;

namespace Game.Interaction
{
    public class DialogInteractableItemView : InteractableItemView
    {
        [SerializeField] private string _speakerName;
        [SerializeField] private DialogStateView _dialogStateView;

        public override void Interact()
        {
            if (QuestSystem.Instance.HasQuest(_speakerName))
                AcceptQuest();
            else
                TriggerDialog();
        }

        private void AcceptQuest()
        {
            QuestSystem.Instance.AcceptQuest(_speakerName);
            TriggerDialog();
        }

        private void TriggerDialog()
        {
            DialogSystem.Instance.TriggerDialog(_speakerName, _dialogStateView.State);
        }
    }
}
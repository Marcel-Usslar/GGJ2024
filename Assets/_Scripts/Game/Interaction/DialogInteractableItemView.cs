using Game.Dialog;
using Game.Quests;
using Game.Speaker;
using Game.Utility;
using UnityEngine;

namespace Game.Interaction
{
    public class DialogInteractableItemView : InteractableItemView
    {
        [SerializeField] private SpeakerType _speaker;
        [SerializeField] private DialogStateView _dialogStateView;

        public override void Interact()
        {
            if (QuestSystem.Instance.HasQuest(_speaker))
                AcceptQuest();
            else
                TriggerDialog();
        }

        private void AcceptQuest()
        {
            TriggerDialog();
            QuestSystem.Instance.AcceptQuest(_speaker);
        }

        private void TriggerDialog()
        {
            DialogSystem.Instance.TriggerDialog(_speaker, _dialogStateView.State);
        }
    }
}
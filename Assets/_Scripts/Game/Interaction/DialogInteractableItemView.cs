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
            var questConfig = ConfigSingletonInstaller.Instance.QuestConfig;
            QuestSystem.Instance.AcceptQuest(questConfig.GetQuestType(_speaker));
        }

        protected void TriggerDialog()
        {
            var id = new DialogId(_speaker, _dialogStateView.State);
            DialogSystem.Instance.TriggerDialog(id);
        }
    }
}
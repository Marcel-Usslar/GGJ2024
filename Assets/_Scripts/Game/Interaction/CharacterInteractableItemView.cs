using Game.Dialog;
using Game.Quests;
using Game.Utility;
using UnityEngine;

namespace Game.Interaction
{
    public class CharacterInteractableItemView : InteractableItemView
    {
        [SerializeField] private string _id;
        [SerializeField] private string _speakerName;

        public override string Id => _id;

        public override void Interact()
        {
            if (QuestSystem.Instance.HasQuest(_speakerName))
                AcceptQuest();
            else
                DialogSystem.Instance.TriggerDialog(ConfigSingletonInstaller.Instance.CharacterDialogConfig.GetDialogId(_speakerName, 0));
        }

        private void AcceptQuest()
        {
            QuestSystem.Instance.AcceptQuest(_speakerName);
            //TODO trigger dialog for quest
        }
    }
}
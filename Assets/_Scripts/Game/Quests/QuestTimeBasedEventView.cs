using Game.Dialog;
using Game.LevelTimer;
using UnityEngine;

namespace Game.Quests
{
    public class QuestTimeBasedEventView : BaseTimeBasedEventView
    {
        [SerializeField] private QuestType _questType;
        [SerializeField] private DialogId _dialogId;

        protected override void Trigger()
        {
            DialogSystem.Instance.TriggerDialog(_dialogId);
            QuestSystem.Instance.AcceptQuest(_questType);
        }
    }
}
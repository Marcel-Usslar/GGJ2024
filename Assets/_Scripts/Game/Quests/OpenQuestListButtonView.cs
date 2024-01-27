using CustomButton;
using UnityEngine;

namespace Game.Quests
{
    public class OpenQuestListButtonView : MonoBehaviour
    {
        [SerializeField] private ReactiveButton _button;

        private void Start()
        {
            _button.RegisterClickHandler(_ => QuestListModel.Instance.IsVisible.Value = true);
        }

        private void OnDestroy()
        {
            _button.UnregisterClickHandler(_ => QuestListModel.Instance.IsVisible.Value = true);
        }
    }
}
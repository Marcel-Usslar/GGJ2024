using System;
using CustomButton;
using Game.UI;
using UnityEngine;
using Utility;

namespace Game.MainMenu
{
    public class LevelSelectionMenuScreen : BaseScreen
    {
        private const float Width = 380f;
        private const float Spacing = 30f;
        private const float Offset = 50f;

        [SerializeField] private RectTransform _scrollRect;
        [SerializeField] private RectTransform _levelContainer;
        [SerializeField] private ReactiveButton _backButton;

        protected override ReactiveProperty<bool> Visibility => MenuModel.Instance.ShowLevelSelection;

        protected override void OnStart()
        {
            _backButton.RegisterClickHandler(_ => ReturnToMainMenu());
        }

        protected override void OnFinalize()
        {
            _backButton.UnregisterClickHandler(_ => ReturnToMainMenu());
        }

        protected override void OnVisibilityChanged(bool visible)
        {
            if (visible)
            {
                ShowLevelEntries();
            }
            else
            {
                //TODO hide level entries
            }
        }

        private void ReturnToMainMenu()
        {
            Hide();
            MenuModel.Instance.ShowMainMenu.Value = true;
        }

        private void ShowLevelEntries()
        {
            //TODO spawn entries and scroll to position
        }

        private void SetScrollPosition(int targetEntry, int totalEntries)
        {
            var targetPosition = (targetEntry - 0.5f) * Width + (targetEntry - 1) * Spacing + Offset;
            var rectSize = _scrollRect.rect.width;
            targetPosition -= rectSize / 2f;

            var maxPosition = totalEntries * Width + (totalEntries - 1) * Spacing + 2 * Offset - rectSize;
            targetPosition = Math.Clamp(targetPosition, 0f, maxPosition);

            _levelContainer.anchoredPosition = new Vector2(-targetPosition, 0);
        }
    }
}
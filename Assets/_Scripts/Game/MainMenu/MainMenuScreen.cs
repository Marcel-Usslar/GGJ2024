using CustomButton;
using Game.UI;
using UnityEngine;
using Utility;

namespace Game.MainMenu
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private ReactiveButton _startButton;
        [SerializeField] private ReactiveButton _settingsButton;
        [SerializeField] private ReactiveButton _quitButton;

        protected override ReactiveProperty<bool> Visibility => MenuModel.Instance.ShowMainMenu;

        protected override void OnStart()
        {
            _startButton.RegisterClickHandler(_ => ShowLevelSelection());
            _settingsButton.RegisterClickHandler(_ => ShowSettings());
            _quitButton.RegisterClickHandler(_ => QuitGame());
        }

        protected override void OnFinalize()
        {
            _startButton.UnregisterClickHandler(_ => ShowLevelSelection());
            _settingsButton.UnregisterClickHandler(_ => ShowSettings());
            _quitButton.UnregisterClickHandler(_ => QuitGame());
        }

        private void ShowLevelSelection()
        {
            Hide();
            MenuModel.Instance.ShowLevelSelection.Value = true;
        }

        private void ShowSettings()
        {
            Hide();
            MenuModel.Instance.ShowSettings.Value = true;
        }

        private static void QuitGame()
        {
            if (!Application.isEditor)
                Application.Quit();
#if UNITY_EDITOR
            else
                UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
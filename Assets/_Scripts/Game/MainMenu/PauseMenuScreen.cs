using CustomButton;
using Game.GameState;
using Game.LevelManagement;
using Game.UI;
using UnityEngine;
using Utility;
using Utility.Time;

namespace Game.MainMenu
{
    public class PauseMenuScreen : BaseScreen
    {
        [SerializeField] private ReactiveButton _resumeButton;
        [SerializeField] private ReactiveButton _retryButton;
        [SerializeField] private ReactiveButton _settingsButton;
        [SerializeField] private ReactiveButton _mainMenuButton;
        [SerializeField] private float _unpauseDelay;

        private readonly CallbackHandler _unpauseDelayed = new();
        private TimerCancellationToken _cancellationToken;

        protected override ReactiveProperty<bool> Visibility => MenuModel.Instance.ShowPauseMenu;

        protected override void OnStart()
        {
            _resumeButton.RegisterClickHandler(_ => OnClickResume());
            _retryButton.RegisterClickHandler(_ => OnClickRetry());
            _settingsButton.RegisterClickHandler(_ => OnClickSettings());
            _mainMenuButton.RegisterClickHandler(_ => OnClickMenu());

            _unpauseDelayed.RegisterCallback(UnpauseGame);
        }

        protected override void OnFinalize()
        {
            _resumeButton.UnregisterClickHandler(_ => OnClickResume());
            _retryButton.UnregisterClickHandler(_ => OnClickRetry());
            _settingsButton.UnregisterClickHandler(_ => OnClickSettings());
            _mainMenuButton.UnregisterClickHandler(_ => OnClickMenu());

            _unpauseDelayed.ClearCallbacks();

            if (_cancellationToken != null)
                _cancellationToken.Cancel = true;
        }

        private void OnClickResume()
        {
            Hide();
            _unpauseDelayed.Trigger();
        }

        private void OnClickRetry()
        {
            Hide();
            //TODO restart level
        }

        private void OnClickSettings()
        {
            Hide();
            MenuModel.Instance.ShowSettings.Value = true;
        }

        private void OnClickMenu()
        {
            Hide();
            LevelLoadingModel.Instance.LoadMenu();
        }

        private void UnpauseGame()
        {
            if (_cancellationToken != null)
                _cancellationToken.Cancel = true;

            _cancellationToken = new TimerCancellationToken();
            Timer.Instance.InSeconds(_unpauseDelay, () => GameStateModel.Instance.IsPaused.Value = false,
                _cancellationToken);
        }

        protected override void OnVisibilityChanged(bool visible)
        {
            if (!visible)
                return;

            GameStateModel.Instance.IsPaused.Value = true;
        }
    }
}
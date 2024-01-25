using Game.GameState;
using Game.MainMenu;
using Game.UI;
using Utility;
using Utility.Singletons;

namespace Game.LevelManagement
{
    public class LevelLoadingModel : SingletonModel<LevelLoadingModel>
    {
        public CallbackHandler OnLevelLoaded { get; } = new();

        public void LoadLevel()
        {
            LoadingFeedbackModel.Instance.ShowLoadingFeedback.Value = true;
            LoadingFeedbackModel.Instance.OnFadeInCompleted
                .RegisterCallback(() => LevelManagementService.Instance.LoadGame(OnGameLoaded));
        }

        public void LoadMenu()
        {
            LoadingFeedbackModel.Instance.ShowLoadingFeedback.Value = true;
            LoadingFeedbackModel.Instance.OnFadeInCompleted
                .RegisterCallback(() => LevelManagementService.Instance.UnloadGame(OnGameUnloaded));
        }

        private void OnGameLoaded()
        {
            GameStateModel.Instance.IsPlaying = true;

            OnLevelLoaded.Trigger();

            LoadingFeedbackModel.Instance.ShowLoadingFeedback.Value = false;

            LoadingFeedbackModel.Instance.OnFadeInCompleted
                .UnregisterCallback(() => LevelManagementService.Instance.LoadGame(OnGameLoaded));
        }

        private void OnGameUnloaded()
        {
            MenuModel.Instance.ShowLevelSelection.Value = true;
            GameStateModel.Instance.IsPlaying = false;

            LoadingFeedbackModel.Instance.ShowLoadingFeedback.Value = false;

            LoadingFeedbackModel.Instance.OnFadeInCompleted
                .UnregisterCallback(() => LevelManagementService.Instance.UnloadGame(OnGameUnloaded));
        }
    }
}
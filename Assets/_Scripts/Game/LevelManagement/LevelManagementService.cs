using System;
using System.Collections;
using Game.Utility;
using UnityEngine.SceneManagement;
using Utility.Singletons;

namespace Game.LevelManagement
{
    public class LevelManagementService : SingletonMonoBehaviour<LevelManagementService>
    {
        public void LoadGame(Action onLoaded = null)
        {
            StartCoroutine(LoadGameScene(onLoaded));
        }

        public void UnloadGame(Action onUnloaded = null)
        {
            StartCoroutine(UnloadGameScene(onUnloaded));
        }

        private IEnumerator LoadGameScene(Action onLoaded)
        {
            var config = ConfigSingletonInstaller.Instance.SceneConfig;

            yield return UnloadScene(config.GameScene);
            yield return LoadScene(config.GameScene);

            SetActiveScene(config.GameScene);

            onLoaded?.Invoke();
        }

        private IEnumerator UnloadGameScene(Action onUnloaded)
        {
            var config = ConfigSingletonInstaller.Instance.SceneConfig;

            yield return UnloadScene(config.GameScene);

            onUnloaded?.Invoke();
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            var operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            while (!operation.isDone)
                yield return null;

            yield return null;
        }

        private IEnumerator UnloadScene(int sceneIndex)
        {
            if (!IsSceneLoaded(sceneIndex))
                yield break;

            var operation = SceneManager.UnloadSceneAsync(sceneIndex);

            while (!operation.isDone)
                yield return null;
        }

        private void SetActiveScene(int sceneIndex)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        }

        private bool IsSceneLoaded(int sceneIndex)
        {
            return SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded;
        }
    }
}
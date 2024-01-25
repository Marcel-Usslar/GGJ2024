using Game.LevelManagement;
using UnityEngine;
using Utility.Singletons;

namespace Game.Utility
{
    public class ConfigSingletonInstaller : SingletonMonoBehaviour<ConfigSingletonInstaller>
    {
        [SerializeField] private SceneConfig _sceneConfig;

        public ISceneConfig SceneConfig => _sceneConfig;
    }
}
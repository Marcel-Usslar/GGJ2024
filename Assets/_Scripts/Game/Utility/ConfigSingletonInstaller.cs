using Game.LevelManagement;
using UnityEngine;
using Utility.Singletons;

namespace Game.Utility
{
    public class ConfigSingletonInstaller : SingletonMonoBehaviour<ConfigSingletonInstaller>
    {
        [SerializeField] private SceneConfig _sceneConfig;
        [SerializeField] private LevelTimerConfig _levelTimerConfig;

        public ISceneConfig SceneConfig => _sceneConfig;
        public LevelTimerConfig LevelTimerConfig => _levelTimerConfig;
    }
}
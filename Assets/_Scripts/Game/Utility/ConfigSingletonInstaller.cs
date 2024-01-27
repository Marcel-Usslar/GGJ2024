using Game.LevelManagement;
using Game.Quests;
using Game.Speaker;
using UnityEngine;
using Utility.Singletons;

namespace Game.Utility
{
    public class ConfigSingletonInstaller : SingletonMonoBehaviour<ConfigSingletonInstaller>
    {
        [SerializeField] private SceneConfig _sceneConfig;
        [SerializeField] private SpeakerConfig _speakerConfig;
        [SerializeField] private QuestConfig _questConfig;

        public ISceneConfig SceneConfig => _sceneConfig;
        public SpeakerConfig SpeakerConfig => _speakerConfig;
        public QuestConfig QuestConfig => _questConfig;
    }
}
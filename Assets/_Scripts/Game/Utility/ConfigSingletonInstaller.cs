using Game.Dialog.Config;
using Game.LevelManagement;
using Game.Quests;
using Game.Speaker;
using UnityEngine;
using Utility.Singletons;

namespace Game.Utility
{
    public class ConfigSingletonInstaller : SingletonMonoBehaviour<ConfigSingletonInstaller>
    {
        [SerializeField] private CharacterDialogConfig _characterDialogConfig;
        [SerializeField] private DialogConfig _dialogConfig;
        [SerializeField] private LevelTimerConfig _levelTimerConfig;
        [SerializeField] private QuestConfig _questConfig;
        [SerializeField] private SceneConfig _sceneConfig;
        [SerializeField] private SpeakerConfig _speakerConfig;

        public CharacterDialogConfig CharacterDialogConfig => _characterDialogConfig;
        public DialogConfig DialogConfig => _dialogConfig;
        public LevelTimerConfig LevelTimerConfig => _levelTimerConfig;
        public QuestConfig QuestConfig => _questConfig;
        public ISceneConfig SceneConfig => _sceneConfig;
        public SpeakerConfig SpeakerConfig => _speakerConfig;
    }
}
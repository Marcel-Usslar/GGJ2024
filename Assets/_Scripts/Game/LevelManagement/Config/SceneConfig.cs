using UnityEngine;

namespace Game.LevelManagement.Config
{
    public class SceneConfig : ScriptableObject, ISceneConfig
    {
        [SerializeField] private int _gameScene;

        public int GameScene => _gameScene;
    }
}
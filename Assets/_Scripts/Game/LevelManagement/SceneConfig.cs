using UnityEngine;

namespace Game.LevelManagement
{
    public class SceneConfig : ScriptableObject, ISceneConfig
    {
        [SerializeField] private int _gameScene;

        public int GameScene => _gameScene;
    }
}
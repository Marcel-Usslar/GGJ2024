using UnityEngine;

namespace Game.LevelTimer.Config
{
    public class LevelTimerConfig : ScriptableObject
    {
        [SerializeField] private float _totalLevelRealTimeSeconds;
        [SerializeField] private float _gameTimeStartHours;
        [SerializeField] private float _gameTimeEndHours;
        [SerializeField] private float _gameTimeHourSteps;

        public float TotalLevelRealTimeSeconds => _totalLevelRealTimeSeconds;
        public float GameTimeStartHours => _gameTimeStartHours;
        public float GameTimeEndHours => _gameTimeEndHours;
        public float GameTimeHourSteps => _gameTimeHourSteps;
    }
}
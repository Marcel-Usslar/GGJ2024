using System;
using Game.GameState;
using Game.Utility;
using TMPro;
using UnityEngine;
using Utility;

namespace Game.LevelTimer
{
    public class LevelTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        public DateTime CurrentTime { set => _timerText.text = $"{value.Hour:00}:{value.Minute:00}"; }

        private float _timeCountSinceLevelStart;

        private DateTime _startTime;
        private float _roundedPassedTime;

        private void Awake()
        {
            var config = ConfigSingletonInstaller.Instance.LevelTimerConfig;
            _startTime = DateTime.MinValue.AddHours(config.GameTimeStartHours);
            CurrentTime = _startTime;
        }

        private void Update()
        {
            if (GameStateModel.Instance.IsPaused.Value)
                return;

            _timeCountSinceLevelStart += Time.deltaTime;

            var totalRealTime = ConfigSingletonInstaller.Instance.LevelTimerConfig.TotalLevelRealTimeSeconds;
            var overTotalTime = _timeCountSinceLevelStart > totalRealTime;

            if (overTotalTime)
                _timeCountSinceLevelStart = totalRealTime;

            var passedTime = GetRoundedPassedTime(_timeCountSinceLevelStart);
            if (_roundedPassedTime.AlmostEqual(passedTime))
                return;

            _roundedPassedTime = passedTime;
            CurrentTime = _startTime.AddHours(_roundedPassedTime);
        }

        private float GetRoundedPassedTime(float passedSeconds)
        {
            var config = ConfigSingletonInstaller.Instance.LevelTimerConfig;
            var startTime = config.GameTimeStartHours;
            var endTime = config.GameTimeEndHours;
            var totalRealTimeSeconds = config.TotalLevelRealTimeSeconds;
            var timeStep = config.GameTimeHourSteps;

            var passedInGameTime = (endTime - startTime) * passedSeconds / totalRealTimeSeconds;
            return timeStep * (int) (passedInGameTime / timeStep);
        }
    }
}
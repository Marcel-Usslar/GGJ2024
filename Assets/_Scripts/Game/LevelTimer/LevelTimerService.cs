using System;
using Game.GameState;
using Game.Utility;
using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.LevelTimer
{
    public class LevelTimerService : SingletonMonoBehaviour<LevelTimerService>
    {
        private float _timeCountSinceLevelStart;
        private DateTime _startTime;
        private float _roundedPassedTime;

        public ReactiveProperty<DateTime> CurrentTime { get; } = new();
        public CallbackHandler OnEndOfDayReached { get; } = new();

        protected override void OnInitialize()
        {
            var config = ConfigSingletonInstaller.Instance.LevelTimerConfig;
            _startTime = DateTime.MinValue.AddHours(config.GameTimeStartHours);
            CurrentTime.Value = _startTime;
        }

        private void Update()
        {
            var totalRealTime = ConfigSingletonInstaller.Instance.LevelTimerConfig.TotalLevelRealTimeSeconds;

            if (GameStateModel.Instance.IsPaused.Value || _timeCountSinceLevelStart >= totalRealTime)
                return;

            _timeCountSinceLevelStart += Time.deltaTime;

            var overTotalTime = _timeCountSinceLevelStart > totalRealTime;

            if (overTotalTime)
            {
                _timeCountSinceLevelStart = totalRealTime;
                OnEndOfDayReached.Trigger();
            }

            _roundedPassedTime = GetRoundedPassedTime(_timeCountSinceLevelStart);
            CurrentTime.Value = _startTime.AddHours(_roundedPassedTime);
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
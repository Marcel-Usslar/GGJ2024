using System;
using Game.GameState;
using Game.LevelManagement;
using Game.Utility;
using Utility;
using Utility.Singletons;

namespace Game.LevelTimer
{
    public class LevelTimerModel : SingletonModel<LevelTimerModel>
    {
        private readonly DateTime _startTime;
        private float _timeCountSinceLevelStart;
        private float _roundedPassedTime;

        public ReactiveProperty<DateTime> CurrentTime { get; } = new();
        public CallbackHandler OnEndOfDayReached { get; } = new();

        public LevelTimerModel()
        {
            var config = ConfigSingletonInstaller.Instance.LevelTimerConfig;
            _startTime = DateTime.MinValue.AddHours(config.GameTimeStartHours);
            CurrentTime.Value = _startTime;

            LevelLoadingModel.Instance.OnLevelLoaded
                .RegisterCallback(() => CurrentTime.Value = _startTime);
        }

        public void AddDeltaTime(float deltaTime)
        {
            var totalRealTime = ConfigSingletonInstaller.Instance.LevelTimerConfig.TotalLevelRealTimeSeconds;

            if (GameStateModel.Instance.IsPaused.Value || _timeCountSinceLevelStart >= totalRealTime)
                return;

            _timeCountSinceLevelStart += deltaTime;

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
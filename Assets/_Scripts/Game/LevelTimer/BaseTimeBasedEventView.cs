using System;
using UnityEngine;

namespace Game.LevelTimer
{
    public abstract class BaseTimeBasedEventView : MonoBehaviour
    {
        [SerializeField] private int _hours;
        [SerializeField] private int _minutes;

        private bool _hasTriggered;

        protected abstract void Trigger();
        protected virtual void OnStart() { }
        protected virtual void OnFinalize() { }

        private void Start()
        {
            LevelTimerService.Instance.CurrentTime.RegisterCallback(TryTriggerEvent);

            OnStart();
        }

        private void OnDestroy()
        {
            LevelTimerService.Instance.CurrentTime.UnregisterCallback(TryTriggerEvent);

            OnFinalize();
        }

        private void TryTriggerEvent(DateTime time)
        {
            if (_hasTriggered || time.Hour < _hours || time.Minute < _minutes)
                return;

            _hasTriggered = true;
            Trigger();
        }
    }
}
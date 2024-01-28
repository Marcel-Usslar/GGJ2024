using System;
using TMPro;
using UnityEngine;

namespace Game.LevelTimer
{
    public class LevelTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private void Start()
        {
            LevelTimerModel.Instance.CurrentTime.RegisterCallback(UpdateCurrentTime);
        }

        private void OnDestroy()
        {
            LevelTimerModel.Instance.CurrentTime.UnregisterCallback(UpdateCurrentTime);
        }

        private void Update()
        {
            LevelTimerModel.Instance.AddDeltaTime(Time.deltaTime);
        }

        private void UpdateCurrentTime(DateTime time)
        {
            _timerText.text = $"{time.Hour:00}:{time.Minute:00}";
        }
    }
}
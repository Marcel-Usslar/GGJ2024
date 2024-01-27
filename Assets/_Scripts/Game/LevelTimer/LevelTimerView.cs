using System;
using Game.Utility;
using TMPro;
using UnityEngine;

namespace Game.LevelTimer
{
    public class LevelTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        public string TimerText { set => _timerText.text = value; }

        float timeCountSinceLevelStart = 0;
        bool timeIsUpdated = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timeIsUpdated) { 
                timeCountSinceLevelStart += Time.deltaTime;
                CheckTimeIsUp();
                TimerText = GetInGameTime(timeCountSinceLevelStart);
            }
            else {
                PrintMessage("TimeUp");
            }
        }

        public void CheckTimeIsUp() {            
            timeIsUpdated = (timeCountSinceLevelStart <= ConfigSingletonInstaller.Instance.LevelTimerConfig.realLifeTimeUntilEndOfLevel_secs);
        }

        public void PrintMessage(string Trigger)
        {
            string messageString = "";

            switch (Trigger) {
                case "TimeUp":
                    messageString = "Your time is up!";
                    break;
                default:
                    break;
            }
            Debug.LogError(messageString);
        }

        public string GetInGameTime(float realLifeTime)
        {
            DateTime startDate = DateTime.MinValue.AddHours(ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameStartTime);
            float passedTimeInGame = (ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameEndTime -
                                ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameStartTime) *
                realLifeTime / ConfigSingletonInstaller.Instance.LevelTimerConfig.realLifeTimeUntilEndOfLevel_secs;
            float quantiledPassedTimeInGame = ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameTimeStep * (int) (passedTimeInGame / ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameTimeStep);
            DateTime currentTimeInGame = startDate.AddHours(quantiledPassedTimeInGame);

            //return currentTimeInGame.ToString(@"hh\:mm");
            return currentTimeInGame.ToString("t");
        }
    }
}
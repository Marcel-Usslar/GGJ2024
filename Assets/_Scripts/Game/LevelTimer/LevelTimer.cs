using Game.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelTimer : MonoBehaviour
{
    float timeCountSinceLevelStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCountSinceLevelStart += Time.deltaTime;
        if (TimeIsUp())
        {
            PrintMessage("TimeUp");
        }
    }

    public bool TimeIsUp() {
        return timeCountSinceLevelStart >= ConfigSingletonInstaller.Instance.LevelTimerConfig.realLifeTimeUntilEndOfLevel_secs;
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
        float passedTime = (ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameEndTime -
            ConfigSingletonInstaller.Instance.LevelTimerConfig.inGameStartTime) *
            realLifeTime / ConfigSingletonInstaller.Instance.LevelTimerConfig.realLifeTimeUntilEndOfLevel_secs;
        DateTime currentTimeInGame = startDate.AddHours(passedTime);
        int hours = currentTimeInGame.Hour;
        int minutes = currentTimeInGame.Minute;
        Debug.LogError($"{hours}:{minutes}");

        return currentTimeInGame.ToString(@"hh\:mm\:ss\:fff");
    }
}

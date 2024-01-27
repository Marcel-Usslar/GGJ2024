using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimerConfig : ScriptableObject
{
    public float realLifeTimeUntilEndOfLevel_secs;
    public float inGameStartTime;
    public float inGameEndTime;
    public float inGameTimeStep;
}

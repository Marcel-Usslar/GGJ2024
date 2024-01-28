using System;
using Utility;
using Utility.Singletons;

namespace Game.LevelTimer
{
    public class LevelTimerModel : SingletonModel<LevelTimerModel>
    {
        public ReactiveProperty<DateTime> CurrentTime { get; } = new();
        public CallbackHandler OnEndOfDayReached { get; } = new();
    }
}
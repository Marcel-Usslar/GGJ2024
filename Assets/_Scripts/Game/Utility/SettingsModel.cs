using Game.UI;
using Utility;
using Utility.Singletons;

namespace Game.Utility
{
    public class SettingsModel : SingletonModel<SettingsModel>
    {
        public ReactiveProperty<bool> IsMusicMuted { get; } = new();

        public ReactiveProperty<float> JoystickSize { get; } = new(1);
        public ReactiveProperty<JoystickSetting> JoystickSetting { get; } = new();
    }
}
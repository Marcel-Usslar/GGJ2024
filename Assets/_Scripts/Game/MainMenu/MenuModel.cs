using Utility;
using Utility.Singletons;

namespace Game.MainMenu
{
    public class MenuModel : SingletonModel<MenuModel>
    {
        public ReactiveProperty<bool> ShowMainMenu { get; } = new(true);
        public ReactiveProperty<bool> ShowLevelSelection { get; } = new();
        public ReactiveProperty<bool> ShowSettings { get; } = new();
        public ReactiveProperty<bool> ShowPauseMenu { get; } = new();
    }
}
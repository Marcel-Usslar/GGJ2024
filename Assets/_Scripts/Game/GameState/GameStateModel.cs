using Utility;
using Utility.Singletons;

namespace Game.GameState
{
    public class GameStateModel : SingletonModel<GameStateModel>
    {
        public ReactiveProperty<bool> IsPaused { get; } = new();
        public bool IsPlaying { get; set; } = false;
    }
}
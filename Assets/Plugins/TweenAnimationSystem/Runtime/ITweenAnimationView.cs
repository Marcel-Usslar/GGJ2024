using DG.Tweening;

namespace Plugins.TweenAnimationSystem
{
    public interface ITweenAnimationView : ITweenAnimationCallbackHandler
    {
        bool IsEnabled { get; }
        bool IsPlaying { get; }

        void PlayAnimation();
        void SkipAnimation();
        void StopAnimation();

        Tween CreateAnimation();
        void ResetAnimation();
    }
}
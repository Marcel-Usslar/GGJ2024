using DG.Tweening;

namespace Plugins.TweenAnimationSystem
{
    public interface IAnimationTween
    {
        Tween CreateAnimation();
        void ResetAnimation();

        IAnimationTween Copy();
    }
}
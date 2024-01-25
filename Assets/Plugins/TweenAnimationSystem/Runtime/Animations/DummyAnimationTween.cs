using System;
using DG.Tweening;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class DummyAnimationTween : IAnimationTween
    {
        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            return sequence;
        }

        public void ResetAnimation()
        { }

        public IAnimationTween Copy()
        {
            return new DummyAnimationTween();
        }
    }
}
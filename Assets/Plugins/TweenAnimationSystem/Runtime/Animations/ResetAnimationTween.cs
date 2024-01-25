using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class ResetAnimationTween : IAnimationTween
    {
        [SerializeField] private TweenAnimationView _animation;

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _animation.ResetAnimation());
            return sequence;
        }

        public void ResetAnimation()
        { }

        public IAnimationTween Copy()
        {
            return new ResetAnimationTween
            {
                _animation = _animation
            };
        }
    }
}
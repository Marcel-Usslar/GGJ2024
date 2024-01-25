using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class IntervalAnimationTween : IAnimationTween
    {
        [SerializeField] private float _duration;

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_duration);
            return sequence;
        }

        public void ResetAnimation()
        { }

        public IAnimationTween Copy()
        {
            return new IntervalAnimationTween
            {
                _duration = _duration
            };
        }
    }
}
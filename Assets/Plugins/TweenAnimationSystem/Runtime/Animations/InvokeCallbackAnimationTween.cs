using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class InvokeCallbackAnimationTween : IAnimationTween
    {
        [Serializable]
        private class AnimationEvent : UnityEvent {}

        [SerializeField] private AnimationEvent _animationEvent = new AnimationEvent();

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _animationEvent.Invoke());
            return sequence;
        }

        public void ResetAnimation()
        { }

        public IAnimationTween Copy()
        {
            return new InvokeCallbackAnimationTween
            {
                _animationEvent = _animationEvent.Copy()
            };
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class PlayParticleEffectAnimationTween : IAnimationTween
    {
        [SerializeField] private ParticleSystem _target;
        [SerializeField] private bool _withChildren;

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _target.Play(_withChildren));
            return sequence;
        }

        public void ResetAnimation()
        {
            _target.Stop();
        }

        public IAnimationTween Copy()
        {
            return new PlayParticleEffectAnimationTween
            {
                _target = _target,
                _withChildren = _withChildren
            };
        }
    }
}
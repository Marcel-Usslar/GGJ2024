using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class ToggleGameObjectAnimationTween : IAnimationTween
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _finalState;

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _target.SetActive(_finalState));
            return sequence;
        }

        public void ResetAnimation()
        {
            _target.SetActive(!_finalState);
        }

        public IAnimationTween Copy()
        {
            return new ToggleGameObjectAnimationTween
            {
                _target = _target,
                _finalState = _finalState
            };
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class SceneReferenceAnimationTween : IAnimationTween
    {
        [SerializeField] private TweenAnimationView _sceneReference;
        [Tooltip("When this flag is set, the sequence will wait for the reference animation to be done before continuing.")]
        [SerializeField] private bool _isBlocking = true;

        public Tween CreateAnimation()
        {
            if (!_sceneReference.IsEnabled)
                return DOTween.Sequence();

            if (_isBlocking)
                return _sceneReference.CreateAnimation();

            var sequence = DOTween.Sequence();
            sequence.AppendCallback(_sceneReference.PlayAnimation);
            return sequence;
        }

        public void ResetAnimation()
        {
            if (_sceneReference.IsEnabled)
                _sceneReference.ResetAnimation();
        }

        public IAnimationTween Copy()
        {
            return new SceneReferenceAnimationTween
            {
                _sceneReference = _sceneReference,
                _isBlocking = _isBlocking
            };
        }
    }
}
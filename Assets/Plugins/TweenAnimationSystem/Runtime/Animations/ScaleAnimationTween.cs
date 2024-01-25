using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class ScaleAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private Transform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private ScaleAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _startScale;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _endScale;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            var startScale = _useConfig ? _config.StartScale : _startScale;
            _target.localScale = startScale * Vector3.one;
        }

        protected override Tween Create()
        {
            var endScale = _useConfig ? _config.EndScale : _endScale;
            var duration = _useConfig ? _config.Duration : _duration;

            return _target.DOScale(endScale, duration);
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new ScaleAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _startScale = _startScale,
                _endScale = _endScale
            };
        }
    }
}
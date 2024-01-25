using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class RotateAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private Transform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private RotateAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector3 _startRotation;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector3 _endRotation;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private RotateMode _rotateMode;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            var startRotation = _useConfig ? _config.StartRotation : _startRotation;
            _target.localRotation = Quaternion.Euler(startRotation);
        }

        protected override Tween Create()
        {
            var endRotation = _useConfig ? _config.EndRotation : _endRotation;
            var duration = _useConfig ? _config.Duration : _duration;
            var rotateMode = _useConfig ? _config.RotateMode : _rotateMode;

            return _target.DOLocalRotate(endRotation, duration, rotateMode);
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new RotateAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _startRotation = _startRotation,
                _endRotation = _endRotation,
                _rotateMode = _rotateMode
            };
        }
    }
}
using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class PunchRotateAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private Transform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private PunchRotationAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector3 _punchRotation;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private int _vibrato;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _elasticity;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            _target.localRotation = Quaternion.identity;
        }

        protected override Tween Create()
        {
            var punchRotation  = _useConfig ? _config.PunchRotation : _punchRotation;
            var duration = _useConfig ? _config.Duration : _duration;
            var vibrato = _useConfig ? _config.Vibrato : _vibrato;
            var elasticity = _useConfig ? _config.Elasticity : _elasticity;

            return _target.DOPunchRotation(punchRotation, duration, vibrato, elasticity);
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new PunchRotateAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _punchRotation = _punchRotation,
                _vibrato = _vibrato,
                _elasticity = _elasticity
            };
        }
    }
}
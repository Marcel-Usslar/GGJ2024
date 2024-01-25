using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class PunchScaleAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private Transform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private PunchScaleAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _punchScale;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private int _vibrato;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _elasticity;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            _target.localScale = Vector3.one;
        }

        protected override Tween Create()
        {
            var punchScale  = _useConfig ? _config.PunchScale : _punchScale;
            var duration = _useConfig ? _config.Duration : _duration;
            var vibrato = _useConfig ? _config.Vibrato : _vibrato;
            var elasticity = _useConfig ? _config.Elasticity : _elasticity;

            return _target.DOPunchScale(Vector3.one * punchScale, duration, vibrato, elasticity);
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new PunchScaleAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _punchScale = _punchScale,
                _vibrato = _vibrato,
                _elasticity = _elasticity
            };
        }
    }
}
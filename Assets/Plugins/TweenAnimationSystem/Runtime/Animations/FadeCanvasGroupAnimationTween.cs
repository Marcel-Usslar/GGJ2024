using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class FadeCanvasGroupAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private CanvasGroup _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private FadeAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _startAlpha;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _endAlpha;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            _target.alpha = _useConfig ? _config.StartAlpha : _startAlpha;
        }

        protected override Tween Create()
        {
            var startAlpha = _useConfig ? _config.StartAlpha : _startAlpha;
            var endAlpha = _useConfig ? _config.EndAlpha : _endAlpha;
            var duration = _useConfig ? _config.Duration : _duration;

            return DOVirtual.Float(startAlpha, endAlpha, duration, value => _target.alpha = value);
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new FadeCanvasGroupAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _startAlpha = _startAlpha,
                _endAlpha = _endAlpha
            };
        }
    }
}
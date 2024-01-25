using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class FadeGraphicAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private Graphic _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private FadeAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _startAlpha;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _endAlpha;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            SetAlpha(_useConfig ? _config.StartAlpha : _startAlpha);
        }

        protected override Tween Create()
        {
            var startAlpha = _useConfig ? _config.StartAlpha : _startAlpha;
            var endAlpha = _useConfig ? _config.EndAlpha : _endAlpha;
            var duration = _useConfig ? _config.Duration : _duration;

            return DOVirtual.Float(startAlpha, endAlpha, duration, SetAlpha);
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new FadeGraphicAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _startAlpha = _startAlpha,
                _endAlpha = _endAlpha
            };
        }

        private void SetAlpha(float alpha)
        {
            _target.color = GetColorWithAlpha(_target.color, alpha);
        }

        private Color GetColorWithAlpha(Color color, float alpha)
        {
            return new Color(color.r,color.g, color.b, alpha);
        }
    }
}
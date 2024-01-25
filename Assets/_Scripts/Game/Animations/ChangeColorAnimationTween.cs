using System;
using DG.Tweening;
using Game.Animations;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Animations
{
    [Serializable]
    public class ChangeColorAnimationTween : AbstractCustomAnimationTween
    {
        [SerializeField] private Graphic _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private ChangeColorAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Color _startColor;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Color _endColor;

        public override int Id => (int) CustomAnimationTypes.ChangeColor;
        public override string DisplayName => "Change Color";

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            _target.color = _useConfig ? _config.StartColor : _startColor;
        }

        protected override Tween Create()
        {
            var startColor = _useConfig ? _config.StartColor : _startColor;
            var endColor = _useConfig ? _config.EndColor : _endColor;
            var duration = _useConfig ? _config.Duration : _duration;

            return DOVirtual.Float(0, 1, duration, t => _target.color = Color.Lerp(startColor, endColor, t));
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new ChangeColorAnimationTween
            {
                _target = _target,
                _config = _config,
                _duration = _duration,
                _startColor = _startColor,
                _endColor = _endColor
            };
        }
    }
}
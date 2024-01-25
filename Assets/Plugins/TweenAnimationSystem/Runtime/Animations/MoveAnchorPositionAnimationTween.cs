using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Animations
{
    [Serializable]
    internal class MoveAnchorPositionAnimationTween : AbstractAnimationTween
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private RectTransform _startPosition;
        [SerializeField] private RectTransform _endPosition;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private MoveAnchorPositionAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            _target.anchoredPosition = _startPosition.anchoredPosition;
        }

        protected override Tween Create()
        {
            var duration = _useConfig ? _config.Duration : _duration;
            var startPosition = _startPosition.anchoredPosition;
            var endPosition = _endPosition.anchoredPosition;

            return DOVirtual.Float(0, 1, duration,
                value => _target.anchoredPosition = Vector2.Lerp(startPosition, endPosition, value));
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new MoveAnchorPositionAnimationTween
            {
                _target = _target,
                _startPosition = _startPosition,
                _endPosition = _endPosition,
                _config = _config,
                _duration = _duration
            };
        }
    }
}
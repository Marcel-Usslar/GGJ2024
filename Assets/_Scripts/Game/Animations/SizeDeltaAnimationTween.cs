using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    [Serializable]
    public class SizeDeltaAnimationTween : AbstractCustomAnimationTween
    {
        [SerializeField] private RectTransform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private SizeDeltaAnimationConfig _config;
        [Header("0 will stretch the size to the parent")]
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector2 _startSize;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector2 _endSize;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;

        public override int Id => (int) CustomAnimationTypes.SizeDelta;
        public override string DisplayName => "Size Delta";

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            var startSize = _useConfig ? _config.StartSize : _startSize;
            _target.sizeDelta = startSize;
        }

        protected override Tween Create()
        {
            var startSize = _useConfig ? _config.StartSize : _startSize;
            var endSize = _useConfig ? _config.EndSize : _endSize;
            var duration = _useConfig ? _config.Duration : _duration;

            return DOTween.Sequence()
                .AppendCallback(() => _target.sizeDelta = startSize)
                .Append(_target.DOSizeDelta(endSize, duration));
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new SizeDeltaAnimationTween
            {
                _target = _target,
                _config = _config,
                _startSize = _startSize,
                _endSize = _endSize,
                _duration = _duration,
            };
        }
    }
}
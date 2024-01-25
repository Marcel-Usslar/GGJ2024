using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Animations
{
    [Serializable]
    public class LayoutElementPreferredSizeAnimationTween : AbstractCustomAnimationTween
    {
        [SerializeField] private LayoutElement _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private LayoutElementPreferredSizeAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private bool _changeWidth;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private bool _changeHeight;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector2 _startSize;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector2 _endSize;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;

        public override int Id => (int) CustomAnimationTypes.LayoutElementPreferredSize;
        public override string DisplayName => "Layout Element Preferred Size";

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            UpdateSize(_useConfig ? _config.StartSize : _startSize);
        }

        protected override Tween Create()
        {
            var startSize = _useConfig ? _config.StartSize : _startSize;
            var endSize = _useConfig ? _config.EndSize : _endSize;
            var duration = _useConfig ? _config.Duration : _duration;

            return DOTween.Sequence()
                .AppendCallback(() => UpdateSize(startSize))
                .Append(DOVirtual.Float(0, 1, duration,
                value => UpdateSize(Vector2.Lerp(startSize, endSize, value))));
        }

        private void UpdateSize(Vector2 size)
        {
            var changeWidth = _useConfig ? _config.ChangeWidth : _changeWidth;
            if (changeWidth)
                _target.preferredWidth = size.x;

            var changeHeight = _useConfig ? _config.ChangeHeight : _changeHeight;
            if (changeHeight)
                _target.preferredHeight = size.y;
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new LayoutElementPreferredSizeAnimationTween
            {
                _target = _target,
                _config = _config,
                _changeWidth = _changeWidth,
                _changeHeight = _changeHeight,
                _startSize = _startSize,
                _endSize = _endSize,
                _duration = _duration,
            };
        }
    }
}
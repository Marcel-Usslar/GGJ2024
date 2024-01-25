using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    [Serializable]
    public class MoveTransformAnimationTween : AbstractCustomAnimationTween
    {
        [SerializeField] private Transform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private MoveTransformAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private bool _useLocal;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private bool _useRelative;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Transform _startPosition;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Transform _endPosition;

        public override int Id => (int) CustomAnimationTypes.MoveTransform;
        public override string DisplayName => "Move Transform";

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            var useLocal = _useConfig ? _config.UseLocal : _useLocal;
            var startPosition = GetStartPosition();
            
            if (useLocal)
            {
                _target.localPosition = startPosition;
                return;
            }

            _target.position = startPosition;
        }

        protected override Tween Create()
        {
            var useLocal = _useConfig ? _config.UseLocal : _useLocal;
            var useRelative = _useConfig ? _config.UseRelative : _useRelative;
            var duration = _useConfig ? _config.Duration : _duration;
            var startPosition = GetStartPosition();
            var endPosition = GetEndPosition();
            
            var sequence = DOTween.Sequence();

            if (useLocal)
            {
                sequence
                    .AppendCallback(() => _target.localPosition = startPosition)
                    .Append(_target.DOLocalMove(endPosition, duration)
                        .SetRelative(useRelative));
            }
            else
            {
                sequence
                    .AppendCallback(() => _target.position = startPosition)
                    .Append(_target.DOMove(endPosition, duration)
                        .SetRelative(useRelative));
            }

            return sequence;
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new MoveTransformAnimationTween
            {
                _target = _target,
                _config = _config,
                _useLocal = _useLocal,
                _useRelative = _useRelative,
                _duration = _duration,
                _startPosition = _startPosition,
                _endPosition = _endPosition
            };
        }

        private Vector3 GetStartPosition()
        {
            return _useConfig ? _config.StartPosition :
                _useLocal ? _startPosition.localPosition : _startPosition.position;
        }

        private Vector3 GetEndPosition()
        {
            return _useConfig ? _config.EndPosition :
                _useLocal ? _endPosition.localPosition : _endPosition.position;
        }
    }
}
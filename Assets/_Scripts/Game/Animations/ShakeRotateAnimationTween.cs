using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    [Serializable]
    public class ShakeRotateAnimationTween : AbstractCustomAnimationTween
    {
        [SerializeField] private Transform _target;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private ShakeRotateAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private Vector3 _shakeStrength;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private int _vibrato;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _randomness;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;

        protected override AbstractAnimationConfig Config => _config;

        public override int Id => (int) CustomAnimationTypes.ShakeRotate;
        public override string DisplayName => "Shake Rotate";

        protected override Tween Create()
        {
            var duration = _useConfig ? _config.Duration : _duration;
            var strength  = _useConfig ? _config.ShakeStrength : _shakeStrength;
            var vibrato = _useConfig ? _config.Vibrato : _vibrato;
            var randomness = _useConfig ? _config.Randomness : _randomness;
            return _target.DOShakeRotation(duration, strength, vibrato, randomness);
        }

        public override void ResetAnimation()
        {
            _target.localRotation = Quaternion.identity;
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new ShakeRotateAnimationTween
            {
                _target = _target,
                _config = _config,
                _shakeStrength = _shakeStrength,
                _vibrato = _vibrato,
                _randomness = _randomness,
                _duration = _duration,
            };
        }
    }
}
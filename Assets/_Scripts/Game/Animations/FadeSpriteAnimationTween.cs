using System;
using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using Plugins.TweenAnimationSystem.Configs;
using UnityEngine;
using Utility;

namespace Game.Animations
{
    [Serializable]
    public class FadeSpriteAnimationTween : AbstractCustomAnimationTween
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [AllowNesting, ShowIf(nameof(_useConfig))] [SerializeField] private FadeAnimationConfig _config;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _startAlpha;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _endAlpha;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] private float _duration;

        public override int Id => (int) CustomAnimationTypes.FadeSprite;
        public override string DisplayName => "Fade Sprite Renderer";

        protected override AbstractAnimationConfig Config => _config;

        public override void ResetAnimation()
        {
            _spriteRenderer.SetAlpha(_startAlpha);
        }

        protected override Tween Create()
        {
            var startAlpha = _useConfig ? _config.StartAlpha : _startAlpha;
            var endAlpha = _useConfig ? _config.EndAlpha : _endAlpha;
            var duration = _useConfig ? _config.Duration : _duration;

            return DOVirtual.Float(startAlpha, endAlpha, duration, value => _spriteRenderer.SetAlpha(value));
        }

        protected override AbstractAnimationTween CopyInternal()
        {
            return new FadeSpriteAnimationTween
            {
                _spriteRenderer = _spriteRenderer,
                _config = _config,
                _startAlpha = _startAlpha,
                _endAlpha = _endAlpha,
                _duration = _duration
            };
        }
    }
}
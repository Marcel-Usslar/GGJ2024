using System;
using DG.Tweening;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Animations
{
    [Serializable]
    public class SetSpriteAnimationTween : ICustomAnimationTween
    {
        [SerializeField] private Image _target;
        [SerializeField] private Sprite _sprite;

        public int Id => (int) CustomAnimationTypes.SetSprite;
        public string DisplayName => "Set Sprite";

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _target.sprite = _sprite);
            return sequence;
        }

        public void ResetAnimation()
        { }

        public IAnimationTween Copy()
        {
            return new SetSpriteAnimationTween
            {
                _target = _target,
                _sprite = _sprite
            };
        }
    }
}
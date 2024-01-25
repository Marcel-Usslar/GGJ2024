using System;
using DG.Tweening;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Animations
{
    [Serializable]
    public class SetColorAnimationTween : ICustomAnimationTween
    {
        [SerializeField] private Graphic _target;
        [SerializeField] private Color _color;

        public int Id => (int) CustomAnimationTypes.SetColor;
        public string DisplayName => "Set Color";

        public Tween CreateAnimation()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _target.color = _color);
            return sequence;
        }

        public void ResetAnimation()
        { }

        public IAnimationTween Copy()
        {
            return new SetColorAnimationTween
            {
                _target = _target,
                _color = _color
            };
        }
    }
}
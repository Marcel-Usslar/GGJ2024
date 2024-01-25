using DG.Tweening;
using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    public class ChangeColorAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private float _duration;
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;
        [SerializeField] private Ease _fadeEase;

        public float Duration => _duration;
        public Color StartColor => _startColor;
        public Color EndColor => _endColor;
        public Ease FadeEase => _fadeEase;
    }
}
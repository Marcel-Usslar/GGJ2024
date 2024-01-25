using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    public class LayoutElementPreferredSizeAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private bool _changeWidth;
        [SerializeField] private bool _changeHeight;
        [SerializeField] private Vector2 _startSize;
        [SerializeField] private Vector2 _endSize;
        [SerializeField] private float _duration;

        public bool ChangeWidth => _changeWidth;
        public bool ChangeHeight => _changeHeight;
        public Vector2 StartSize => _startSize;
        public Vector2 EndSize => _endSize;
        public float Duration => _duration;
    }
}
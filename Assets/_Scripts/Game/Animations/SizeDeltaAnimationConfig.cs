using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    public class SizeDeltaAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private Vector2 _startSize;
        [SerializeField] private Vector2 _endSize;
        [SerializeField] private float _duration;

        public Vector2 StartSize => _startSize;
        public Vector2 EndSize => _endSize;
        public float Duration => _duration;
    }
}
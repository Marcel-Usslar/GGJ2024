using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    public class MoveTransformAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private bool _useLocal;
        [SerializeField] private bool _useRelative;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;

        public bool UseLocal => _useLocal;
        public bool UseRelative => _useRelative;
        public float Duration => _duration;
        public Vector3 StartPosition => _startPosition;
        public Vector3 EndPosition => _endPosition;
    }
}
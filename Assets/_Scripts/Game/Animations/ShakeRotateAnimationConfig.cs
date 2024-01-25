using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Animations
{
    public class ShakeRotateAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private Vector3 _shakeStrength;
        [SerializeField] private float _duration;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;

        public Vector3 ShakeStrength => _shakeStrength;
        public float Duration => _duration;
        public int Vibrato => _vibrato;
        public float Randomness => _randomness;
    }
}
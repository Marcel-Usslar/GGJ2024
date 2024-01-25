using UnityEngine;

namespace Plugins.TweenAnimationSystem.Configs
{
    public class FadeAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _startAlpha;
        [SerializeField] private float _endAlpha;

        public float Duration => _duration;
        public float StartAlpha => _startAlpha;
        public float EndAlpha => _endAlpha;
    }
}
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Configs
{
    public class ScaleAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _startScale;
        [SerializeField] private float _endScale;

        public float Duration => _duration;
        public float StartScale => _startScale;
        public float EndScale => _endScale;
    }
}
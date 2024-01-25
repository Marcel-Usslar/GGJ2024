using UnityEngine;

namespace Plugins.TweenAnimationSystem.Configs
{
    public class PunchScaleAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private float _punchScale;
        [SerializeField] private float _duration;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _elasticity;

        public float PunchScale => _punchScale;
        public float Duration => _duration;
        public int Vibrato => _vibrato;
        public float Elasticity => _elasticity;
    }
}
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Configs
{
    public class PunchRotationAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private Vector3 _punchRotation;
        [SerializeField] private float _duration;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _elasticity;

        public Vector3 PunchRotation => _punchRotation;
        public float Duration => _duration;
        public int Vibrato => _vibrato;
        public float Elasticity => _elasticity;
    }
}
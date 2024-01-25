using DG.Tweening;
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Configs
{
    public class RotateAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _startRotation;
        [SerializeField] private Vector3 _endRotation;
        [SerializeField] private RotateMode _rotateMode;

        public float Duration => _duration;
        public Vector3 StartRotation => _startRotation;
        public Vector3 EndRotation => _endRotation;
        public RotateMode RotateMode => _rotateMode;
    }
}
using UnityEngine;

namespace Plugins.TweenAnimationSystem.Configs
{
    public class MoveAnchorPositionAnimationConfig : AbstractAnimationConfig
    {
        [SerializeField] private float _duration;

        public float Duration => _duration;
    }
}
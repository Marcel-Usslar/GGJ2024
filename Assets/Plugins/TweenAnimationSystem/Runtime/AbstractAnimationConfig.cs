using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Plugins.TweenAnimationSystem
{
    public abstract class AbstractAnimationConfig : ScriptableObject
    {
        [SerializeField] private bool _useAnimationCurve;
        [ShowIf(nameof(_useAnimationCurve))] [SerializeField] private AnimationCurve _animationCurve;
        [HideIf(nameof(_useAnimationCurve))] [SerializeField] private Ease _ease;

        public bool UseAnimationCurve => _useAnimationCurve;
        public Ease Ease => _ease;
        public AnimationCurve AnimationCurve => _animationCurve;
    }
}
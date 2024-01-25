using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Plugins.TweenAnimationSystem
{
    [Serializable]
    internal class AnimationTweenData
    {
        [HideInInspector] [SerializeField] private string _name;
        [AnimationTypeDisplay] [SerializeField] private AnimationType _type;
        [AllowNesting, HideIf(nameof(_type), AnimationType.Interval)]
        [SerializeField] private AnimationCombinationType _combinationType;
        [Space]
        [SerializeReference] private IAnimationTween _animationTween;

        public AnimationCombinationType CombinationType => _combinationType;

        public Tween CreateAnimation()
        {
            return _animationTween.CreateAnimation();
        }

        public void ResetAnimation()
        {
            _animationTween.ResetAnimation();
        }

        public void UpdateAnimationTween()
        {
            if (_animationTween != null && _animationTween.GetType() == _type.GetAnimationTweenType())
                return;

            _animationTween = _type.GetAnimationTween();
            _name = _type.GetAnimationTypeName();
        }

        public bool IsDuplicateReference(AnimationTweenData data)
        {
            return ReferenceEquals(data._animationTween, _animationTween);
        }

        public void CreateAnimationDeepCopy()
        {
            _animationTween = _animationTween.Copy();
        }
    }
}
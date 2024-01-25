using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Plugins.TweenAnimationSystem
{
    [Serializable]
    public abstract class AbstractAnimationTween : IAnimationTween
    {
        [SerializeField] protected bool _useConfig;
        [AllowNesting, HideIf(nameof(_useConfig))] [SerializeField] protected bool _useAnimationCurve;
        [AllowNesting, ShowIf(nameof(_useAnimationCurve))] [SerializeField] protected AnimationCurve _animationCurve;
        [AllowNesting, HideIf(nameof(_useAnimationCurve))] [SerializeField] protected Ease _ease;
        [SerializeField] private bool _looping;
        [AllowNesting, ShowIf(nameof(_looping))] [SerializeField] private int _loops;
        [AllowNesting, ShowIf(nameof(_looping))] [SerializeField] private LoopType _loopType;

        protected abstract AbstractAnimationConfig Config { get; }

        public Tween CreateAnimation()
        {
            var tween = Create();

            if (_useConfig && Config.UseAnimationCurve || _useAnimationCurve)
                tween.SetEase(_useConfig ? Config.AnimationCurve : _animationCurve);
            else
                tween.SetEase(_useConfig ? Config.Ease : _ease);

            if (_looping)
                tween.SetLoops(_loops, _loopType);

            return tween;
        }

        public IAnimationTween Copy()
        {
            var copy = CopyInternal();
            copy._useConfig = _useConfig;
            copy._useAnimationCurve = _useAnimationCurve;
            copy._animationCurve = _animationCurve;
            copy._ease = _ease;
            copy._looping = _looping;
            copy._loops = _loops;
            copy._loopType = _loopType;

            return copy;
        }

        public abstract void ResetAnimation();
        protected abstract Tween Create();
        protected abstract AbstractAnimationTween CopyInternal();
    }
}
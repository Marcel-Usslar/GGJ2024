using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Utility;

namespace Plugins.TweenAnimationSystem
{
    public class TweenAnimationView : MonoBehaviour, ITweenAnimationView
    {
        [SerializeField] private bool _playOnStart;
        [SerializeField] private bool _ignoreTimeScale = true;
        [SerializeField] private LinkBehaviour _linkBehaviour = LinkBehaviour.KillOnDestroy;
        [SerializeField] private bool _looping;
        [Tooltip("-1 => Infinite loops")]
        [ShowIf(nameof(_looping))] [SerializeField] private int _loops;
        [ShowIf(nameof(_looping))] [SerializeField] private LoopType _loopType;
        [Space]
        [SerializeField] private List<AnimationTweenData> _tweenData;

        private readonly CallbackHandler _animationFinishedHandler = new();
        private Tween _tween;
        public bool IsEnabled => enabled;
        public bool IsPlaying => _tween?.IsPlaying() ?? false;

        public void PlayAnimation()
        {
            if (IsPlaying)
                _tween.Kill();

            _tween = CreateAnimation();
            _tween.OnComplete(() =>
            {
                _tween = null;
                _animationFinishedHandler.Trigger();
            });
            _tween.OnKill(() =>
            {
                _tween = null;
            });

            _tween.Restart();
        }

        public void SkipAnimation()
        {
            if (IsPlaying)
                _tween.timeScale = 100;
        }

        public void StopAnimation()
        {
            _tween?.Kill();
        }

        public Tween CreateAnimation()
        {
            ResetAnimation();

            var animationSequence = CreateSequence();

            animationSequence
                .SetUpdate(_ignoreTimeScale)
                .SetLink(gameObject, _linkBehaviour)
                .Pause();

            return animationSequence;
        }

        public void ResetAnimation()
        {
            for (var i = _tweenData.Count - 1; i >= 0; i--)
            {
                _tweenData[i]?.ResetAnimation();
            }
        }

        public void RegisterAnimationFinishedHandler(Action handler)
        {
            _animationFinishedHandler.RegisterCallback(handler);
        }

        public void UnregisterAnimationFinishedHandler(Action handler)
        {
            _animationFinishedHandler.UnregisterCallback(handler);
        }

        private Sequence CreateSequence()
        {
            var sequence = AnimationSequenceBuilder.BuildSequence(_tweenData);

            if (_looping)
                sequence.SetLoops(_loops, _loopType);

            return sequence;
        }

        private void Start()
        {
            if (_playOnStart)
                PlayAnimation();
        }

        private void OnValidate()
        {
            if (_tweenData == null)
                _tweenData = new List<AnimationTweenData>();

            for (var i = 0; i < _tweenData.Count; i++)
            {
                if (_tweenData[i] == null)
                    _tweenData[i] = new AnimationTweenData();
                else
                    ReplaceIfDuplicate(i);

                _tweenData[i].UpdateAnimationTween();
            }
        }

        private void ReplaceIfDuplicate(int index)
        {
            var data = _tweenData[index];
            for (var i = 0; i < index; i++)
            {
                var existingData = _tweenData[i];
                if (data.IsDuplicateReference(existingData))
                {
                    data.CreateAnimationDeepCopy();
                    return;
                }
            }
        }

        [Button("Start")]
        private void Restart()
        {
            if (Application.isPlaying)
                PlayAnimation();
            else
                CreateAnimation().PlayOnceInEditor();
        }

        [Button("Reset")]
        private void Stop()
        {
            if (Application.isPlaying)
                StopAnimation();
            else
                TweenUtility.StopEditorAnimations();

            ResetAnimation();
        }
    }
}
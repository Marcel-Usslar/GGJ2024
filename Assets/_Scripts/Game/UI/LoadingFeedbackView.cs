using Plugins.TweenAnimationSystem;
using UnityEngine;
using Utility;

namespace Game.UI
{
    public class LoadingFeedbackView : BaseSingletonScreen<LoadingFeedbackView>
    {
        [SerializeField] private TweenAnimationView _fadeInTween;
        [SerializeField] private TweenAnimationView _fadeOutTween;

        protected override ReactiveProperty<bool> Visibility => LoadingFeedbackModel.Instance.ShowLoadingFeedback;

        protected override void OnStart()
        {
            _fadeInTween.ResetAnimation();

            _fadeInTween.RegisterAnimationFinishedHandler(() =>
                LoadingFeedbackModel.Instance.OnFadeInCompleted.Trigger());
            _fadeOutTween.RegisterAnimationFinishedHandler(() =>
                LoadingFeedbackModel.Instance.OnFadeOutCompleted.Trigger());
        }

        protected override void OnFinalize()
        {
            _fadeInTween.UnregisterAnimationFinishedHandler(() =>
                LoadingFeedbackModel.Instance.OnFadeInCompleted.Trigger());
            _fadeOutTween.UnregisterAnimationFinishedHandler(() =>
                LoadingFeedbackModel.Instance.OnFadeOutCompleted.Trigger());
        }

        protected override void OnVisibilityChanged(bool visible)
        {
            if (visible)
            {
                _fadeOutTween.StopAnimation();
                _fadeInTween.PlayAnimation();
            }
            else
            {
                _fadeInTween.StopAnimation();
                _fadeOutTween.PlayAnimation();
            }
        }
    }
}
using Plugins.TweenAnimationSystem;
using UnityEngine;

namespace Game.Input
{
    public class InputTutorialComponent : MonoBehaviour
    {
        [SerializeField] private InputView _inputView;
        [SerializeField] private TweenAnimationView _tutorialAnimation;
        [SerializeField] private float _tutorialWaitDuration;

        private bool _hasUsedInput;
        private bool _hasShownTutorial;
        private float _tutorialTimer;

        private void FixedUpdate()
        {
            UpdateTutorialTimer();
            TryStopTutorialAnimation();
        }

        private void UpdateTutorialTimer()
        {
            if (_hasUsedInput || _hasShownTutorial)
                return;

            if (_inputView.HasInput)
            {
                _hasUsedInput = true;
                return;
            }

            _tutorialTimer += Time.fixedDeltaTime;

            if (_tutorialTimer < _tutorialWaitDuration)
                return;

            _hasShownTutorial = true;
            _tutorialAnimation.PlayAnimation();
        }

        private void TryStopTutorialAnimation()
        {
            if (_hasUsedInput || !_hasShownTutorial)
                return;

            if (!_inputView.HasInput)
                return;

            _hasUsedInput = true;
            _tutorialAnimation.StopAnimation();
        }
    }
}
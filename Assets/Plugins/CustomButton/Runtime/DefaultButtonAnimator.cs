using DG.Tweening;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CustomButton
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ReactiveButton))]
    public class DefaultButtonAnimator : ButtonDecoratorComponent
    {
        [SerializeField] private ReactiveButton _button;
        [SerializeField] private TweenAnimationView _pointerUpAnimationView;
        [SerializeField] private TweenAnimationView _pointerDownAnimationView;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _nonInteractableColor;

        private Tween _pointerUpAnimation;
        private Tween _pointerDownAnimation;

        private Color _defaultColor;

        public Color ButtonColor
        {
            set
            {
                _defaultColor = value;
                if (_button.Interactable && _buttonImage != null)
                    _buttonImage.color = value;
            }
        }

        private Tween PointerUpAnimation => _pointerUpAnimation ??= CreatePointerUpAnimation();
        private Tween PointerDownAnimation => _pointerDownAnimation ??= CreatePointerDownAnimation();

        public override void OnPointerUp(bool interactable)
        {
            PointerUpAnimation.Restart();
        }

        public override void OnPointerDown(bool interactable)
        {
            PointerDownAnimation.Restart();
        }

        public override void OnInteractabilityChanged(bool interactable)
        {
            if (_buttonImage == null)
                return;

            _buttonImage.color = interactable ? _defaultColor : _nonInteractableColor;
        }

        private void Start()
        {
            if (_buttonImage == null)
                return;

            _defaultColor = _buttonImage.color;
        }

        private Tween CreatePointerUpAnimation()
        {
            var tween = _pointerUpAnimationView.CreateAnimation();
            tween.SetAutoKill(false);
            return tween;
        }

        private Tween CreatePointerDownAnimation()
        {
            var tween = _pointerDownAnimationView.CreateAnimation();
            tween.SetAutoKill(false);
            return tween;
        }

        private void OnValidate()
        {
            if (_button == null)
                _button = GetComponent<ReactiveButton>();
        }
    }
}
using System;
using Game.Camera;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Game.Input
{
    public class JoystickButtonView : InputView, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private float _minimumJoystickRange;
        [SerializeField] private float _maximumJoystickRange;
        [SerializeField] private float _joystickButtonWaitDuration;
        [Space]
        [SerializeField] private RectTransform _joystickContainer;
        [SerializeField] private RectTransform _joystickDisplayContainer;
        [SerializeField] private RectTransform _joystick;
        [Space]
        [SerializeField] private TweenAnimationView _joystickAppearAnimation;
        [SerializeField] private TweenAnimationView _joystickDisappearAnimation;

        private bool _isTouching;
        private float _touchSeconds;
        private bool _isShowingJoystick;
        private Vector2 _lastInput;

        private readonly CallbackHandler<Vector3> _inputHandler = new();
        public override bool HasInput => _isTouching;

        public void RegisterInputHandler(Action<Vector3> handler)
        {
            _inputHandler.RegisterCallback(handler);
        }

        public void UnregisterInputHandler(Action<Vector3> handler)
        {
            _inputHandler.UnregisterCallback(handler);
        }

        private void FixedUpdate()
        {
            if (!_isTouching || _isShowingJoystick)
                return;

            if (!_isShowingJoystick)
                _touchSeconds += Time.fixedDeltaTime;

            if (_touchSeconds < _joystickButtonWaitDuration)
                return;

            _isShowingJoystick = true;
            _joystickAppearAnimation.PlayAnimation();
            _joystickDisappearAnimation.ResetAnimation();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isShowingJoystick)
            {
                _joystickAppearAnimation.ResetAnimation();
                _joystickDisappearAnimation.PlayAnimation();
            }

            _inputHandler.Trigger(_isShowingJoystick && _lastInput.magnitude >= _minimumJoystickRange
                ? new Vector3(_lastInput.x, 0, _lastInput.y).normalized
                : Vector3.zero);

            _isTouching = false;
            _touchSeconds = 0;
            _isShowingJoystick = false;

            _joystick.anchoredPosition = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isTouching = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isShowingJoystick)
                return;

            var pointerPosition = GetLocalPosition(eventData.position);
            _lastInput = pointerPosition - _joystickDisplayContainer.anchoredPosition;
            _joystick.anchoredPosition = _lastInput.magnitude > _maximumJoystickRange
                ? _lastInput.normalized * _maximumJoystickRange
                : _lastInput;
        }

        private Vector2 GetLocalPosition(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickContainer, position,
                CameraView.Instance.Camera, out var localPoint);

            return localPoint;
        }
    }
}
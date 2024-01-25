using Game.Camera;
using Game.UI;
using Game.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Input
{
    public class JoystickView : InputView, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private bool _allowKeyboardInput;
        [SerializeField] private bool _hideJoystickOnRelease;
        [SerializeField] private float _defaultJoystickRange;
        [SerializeField] private float _defaultJoystickSize;
        [SerializeField] private float _defaultJoystickOffset;
        [SerializeField] private float _defaultJoystickDragSpeed;
        [Space]
        [SerializeField] private RectTransform _joystickContainer;
        [SerializeField] private RectTransform _joystickDisplayContainer;
        [SerializeField] private RectTransform _joystick;

        private JoystickSetting _lastJoystickSetting;

        private Vector2 _lastPointerPosition;
        private bool _isTouching;

        public override bool HasInput => _isTouching;

        private void Awake()
        {
            UnityEngine.Input.multiTouchEnabled = true;

            _lastJoystickSetting = SettingsModel.Instance.JoystickSetting.Value;

            SettingsModel.Instance.JoystickSize.RegisterCallback(UpdateJoystickSize);
            SettingsModel.Instance.JoystickSetting.RegisterCallback(OnUpdateJoystickSettings);

#if !UNITY_EDITOR
            _allowKeyboardInput = false;
#endif
        }

        private void OnDestroy()
        {
            SettingsModel.Instance.JoystickSize.UnregisterCallback(UpdateJoystickSize);
            SettingsModel.Instance.JoystickSetting.UnregisterCallback(OnUpdateJoystickSettings);
        }

        private void Update()
        {
            if (!_allowKeyboardInput)
                return;

            var keyboardInput = Vector2.zero;

            if (UnityEngine.Input.GetKey(KeyCode.A))
                keyboardInput.x -= 1;
            if (UnityEngine.Input.GetKey(KeyCode.D))
                keyboardInput.x += 1;
            if (UnityEngine.Input.GetKey(KeyCode.W))
                keyboardInput.y += 1;
            if (UnityEngine.Input.GetKey(KeyCode.S))
                keyboardInput.y -= 1;

            InputModel.Instance.UpdateInput(keyboardInput);
        }

        private void LateUpdate()
        {
            if (!_isTouching || SettingsModel.Instance.JoystickSetting.Value != JoystickSetting.Following)
                return;

            var inputDelta = _lastPointerPosition - _joystickDisplayContainer.anchoredPosition;
            if (inputDelta.magnitude > _defaultJoystickRange * SettingsModel.Instance.JoystickSize.Value)
                _joystickDisplayContainer.anchoredPosition += _defaultJoystickDragSpeed * Time.deltaTime * inputDelta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_hideJoystickOnRelease)
                _joystickDisplayContainer.gameObject.SetActive(false);

            _joystick.anchoredPosition = Vector2.zero;
            InputModel.Instance.UpdateInput(Vector2.zero);
            _isTouching = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _joystickDisplayContainer.gameObject.SetActive(true);

            if (SettingsModel.Instance.JoystickSetting.Value != JoystickSetting.Static)
                _joystickDisplayContainer.anchoredPosition = GetLocalPosition(eventData.position);

            _isTouching = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _lastPointerPosition = GetLocalPosition(eventData.position);
            var input = _lastPointerPosition - _joystickDisplayContainer.anchoredPosition;
            var maxInput = SettingsModel.Instance.JoystickSize.Value * _defaultJoystickRange;
            InputModel.Instance.UpdateInput(input / maxInput);

            _joystick.anchoredPosition = input.magnitude > maxInput
                ? input.normalized * maxInput
                : input;
        }

        private void UpdateJoystickSize(float joystickSize)
        {
            var size = joystickSize * _defaultJoystickSize;
            _joystickDisplayContainer.sizeDelta = new Vector2(size, size);
            ResetJoystickPosition(joystickSize);
        }

        private void OnUpdateJoystickSettings(JoystickSetting joystickSetting)
        {
            var setting = SettingsModel.Instance.JoystickSetting.Value;

            if (setting == _lastJoystickSetting)
                return;

            _lastJoystickSetting = setting;

            if (setting == JoystickSetting.Static)
                ResetJoystickPosition(SettingsModel.Instance.JoystickSize.Value);
        }

        private void ResetJoystickPosition(float joystickSize)
        {
            var offset = joystickSize * _defaultJoystickOffset;
            _joystickDisplayContainer.anchoredPosition = new Vector2(offset, offset);
        }

        private Vector2 GetLocalPosition(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickContainer, position,
                CameraView.Instance.Camera, out var localPoint);

            return localPoint;
        }
    }
}
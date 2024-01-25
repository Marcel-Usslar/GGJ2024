using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace CustomButton
{
    [DisallowMultipleComponent]
    public class ReactiveButton : MonoBehaviour, IButton, IPointerDownHandler, IPointerUpHandler, IDragHandler,
        IBeginDragHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private List<ButtonDecoratorComponent> _decorators;

        private bool _isInteractable = true;

        private bool _buttonPressed;
        private bool _wasCancelled;
        private bool _dragStarted;

        private CallbackHandler<bool> _interactabilityChangedHandler;
        private CallbackHandler<bool> _onButtonClickHandler;
        private CallbackHandler<bool> _onPointerDownHandler;
        private CallbackHandler<bool> _onPointerUpHandler;

        public bool Interactable
        {
            get => _isInteractable;
            set
            {
                var previous = _isInteractable;
                _isInteractable = value;

                if (previous == _isInteractable)
                    return;

                _interactabilityChangedHandler?.Trigger(_isInteractable);
                _decorators.ForEach(component => component.OnInteractabilityChanged(_isInteractable));
            }
        }

        public void RegisterClickHandler(Action<bool> handler)
        {
            _onButtonClickHandler ??= new();
            _onButtonClickHandler.RegisterCallback(handler);
        }

        public void RegisterPointerUpHandler(Action<bool> handler)
        {
            _onPointerUpHandler ??= new();
            _onPointerUpHandler.RegisterCallback(handler);
        }

        public void RegisterPointerDownHandler(Action<bool> handler)
        {
            _onPointerDownHandler ??= new();
            _onPointerDownHandler.RegisterCallback(handler);
        }

        public void RegisterInteractabilityChanged(Action<bool> handler)
        {
            _interactabilityChangedHandler ??= new();
            _interactabilityChangedHandler.RegisterCallback(handler);
        }

        public void UnregisterClickHandler(Action<bool> handler)
        {
            _onButtonClickHandler?.UnregisterCallback(handler);
        }

        public void UnregisterPointerUpHandler(Action<bool> handler)
        {
            _onPointerUpHandler?.UnregisterCallback(handler);
        }

        public void UnregisterPointerDownHandler(Action<bool> handler)
        {
            _onPointerDownHandler?.UnregisterCallback(handler);
        }

        public void UnregisterInteractabilityChanged(Action<bool> handler)
        {
            _interactabilityChangedHandler?.UnregisterCallback(handler);
        }

        #region Unity Event Handling

        //Drag threshold can be configured in the Unity EventSystem Component
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStarted = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonPressed = true;
            _wasCancelled = false;
            _dragStarted = false;

            _onPointerDownHandler?.Trigger(_isInteractable);
            _decorators.ForEach(component => component.OnPointerDown(_isInteractable));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonPressed = false;

            _onPointerUpHandler?.Trigger(_isInteractable);
            _decorators.ForEach(component => component.OnPointerUp(_isInteractable));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // checking for touchCount > 1 since it is updated after the method is called
            if (!_dragStarted && !_wasCancelled && !(Input.touchCount > 1))
            {
                _onButtonClickHandler?.Trigger(_isInteractable);
                _decorators.ForEach(component => component.OnClick(_isInteractable));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CancelButtonClick();
        }

        public void OnDrag(PointerEventData eventData)
        {
            //needs to be implemented for OnBeginDrag to be called properly
        }

        private void OnDisable()
        {
            CancelButtonClick();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                CancelButtonClick();
            }
        }

        private void CancelButtonClick()
        {
            _wasCancelled = true;

            if (!_buttonPressed)
                return;

            _buttonPressed = false;
            OnPointerUp(null);
        }

        #endregion

        public void EditorUpdateDecorators(List<ButtonDecoratorComponent> decorators)
        {
            _decorators = decorators;
        }
    }
}
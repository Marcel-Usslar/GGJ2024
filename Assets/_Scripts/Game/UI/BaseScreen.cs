using UnityEngine;
using Utility;

namespace Game.UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private PanelView _panelView;

        protected abstract ReactiveProperty<bool> Visibility { get; }

        private void Start()
        {
            Visibility.RegisterCallback(UpdateVisibility);
            if (_panelView.CloseButton != null)
                _panelView.CloseButton.RegisterClickHandler(_ => Hide());

            OnStart();
        }

        private void OnDestroy()
        {
            Visibility.UnregisterCallback(UpdateVisibility);
            if (_panelView.CloseButton != null)
                _panelView.CloseButton.UnregisterClickHandler(_ => Hide());

            OnFinalize();
        }

        private void UpdateVisibility(bool visible)
        {
            _panelView.Visible = visible;
            OnVisibilityChanged(visible);
        }

        protected virtual void OnStart() { }
        protected virtual void OnFinalize() { }
        protected virtual void OnVisibilityChanged(bool visible) { }

        protected void Show()
        {
            Visibility.Value = true;
        }

        protected void Hide()
        {
            Visibility.Value = false;
        }
    }
}
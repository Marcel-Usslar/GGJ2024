using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.UI
{
    public abstract class BaseSingletonScreen<T> : SingletonMonoBehaviour<T> where T : BaseSingletonScreen<T>
    {
        [SerializeField] private PanelView _panelView;

        protected abstract ReactiveProperty<bool> Visibility { get; }

        protected sealed override void OnInitialize()
        { }

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
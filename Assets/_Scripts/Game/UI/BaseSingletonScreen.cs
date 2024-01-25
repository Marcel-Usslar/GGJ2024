using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.UI
{
    public abstract class BaseSingletonScreen<T> : SingletonMonoBehaviour<T> where T : BaseSingletonScreen<T>
    {
        [SerializeField] private GameObject _menuRoot;

        protected abstract ReactiveProperty<bool> Visibility { get; }

        protected sealed override void OnInitialize()
        { }

        private void Start()
        {
            Visibility.RegisterCallback(UpdateVisibility);

            OnStart();
        }

        private void OnDestroy()
        {
            Visibility.UnregisterCallback(UpdateVisibility);

            OnFinalize();
        }

        private void UpdateVisibility(bool visible)
        {
            _menuRoot.SetActive(visible);
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
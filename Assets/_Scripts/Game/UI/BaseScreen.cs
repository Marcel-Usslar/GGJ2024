using UnityEngine;
using Utility;

namespace Game.UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _menuRoot;

        protected abstract ReactiveProperty<bool> Visibility { get; }

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
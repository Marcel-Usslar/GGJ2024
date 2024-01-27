using System.Collections.Generic;
using UnityEngine;
using Utility.Singletons;

namespace Utility.Pools
{
    public class PooledView<T> : SingletonModel<PooledView<T>> where T : Component
    {
        private bool _isSetup;
        private IPooledViewConfig<T> _config;
        private Stack<T> _inactiveItems;
        private GameObject _container;

        public void TrySetupPool(IPooledViewConfig<T> config)
        {
            if (_isSetup)
                return;

            _isSetup = true;
            _config = config;
            _inactiveItems = new Stack<T>();
            _container = new GameObject($"{typeof(T).Name}Pool");

            Object.DontDestroyOnLoad(_container);
        }

        public T Spawn(Transform parent)
        {
            var item = _inactiveItems.Count == 0
                ? Object.Instantiate(_config.Prefab, Vector3.zero, Quaternion.identity, _container.transform)
                : _inactiveItems.Pop();
            item.transform.SetParent(parent, false);
            item.gameObject.SetActive(true);

            return item;
        }

        public void Despawn(T instance)
        {
            _inactiveItems.Push(instance);
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_container.transform, false);
        }
    }
}
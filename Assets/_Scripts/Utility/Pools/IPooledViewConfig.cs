using UnityEngine;

namespace Utility.Pools
{
    public interface IPooledViewConfig<T> where T : Component
    {
        public T Prefab { get; }
    }
}
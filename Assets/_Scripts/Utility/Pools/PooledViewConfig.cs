using UnityEngine;

namespace Utility.Pools
{
    public abstract class PooledViewConfig<T> : ScriptableObject where T : Component
    {
        public abstract T Prefab { get; }
    }
}
using System;

namespace Utility
{
    public interface ICallbackHandler<out T>
    {
        public void RegisterCallback(Action<T> callback);
        public void UnregisterCallback(Action<T> callback);
    }
}
using System;
using System.Collections.Generic;

namespace Utility
{
    public class CallbackHandler
    {
        private readonly List<Action> _callbacks = new();

        public void RegisterCallback(Action callback)
        {
            _callbacks.Add(callback);
        }

        public void UnregisterCallback(Action callback)
        {
            _callbacks.Remove(callback);
        }

        public void ClearCallbacks()
        {
            _callbacks.Clear();
        }

        public void Trigger()
        {
            if (_callbacks.Count == 0)
                return;

            foreach (var callback in _callbacks)
                callback.Invoke();
        }
    }

    public class CallbackHandler<T> : ICallbackHandler<T>
    {
        private readonly List<Action<T>> _callbacks = new();

        public void RegisterCallback(Action<T> callback)
        {
            _callbacks.Add(callback);
        }

        public void UnregisterCallback(Action<T> callback)
        {
            _callbacks.Remove(callback);
        }

        public void ClearCallbacks()
        {
            _callbacks.Clear();
        }

        public void Trigger(T value)
        {
            if (_callbacks.Count == 0)
                return;

            foreach (var callback in _callbacks)
                callback.Invoke(value);
        }
    }
}
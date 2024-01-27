using System;
using System.Collections.Generic;

namespace Utility
{
    public class ReactiveProperty<T> : ICallbackHandler<T>
    {
        private readonly List<Action<T>> _callbacks = new();

        private T _value;

        public T Value
        {
            get => _value;
            set => Trigger(value);
        }

        public ReactiveProperty(T defaultValue = default)
        {
            _value = defaultValue;
        }

        public void RegisterCallback(Action<T> callback)
        {
            _callbacks.Add(callback);
            callback.Invoke(_value);
        }

        public void UnregisterCallback(Action<T> callback)
        {
            _callbacks.Remove(callback);
        }

        public void ClearCallbacks()
        {
            _callbacks.Clear();
        }

        private void Trigger(T value)
        {
            if (_value.Equals(value))
                return;

            _value = value;

            if (_callbacks.Count == 0)
                return;

            foreach (var callback in _callbacks)
                callback.Invoke(value);
        }
    }
}
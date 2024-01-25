using System;
using Game.Utility;
using UnityEngine;
using Utility;

namespace Utility.CollisionDetection
{
    public class CollisionDetectionModel<T> : ICollisionDetectionModel<T>, ICollisionUpdateModel where T : Component
    {
        private CallbackHandler<T> _onStayHandler;
        private CallbackHandler<T> _onEnterHandler;
        private CallbackHandler<T> _onExitHandler;

        public void RegisterStayHandler(Action<T> handler)
        {
            _onStayHandler ??= new CallbackHandler<T>();
            _onStayHandler.RegisterCallback(handler);
        }

        public void RegisterEnterHandler(Action<T> handler)
        {
            _onEnterHandler ??= new CallbackHandler<T>();
            _onEnterHandler.RegisterCallback(handler);
        }

        public void RegisterExitHandler(Action<T> handler)
        {
            _onExitHandler ??= new CallbackHandler<T>();
            _onExitHandler.RegisterCallback(handler);
        }

        public void UnregisterStayHandler(Action<T> handler)
        {
            _onStayHandler?.UnregisterCallback(handler);
        }

        public void UnregisterEnterHandler(Action<T> handler)
        {
            _onEnterHandler?.UnregisterCallback(handler);
        }

        public void UnregisterExitHandler(Action<T> handler)
        {
            _onExitHandler?.UnregisterCallback(handler);
        }

        public void TryTriggerStay(GameObject other)
        {
            if (_onStayHandler == null)
                return;

            var component = other.gameObject.GetComponent<T>();
            if (component == null)
                return;

            _onStayHandler.Trigger(component);
        }

        public void TryTriggerEnter(GameObject other)
        {
            if (_onEnterHandler == null)
                return;

            var component = other.gameObject.GetComponent<T>();
            if (component == null)
                return;

            _onEnterHandler.Trigger(component);
        }

        public void TryTriggerExit(GameObject other)
        {
            if (_onExitHandler == null)
                return;

            var component = other.gameObject.GetComponent<T>();
            if (component == null)
                return;

            _onExitHandler.Trigger(component);
        }
    }
}
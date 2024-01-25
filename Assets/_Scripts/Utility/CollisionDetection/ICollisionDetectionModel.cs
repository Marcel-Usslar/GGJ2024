using System;

namespace Utility.CollisionDetection
{
    public interface ICollisionDetectionModel<out T> where T : class
    {
        void RegisterStayHandler(Action<T> handler);
        void RegisterEnterHandler(Action<T> handler);
        void RegisterExitHandler(Action<T> handler);

        void UnregisterStayHandler(Action<T> handler);
        void UnregisterEnterHandler(Action<T> handler);
        void UnregisterExitHandler(Action<T> handler);
    }
}
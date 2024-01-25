using System;

namespace CustomButton
{
    public interface IReadOnlyButton
    {
        void RegisterClickHandler(Action<bool> handler);
        void RegisterPointerUpHandler(Action<bool> handler);
        void RegisterPointerDownHandler(Action<bool> handler);
        void RegisterInteractabilityChanged(Action<bool> handler);

        void UnregisterClickHandler(Action<bool> handler);
        void UnregisterPointerUpHandler(Action<bool> handler);
        void UnregisterPointerDownHandler(Action<bool> handler);
        void UnregisterInteractabilityChanged(Action<bool> handler);
    }
}
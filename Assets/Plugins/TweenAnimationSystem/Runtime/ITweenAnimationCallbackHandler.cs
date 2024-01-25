using System;

namespace Plugins.TweenAnimationSystem
{
    public interface ITweenAnimationCallbackHandler
    {
        void RegisterAnimationFinishedHandler(Action handler);
        void UnregisterAnimationFinishedHandler(Action handler);
    }
}
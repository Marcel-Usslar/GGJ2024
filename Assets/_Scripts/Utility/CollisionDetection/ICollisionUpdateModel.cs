using UnityEngine;

namespace Game.Utility
{
    public interface ICollisionUpdateModel
    {
        void TryTriggerStay(GameObject other);
        void TryTriggerEnter(GameObject other);
        void TryTriggerExit(GameObject other);
    }
}
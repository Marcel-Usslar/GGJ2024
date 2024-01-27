using UnityEngine;

namespace Game.Interaction
{
    public abstract class InteractableItemView : MonoBehaviour
    {
        public abstract string Id { get; }
        public abstract Vector2 Position { get; }
        public abstract void Interact();
    }
}
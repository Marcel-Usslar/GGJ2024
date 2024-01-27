using UnityEngine;

namespace Game.Interaction
{
    public abstract class InteractableItemView : MonoBehaviour
    {
        public abstract string Id { get; }

        public Vector2 Position => transform.position;

        public abstract void Interact();
    }
}
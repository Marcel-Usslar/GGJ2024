using UnityEngine;

namespace Game.Interaction
{
    public class DebugInteractableItemView : InteractableItemView
    {
        [SerializeField] private string _id;

        public override string Id => _id;

        public override Vector2 Position => transform.position;

        public override void Interact()
        {
            Debug.LogError($"Interacting with {_id}");
        }
    }
}
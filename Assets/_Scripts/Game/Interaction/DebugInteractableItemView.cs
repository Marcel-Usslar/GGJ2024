using UnityEngine;

namespace Game.Interaction
{
    public class DebugInteractableItemView : InteractableItemView
    {
        [SerializeField] private string _id;

        public override void Interact()
        {
            Debug.LogError($"Interacting with {_id}");
        }
    }
}
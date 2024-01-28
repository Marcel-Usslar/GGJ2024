using Game.Interaction;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryItemInteractableItemView : InteractableItemView
    {
        [SerializeField] private InventoryItemView _itemView;

        public override void Interact()
        {
            InventoryModel.Instance.AddInteractableItem(_itemView.Item);
        }
    }
}
using System.Collections.Generic;
using Utility;
using Utility.Singletons;

namespace Game.Inventory
{
    public class InventoryModel : SingletonModel<InventoryModel>
    {
        private readonly List<InventoryItem> _items = new();
        private readonly List<InventoryItem> _usedItems = new();

        public CallbackHandler<InventoryItem> OnItemReceived { get; } = new();
        public CallbackHandler<InventoryItem> OnItemUsed { get; } = new();
        public CallbackHandler<InventoryItem> OnItemRemoved { get; } = new();

        public bool HasInventoryItem(InventoryItem item)
        {
            return _items.Contains(item) || _usedItems.Contains(item);
        }

        public void AddInteractableItem(InventoryItem item)
        {
            if (HasInventoryItem(item))
                return;

            _items.Add(item);
        }

        public void RemoveInteractableItem(InventoryItem item)
        {
            _items.Remove(item);
        }

        public void UseInteractableItem(InventoryItem item)
        {
            if (!_items.Contains(item))
                return;

            _items.Remove(item);
            _usedItems.Add(item);
        }
    }
}
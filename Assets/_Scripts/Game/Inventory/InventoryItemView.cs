using Game.GameState;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private GameObject _itemVisuals;

        public InventoryItem Item => _item;

        private void Update()
        {
            if (GameStateModel.Instance.IsPaused.Value)
                return;

            _itemVisuals.SetActive(!InventoryModel.Instance.HasInventoryItem(_item));
        }
    }
}
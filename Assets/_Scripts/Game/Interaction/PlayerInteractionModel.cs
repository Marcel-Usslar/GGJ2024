using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Singletons;

namespace Game.Interaction
{
    public class PlayerInteractionModel : SingletonModel<PlayerInteractionModel>
    {
        private readonly List<InteractableItemView> _interactableItems = new();

        public InteractableItemView CurrentInteractable { get; private set; }
        public IEnumerable<InteractableItemView> InteractableItems => _interactableItems;

        public void CalculateCurrentInteractable(Vector2 position)
        {
            if (_interactableItems.Count == 0)
            {
                CurrentInteractable = null;
                return;
            }

            CurrentInteractable = PlayerInteractionModel.Instance.InteractableItems
                .OrderByDescending(view => Vector2.Distance(position, view.Position))
                .First();
        }

        public void AddInteractableItem(InteractableItemView itemView)
        {
            if (_interactableItems.Contains(itemView))
                return;

            _interactableItems.Add(itemView);
        }

        public void RemoveInteractableItem(InteractableItemView view)
        {
            _interactableItems.Remove(view);
        }
    }
}
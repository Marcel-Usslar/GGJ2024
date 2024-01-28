using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Singletons;

namespace Game.Interaction
{
    public class PlayerInteractionModel : SingletonModel<PlayerInteractionModel>
    {
        private readonly List<InteractableItemView> _interactableItems = new();
        private Vector2 _position;

        public InteractableItemView CurrentInteractable { get; private set; }

        public void CalculateCurrentInteractable(Vector2 position)
        {
            _position = position;
            if (_interactableItems.Count == 0)
            {
                CurrentInteractable = null;
                return;
            }

            CurrentInteractable = _interactableItems
                .OrderBy(view => Vector2.Distance(_position, view.Position))
                .First();
        }

        public void AddInteractableItem(InteractableItemView itemView)
        {
            if (_interactableItems.Contains(itemView))
                return;

            _interactableItems.Add(itemView);
            CalculateCurrentInteractable(_position);
        }

        public void RemoveInteractableItem(InteractableItemView view)
        {
            _interactableItems.Remove(view);
            CalculateCurrentInteractable(_position);
        }
    }
}
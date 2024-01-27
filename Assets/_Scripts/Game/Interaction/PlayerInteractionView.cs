using System.Collections.Generic;
using System.Linq;
using Game.GameState;
using Game.Input;
using Game.Movement;
using UnityEngine;
using Utility.CollisionDetection;

namespace Game.Interaction
{
    public class PlayerInteractionView : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private CollisionDetectionView _interactionDetection;

        private readonly List<InteractableItemView> _interactableItems = new();
        private ICollisionDetectionModel<InteractableItemView> _model;

        public InteractableItemView CurrentInteractable { get; private set; }

        private void Start()
        {
            _model = _interactionDetection.Setup<InteractableItemView>();
            _model.RegisterEnterHandler(AddInteractableItem);
            _model.RegisterExitHandler(view => _interactableItems.Remove(view));
            _playerMovement.Position.RegisterCallback(CalculateCurrentInteractable);
            InputModel.Instance.OnInteract.RegisterCallback(TryInteraction);
        }

        private void OnDestroy()
        {
            _model.UnregisterEnterHandler(AddInteractableItem);
            _model.UnregisterExitHandler(view => _interactableItems.Remove(view));
            _playerMovement.Position.UnregisterCallback(CalculateCurrentInteractable);
            InputModel.Instance.OnInteract.UnregisterCallback(TryInteraction);
        }

        private void AddInteractableItem(InteractableItemView itemView)
        {
            if (_interactableItems.Contains(itemView))
                return;

            _interactableItems.Add(itemView);
        }

        private void CalculateCurrentInteractable(Vector2 position)
        {
            if (_interactableItems.Count == 0)
            {
                CurrentInteractable = null;
                return;
            }

            CurrentInteractable = _interactableItems
                .OrderByDescending(view => Vector2.Distance(position, view.Position))
                .First();
        }

        private void TryInteraction()
        {
            if (CurrentInteractable == null || GameStateModel.Instance.IsPaused.Value)
                return;

            CurrentInteractable.Interact();
        }
    }
}
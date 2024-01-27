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

        private ICollisionDetectionModel<InteractableItemView> _model;


        private void Start()
        {
            _model = _interactionDetection.Setup<InteractableItemView>();
            _model.RegisterEnterHandler(PlayerInteractionModel.Instance.AddInteractableItem);
            _model.RegisterExitHandler(PlayerInteractionModel.Instance.RemoveInteractableItem);
            _playerMovement.Position.RegisterCallback(PlayerInteractionModel.Instance.CalculateCurrentInteractable);
            InputModel.Instance.OnInteract.RegisterCallback(TryInteraction);
        }

        private void OnDestroy()
        {
            _model.UnregisterEnterHandler(PlayerInteractionModel.Instance.AddInteractableItem);
            _model.UnregisterExitHandler(PlayerInteractionModel.Instance.RemoveInteractableItem);
            _playerMovement.Position.UnregisterCallback(PlayerInteractionModel.Instance.CalculateCurrentInteractable);
            InputModel.Instance.OnInteract.UnregisterCallback(TryInteraction);
        }

        private void TryInteraction()
        {
            if (PlayerInteractionModel.Instance.CurrentInteractable == null || GameStateModel.Instance.IsPaused.Value)
                return;

            PlayerInteractionModel.Instance.CurrentInteractable.Interact();
        }
    }
}
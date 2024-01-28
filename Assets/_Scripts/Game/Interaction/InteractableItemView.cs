using CustomButton;
using UnityEngine;

namespace Game.Interaction
{
    public abstract class InteractableItemView : MonoBehaviour
    {
        [SerializeField] private GameObject _interactCanvas;
        [SerializeField] private ReactiveButton _interactButton;

        public Vector2 Position => transform.position;

        public abstract void Interact();
        protected virtual void OnStart() { }
        protected virtual void OnFinalize() { }

        private void Start()
        {
            _interactButton.RegisterClickHandler(_ => Interact());

            OnStart();
        }

        private void OnDestroy()
        {
            _interactButton.UnregisterClickHandler(_ => Interact());

            OnFinalize();
        }

        private void Update()
        {
            _interactCanvas.SetActive(PlayerInteractionModel.Instance.CurrentInteractable == this);
        }
    }
}
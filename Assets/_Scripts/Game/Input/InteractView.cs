using CustomButton;
using Game.Interaction;
using UnityEngine;

namespace Game.Input
{
    public class InteractView : MonoBehaviour
    {
        [SerializeField] private bool _allowKeyboardInput;
        [SerializeField] private ReactiveButton _button;

        private void Start()
        {
            _button.RegisterClickHandler(_ => InputModel.Instance.TriggerInteraction());

#if !UNITY_EDITOR
            _allowKeyboardInput = false;
#endif
        }

        private void OnDestroy()
        {
            _button.UnregisterClickHandler(_ => InputModel.Instance.TriggerInteraction());
        }

        private void Update()
        {
            _button.gameObject.SetActive(PlayerInteractionModel.Instance.CurrentInteractable != null);

            if (_allowKeyboardInput && UnityEngine.Input.GetKeyDown(KeyCode.E))
                InputModel.Instance.TriggerInteraction();
        }
    }
}
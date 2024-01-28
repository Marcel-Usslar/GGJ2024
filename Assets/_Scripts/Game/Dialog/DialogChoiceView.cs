using CustomButton;
using TMPro;
using UnityEngine;

namespace Game.Dialog
{
    public class DialogChoiceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private ReactiveButton _button;

        private DialogActionDto _dto;

        public void Setup(DialogActionDto dto)
        {
            _dto = dto;
            _text.text = dto.Text;
            _button.RegisterClickHandler(_ => ExecuteCallback());
        }

        public void OnReset()
        {
            _dto = default;
            _button.UnregisterClickHandler(_ => ExecuteCallback());
        }

        private void ExecuteCallback()
        {
            _dto.Callback.Invoke();
            DialogSystem.Instance.Continue();
        }
    }
}
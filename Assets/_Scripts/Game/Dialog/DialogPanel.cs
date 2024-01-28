using CustomButton;
using Game.UI;
using Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Game.Dialog
{
    public class DialogPanel : BaseScreen
    {
        [SerializeField] private ReactiveButton _continueButton;
        [SerializeField] private GameObject _continueIcon;
        [Space]
        [SerializeField] private Image _speakerIcon;
        [SerializeField] private TextMeshProUGUI _speakerName;
        [SerializeField] private TextMeshProUGUI _dialogText;

        protected override ReactiveProperty<bool> Visibility { get; } = new();

        protected override void OnStart()
        {
            DialogSystem.Instance.OnDialog.RegisterCallback(ShowDialog);
            DialogSystem.Instance.OnDialogCompleted.RegisterCallback(Hide);
            _continueButton.RegisterClickHandler(_ => DialogSystem.Instance.Continue());
        }

        protected override void OnFinalize()
        {
            DialogSystem.Instance.OnDialog.UnregisterCallback(ShowDialog);
            DialogSystem.Instance.OnDialogCompleted.UnregisterCallback(Hide);
            _continueButton.UnregisterClickHandler(_ => DialogSystem.Instance.Continue());
        }

        private void ShowDialog(DialogDto dto)
        {
            var dialogConfig = ConfigSingletonInstaller.Instance.DialogConfig;
            var speakerConfig = ConfigSingletonInstaller.Instance.SpeakerConfig;

            var speakerType = dialogConfig.GetDialogSpeaker(dto.DialogId, dto.Index);
            _speakerIcon.sprite = speakerConfig.GetSpeakerIcon(speakerType);
            _speakerName.text = speakerConfig.GetSpeakerName(speakerType);
            _dialogText.text = dialogConfig.GetDialogText(dto.DialogId, dto.Index);
            _continueIcon.SetActive(dialogConfig.HasNextDialog(dto.DialogId, dto.Index));

            Show();
        }
    }
}
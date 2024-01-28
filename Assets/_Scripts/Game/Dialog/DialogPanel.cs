using System.Collections.Generic;
using CustomButton;
using Game.UI;
using Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Utility.Pools;

namespace Game.Dialog
{
    public class DialogPanel : BaseScreen
    {
        [SerializeField] private ReactiveButton _continueButton;
        [SerializeField] private GameObject _continueIcon;
        [Space]
        [SerializeField] private GameObject _dialogContent;
        [SerializeField] private Transform _choiceContent;
        [Space]
        [SerializeField] private Image _speakerIcon;
        [SerializeField] private TextMeshProUGUI _speakerName;
        [SerializeField] private TextMeshProUGUI _dialogText;

        private readonly List<DialogChoiceView> _dialogChoiceViews = new();

        protected override ReactiveProperty<bool> Visibility { get; } = new();

        protected override void OnStart()
        {
            PooledView<DialogChoiceView>.Instance
                .TrySetupPool(ConfigSingletonInstaller.Instance.DialogConfig);

            DialogSystem.Instance.OnDialog.RegisterCallback(ShowDialog);
            DialogSystem.Instance.OnChoiceDialog.RegisterCallback(ShowDialog);
            DialogSystem.Instance.OnDialogCompleted.RegisterCallback(Hide);
            _continueButton.RegisterClickHandler(_ => DialogSystem.Instance.Continue());
        }

        protected override void OnFinalize()
        {
            DialogSystem.Instance.OnDialog.UnregisterCallback(ShowDialog);
            DialogSystem.Instance.OnChoiceDialog.UnregisterCallback(ShowDialog);
            DialogSystem.Instance.OnDialogCompleted.UnregisterCallback(Hide);
            _continueButton.UnregisterClickHandler(_ => DialogSystem.Instance.Continue());
        }

        private void ShowDialog(DialogPageDto dto)
        {
            _dialogChoiceViews.ForEach(view => view.OnReset());
            _dialogChoiceViews.ForEach(PooledView<DialogChoiceView>.Instance.Despawn);
            _dialogChoiceViews.Clear();

            var dialogConfig = ConfigSingletonInstaller.Instance.DialogConfig;
            var speakerConfig = ConfigSingletonInstaller.Instance.SpeakerConfig;

            var speakerType = dialogConfig.GetDialogSpeaker(dto.Id, dto.Index);
            _speakerIcon.sprite = speakerConfig.GetSpeakerIcon(speakerType);
            _speakerName.text = speakerConfig.GetSpeakerName(speakerType);
            _dialogText.text = dialogConfig.GetDialogText(dto.Id, dto.Index);
            _continueIcon.SetActive(dialogConfig.HasNextDialog(dto.Id, dto.Index));
            UpdateContent(true);

            Show();
        }

        private void ShowDialog(DialogChoiceDto dto)
        {
            var dialogConfig = ConfigSingletonInstaller.Instance.DialogConfig;
            var speakerConfig = ConfigSingletonInstaller.Instance.SpeakerConfig;

            var speakerType = dialogConfig.GetDialogSpeaker(dto.Page.Id, dto.Page.Index);
            _speakerIcon.sprite = speakerConfig.GetSpeakerIcon(speakerType);
            _speakerName.text = speakerConfig.GetSpeakerName(speakerType);
            UpdateContent(false);

            dto.ActionDtos.ForEach(SpawnQuestEntry);

            Show();
        }

        private void SpawnQuestEntry(DialogActionDto dto)
        {
            var entry = PooledView<DialogChoiceView>.Instance.Spawn(_choiceContent);
            _dialogChoiceViews.Add(entry);
            entry.Setup(dto);
        }

        private void UpdateContent(bool showDialog)
        {
            _dialogContent.SetActive(showDialog);
            _choiceContent.gameObject.SetActive(!showDialog);
            _continueButton.gameObject.SetActive(showDialog);

        }
    }
}
using System.Collections.Generic;
using Game.GameState;
using Game.Utility;
using Utility;
using Utility.Singletons;

namespace Game.Dialog
{
    public class DialogSystem : SingletonModel<DialogSystem>
    {
        public CallbackHandler<DialogPageDto> OnDialog { get; } = new();
        public CallbackHandler<DialogChoiceDto> OnChoiceDialog { get; } = new();
        public CallbackHandler OnDialogCompleted { get; } = new();

        private DialogId _dialogId;
        private int _dialogIndex;
        private List<DialogActionDto> _actionDtos;

        public void TriggerDialog(DialogId id)
        {
            _dialogId = id;
            _dialogIndex = 0;
            _actionDtos = new List<DialogActionDto>();

            OnDialog.Trigger(new DialogPageDto(_dialogId, _dialogIndex));
            GameStateModel.Instance.IsPaused.Value = true;
        }

        public void RegisterDialogActions(List<DialogActionDto> actionDtos)
        {
            _actionDtos = actionDtos;
        }

        public void Continue()
        {
            var dialogConfig = ConfigSingletonInstaller.Instance.DialogConfig;
            if (!dialogConfig.HasNextDialog(_dialogId, _dialogIndex))
            {
                TryTriggerChoice();
                return;
            }

            _dialogIndex++;
            OnDialog.Trigger(new DialogPageDto(_dialogId, _dialogIndex));
        }

        private void TryTriggerChoice()
        {
            if (_actionDtos.Count > 0)
            {
                var choiceDto = new DialogChoiceDto(new DialogPageDto(_dialogId, _dialogIndex), _actionDtos);
                OnChoiceDialog.Trigger(choiceDto);
                _actionDtos = new List<DialogActionDto>();
                return;
            }

            OnDialogCompleted.Trigger();
            GameStateModel.Instance.IsPaused.Value = false;
        }
    }
}
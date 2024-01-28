using Game.Speaker;
using Game.Utility;
using Utility;
using Utility.Singletons;

namespace Game.Dialog
{
    public class DialogSystem : SingletonModel<DialogSystem>
    {
        public CallbackHandler<DialogDto> OnDialog { get; } = new();
        public CallbackHandler OnDialogCompleted { get; } = new();

        private int _dialogId;
        private int _dialogIndex;

        public void TriggerDialog(SpeakerType speaker, int state)
        {
            var dialogConfig = ConfigSingletonInstaller.Instance.CharacterDialogConfig;

            _dialogId = dialogConfig.GetDialogId(speaker, state);
            _dialogIndex = 0;

            OnDialog.Trigger(new DialogDto(_dialogId, _dialogIndex));
        }

        public void Continue()
        {
            var dialogConfig = ConfigSingletonInstaller.Instance.DialogConfig;
            if (!dialogConfig.HasNextDialog(_dialogId, _dialogIndex))
            {
                OnDialogCompleted.Trigger();
                return;
            }

            _dialogIndex++;
            OnDialog.Trigger(new DialogDto(_dialogId, _dialogIndex));
        }
    }
}
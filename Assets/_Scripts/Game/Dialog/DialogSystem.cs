using Game.GameState;
using Game.Speaker;
using Game.Utility;
using Utility;
using Utility.Singletons;

namespace Game.Dialog
{
    public class DialogSystem : SingletonModel<DialogSystem>
    {
        public CallbackHandler<DialogPageDto> OnDialog { get; } = new();
        public CallbackHandler OnDialogCompleted { get; } = new();

        private DialogId _dialogId;
        private int _dialogIndex;

        public void TriggerDialog(DialogId id)
        {
            _dialogId = id;
            _dialogIndex = 0;

            OnDialog.Trigger(new DialogPageDto(_dialogId, _dialogIndex));
            GameStateModel.Instance.IsPaused.Value = true;
        }

        public void Continue()
        {
            var dialogConfig = ConfigSingletonInstaller.Instance.DialogConfig;
            if (!dialogConfig.HasNextDialog(_dialogId, _dialogIndex))
            {
                OnDialogCompleted.Trigger();
                GameStateModel.Instance.IsPaused.Value = false;
                return;
            }

            _dialogIndex++;
            OnDialog.Trigger(new DialogPageDto(_dialogId, _dialogIndex));
        }
    }
}
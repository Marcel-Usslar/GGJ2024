using Game.Utility;
using UnityEngine;
using Utility.Singletons;

namespace Game.Dialog
{
    public class DialogSystem : SingletonMonoBehaviour<DialogSystem>
    {
        override protected void OnInitialize()
        {
            ConfigSingletonInstaller.Instance.DialogConfig.ResetDialogs();
        }
        public void TriggerDialog(int dialogId)
        {
            var message = ConfigSingletonInstaller.Instance.DialogConfig.GetNextDialog(dialogId);
            Debug.LogError(message);
        }
    }
}
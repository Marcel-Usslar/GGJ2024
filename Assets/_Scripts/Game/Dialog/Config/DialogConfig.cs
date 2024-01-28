using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Dialog.Config
{
    public class DialogConfig : ScriptableObject
    {
        [Serializable]
        private class DialogMapping
        {
            public int DialogID;
            public List<DialogSpeakerMapping> DialogSpeakerData;
        }
        [Serializable]
        private class DialogSpeakerMapping
        {
            public string Speaker;
            [TextArea] public string Text;
        }

        [SerializeField] private List<DialogMapping> _dialogData;

        public bool HasNextDialog(int dialogId, int index)
        {
            return _dialogData.First(mapping => mapping.DialogID == dialogId).DialogSpeakerData.Count > index + 1;
        }

        public string GetDialogSpeaker(int dialogId, int index)
        {
            return GetDialogSpeakerMapping(dialogId, index).Speaker;
        }

        public string GetDialogText(int dialogId, int index)
        {
            return GetDialogSpeakerMapping(dialogId, index).Text;
        }

        private DialogSpeakerMapping GetDialogSpeakerMapping(int dialogId, int index)
        {
            return _dialogData.First(mapping => mapping.DialogID == dialogId).DialogSpeakerData[index];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Speaker;
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
            public SpeakerType Speaker;
            [TextArea] public string Text;
        }

        [SerializeField] private List<DialogMapping> _dialogData;

        public bool HasNextDialog(int dialogId, int index)
        {
            return _dialogData.First(mapping => mapping.DialogID == dialogId).DialogSpeakerData.Count > index + 1;
        }

        public SpeakerType GetDialogSpeaker(int dialogId, int index)
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
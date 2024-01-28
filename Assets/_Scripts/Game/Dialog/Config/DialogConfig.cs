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
            //Editor only for better readability
            [HideInInspector] public string Name;
            public DialogId Id;
            public List<DialogSpeakerMapping> DialogSpeakerData;
        }
        [Serializable]
        private class DialogSpeakerMapping
        {
            public SpeakerType Speaker;
            [TextArea] public string Text;
        }

        [SerializeField] private List<DialogMapping> _dialogData;

        public bool HasNextDialog(DialogId dialogId, int index)
        {
            return _dialogData.First(mapping => mapping.Id == dialogId).DialogSpeakerData.Count > index + 1;
        }

        public SpeakerType GetDialogSpeaker(DialogId dialogId, int index)
        {
            return GetDialogSpeakerMapping(dialogId, index).Speaker;
        }

        public string GetDialogText(DialogId dialogId, int index)
        {
            return GetDialogSpeakerMapping(dialogId, index).Text;
        }

        private DialogSpeakerMapping GetDialogSpeakerMapping(DialogId dialogId, int index)
        {
            return _dialogData.First(mapping => mapping.Id == dialogId).DialogSpeakerData[index];
        }

        private void OnValidate()
        {
            _dialogData.ForEach(mapping => mapping.Name = $"{mapping.Id.Speaker}-{mapping.Id.State}");
        }
    }
}
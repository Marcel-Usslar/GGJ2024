using System;
using System.Collections.Generic;
using System.Linq;
using Game.Speaker;
using UnityEngine;

namespace Game.Dialog.Config
{
    public class CharacterDialogConfig : ScriptableObject
    {
        [Serializable]
        private class CharacterDialogMapping
        {
            public SpeakerType Speaker;
            public List<DialogStateMapping> DialogStateData;
        }
        [Serializable]
        private class DialogStateMapping
        {
            public int State;
            public int DialogId;
        }

        [SerializeField] private List<CharacterDialogMapping> _characterDialogData;

        public int GetDialogId(SpeakerType speaker, int state)
        {
            return _characterDialogData.First(mapping => mapping.Speaker == speaker)
                .DialogStateData.First(mapping => mapping.State == state)
                .DialogId;
        }
    }
}
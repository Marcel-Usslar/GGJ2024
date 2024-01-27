using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Dialog.Config
{
    public class CharacterDialogConfig : ScriptableObject
    {
        [Serializable]
        private class CharacterDialogMapping
        {
            public string Speaker;
            public List<DialogStateMapping> DialogStateData;
        }
        [Serializable]
        private class DialogStateMapping
        {
            public int State;
            public int DialogId;
        }

        [SerializeField] private List<CharacterDialogMapping> _characterDialogData;

        public int GetDialogId(string speaker, int state)
        {
            return _characterDialogData.First(mapping => mapping.Speaker == speaker)
                .DialogStateData.First(mapping => mapping.State == state)
                .DialogId;
        }
    }
}
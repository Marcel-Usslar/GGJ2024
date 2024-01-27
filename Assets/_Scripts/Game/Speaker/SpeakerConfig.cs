using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Speaker
{
    public class SpeakerConfig : ScriptableObject
    {
        [Serializable]
        private class SpeakerMapping
        {
            public string SpeakerName;
            public Sprite Icon;
        }

        [SerializeField] private List<SpeakerMapping> _speakerData;

        public Sprite GetSpeakerIcon(string speakerName)
        {
            return _speakerData.First(mapping => mapping.SpeakerName == speakerName).Icon;
        }
    }
}
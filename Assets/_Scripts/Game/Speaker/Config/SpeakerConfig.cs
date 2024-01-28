using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Speaker.Config
{
    public class SpeakerConfig : ScriptableObject
    {
        [Serializable]
        private class SpeakerMapping
        {
            public SpeakerType Speaker;
            public string Name;
            public Sprite Icon;
        }

        [SerializeField] private List<SpeakerMapping> _speakerData;

        public string GetSpeakerName(SpeakerType speaker)
        {
            return _speakerData.First(mapping => mapping.Speaker == speaker).Name;
        }

        public Sprite GetSpeakerIcon(SpeakerType speaker)
        {
            return _speakerData.First(mapping => mapping.Speaker == speaker).Icon;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Pools;

namespace Game.Quests
{
    public class QuestConfig : ScriptableObject, IPooledViewConfig<QuestListEntryView>
    {
        [Serializable]
        private class QuestMapping
        {
            public string QuestName;
            public string SpeakerName;
            public string QuestDescription;
        }

        [SerializeField] private QuestListEntryView _prefab;
        [SerializeField] private int _totalQuests;
        [SerializeField] private List<QuestMapping> _questData;

        public QuestListEntryView Prefab => _prefab;
        public int TotalQuests => _totalQuests;

        public bool HasQuest(string speakerName)
        {
            return _questData.Any(mapping => mapping.SpeakerName == speakerName);
        }

        public string GetQuestName(string speakerName)
        {
            return _questData.First(mapping => mapping.SpeakerName == speakerName).QuestName;
        }

        public string GetQuestDescription(string questName)
        {
            return _questData.First(mapping => mapping.QuestName == questName).QuestDescription;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Speaker;
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
            public SpeakerType Speaker;
            public string QuestDescription;
        }

        [SerializeField] private QuestListEntryView _prefab;
        [SerializeField] private int _totalQuests;
        [SerializeField] private List<QuestMapping> _questData;

        public QuestListEntryView Prefab => _prefab;
        public int TotalQuests => _totalQuests;

        public bool HasQuest(SpeakerType speaker)
        {
            return _questData.Any(mapping => mapping.Speaker == speaker);
        }

        public string GetQuestName(SpeakerType speaker)
        {
            return _questData.First(mapping => mapping.Speaker == speaker).QuestName;
        }

        public string GetQuestDescription(string questName)
        {
            return _questData.First(mapping => mapping.QuestName == questName).QuestDescription;
        }
    }
}
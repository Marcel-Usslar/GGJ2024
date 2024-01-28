using System;
using System.Collections.Generic;
using System.Linq;
using Game.Speaker;
using UnityEngine;
using Utility.Pools;

namespace Game.Quests.Config
{
    public class QuestConfig : ScriptableObject, IPooledViewConfig<QuestListEntryView>
    {
        [Serializable]
        private class QuestMapping
        {
            public QuestType Type;
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

        public QuestType GetQuestType(SpeakerType speaker)
        {
            return _questData.First(mapping => mapping.Speaker == speaker).Type;
        }

        public string GetQuestName(QuestType type)
        {
            return _questData.First(mapping => mapping.Type == type).QuestName;
        }

        public string GetQuestDescription(QuestType type)
        {
            return _questData.First(mapping => mapping.Type == type).QuestDescription;
        }

        public SpeakerType GetQuestSpeaker(QuestType type)
        {
            return _questData.First(mapping => mapping.Type == type).Speaker;
        }
    }
}
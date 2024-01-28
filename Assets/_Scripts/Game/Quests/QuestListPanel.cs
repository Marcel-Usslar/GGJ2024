using System.Collections.Generic;
using Game.UI;
using Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Utility.Pools;

namespace Game.Quests
{
    public class QuestListPanel : BaseScreen
    {
        [SerializeField] private Transform _questContent;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Slider _progressBar;

        private readonly List<QuestListEntryView> _questListEntries = new();

        protected override ReactiveProperty<bool> Visibility => QuestListModel.Instance.IsVisible;

        protected override void OnStart()
        {
            PooledView<QuestListEntryView>.Instance
                .TrySetupPool(ConfigSingletonInstaller.Instance.QuestConfig);

            QuestProgressModel.Instance.CompletedQuests.RegisterCallback(UpdateQuestProgress);
        }

        protected override void OnFinalize()
        {
            QuestProgressModel.Instance.CompletedQuests.UnregisterCallback(UpdateQuestProgress);
        }

        protected override void OnVisibilityChanged(bool visible)
        {
            if (!visible)
            {
                _questListEntries.ForEach(PooledView<QuestListEntryView>.Instance.Despawn);
                _questListEntries.Clear();
                return;
            }

            QuestSystem.Instance.ReceivedQuests.ForEach(SpawnQuestEntry);
        }

        private void SpawnQuestEntry(QuestType quest)
        {
            var entry = PooledView<QuestListEntryView>.Instance.Spawn(_questContent);
            _questListEntries.Add(entry);
            entry.Setup(quest);
        }

        private void UpdateQuestProgress(int completedQuestCount)
        {
            var totalQuests = QuestProgressModel.Instance.TotalQuests;
            var progress = (float) completedQuestCount / totalQuests;

            _progressBar.value = progress;
            _progressText.text = $"{completedQuestCount}/{totalQuests}";
        }
    }
}
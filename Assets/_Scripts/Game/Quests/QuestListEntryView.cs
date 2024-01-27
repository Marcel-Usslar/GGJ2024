using Game.Utility;
using TMPro;
using UnityEngine;

namespace Game.Quests
{
    public class QuestListEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;

        public void Setup(string quest)
        {
            var questConfig = ConfigSingletonInstaller.Instance.QuestConfig;

            _name.text = quest;
            _description.text = questConfig.GetQuestDescription(quest);
        }
    }
}
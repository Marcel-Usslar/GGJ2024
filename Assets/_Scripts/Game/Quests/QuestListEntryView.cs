using Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Quests
{
    public class QuestListEntryView : MonoBehaviour
    {
        [SerializeField] private Image _speakerIcon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;

        public void Setup(string quest)
        {
            var questConfig = ConfigSingletonInstaller.Instance.QuestConfig;
            var speakerConfig = ConfigSingletonInstaller.Instance.SpeakerConfig;
            var speaker = questConfig.GetQuestSpeaker(quest);

            _speakerIcon.sprite = speakerConfig.GetSpeakerIcon(speaker);
            _name.text = quest;
            _description.text = questConfig.GetQuestDescription(quest);
        }
    }
}
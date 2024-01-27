using Utility;
using Utility.Singletons;

namespace Game.Quests
{
    public class QuestListModel : SingletonModel<QuestListModel>
    {
        public ReactiveProperty<bool> IsVisible { get; } = new();
    }
}
using Utility;
using Utility.Singletons;

namespace Game.UI
{
    public class LoadingFeedbackModel : SingletonModel<LoadingFeedbackModel>
    {
        public ReactiveProperty<bool> ShowLoadingFeedback { get; } = new();
        public CallbackHandler OnFadeInCompleted { get; } = new();
        public CallbackHandler OnFadeOutCompleted { get; } = new();
    }
}
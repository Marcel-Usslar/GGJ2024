namespace Plugins.TweenAnimationSystem
{
    public abstract class AbstractCustomAnimationTween : AbstractAnimationTween, ICustomAnimationTween
    {
        public abstract int Id { get; }
        public abstract string DisplayName { get; }
    }
}
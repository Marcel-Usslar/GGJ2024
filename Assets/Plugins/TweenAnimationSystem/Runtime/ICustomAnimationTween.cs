namespace Plugins.TweenAnimationSystem
{
    public interface ICustomAnimationTween : IAnimationTween
    {
        /// <summary>
        /// Unique id between custom animations.
        /// Will be used to identify custom animation tween.
        /// </summary>
        int Id { get; }
        string DisplayName { get; }
    }
}
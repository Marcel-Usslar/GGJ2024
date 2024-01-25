namespace Plugins.TweenAnimationSystem
{
    internal static class AnimationTweenDataExtensions
    {
        public static T Copy<T>(this T original)
        {
#if UNITY_EDITOR
            return (T) ((object) original).Copy();
#else
            return original;
#endif
        }
    }
}
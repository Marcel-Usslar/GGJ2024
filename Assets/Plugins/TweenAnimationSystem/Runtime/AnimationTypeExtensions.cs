using System;
using Plugins.TweenAnimationSystem.Animations;

namespace Plugins.TweenAnimationSystem
{
    internal static class AnimationTypeExtensions
    {
        public static IAnimationTween GetAnimationTween(this AnimationType type)
        {
            return Activator.CreateInstance(type.GetAnimationTweenType()) as IAnimationTween;
        }

        public static Type GetAnimationTweenType(this AnimationType type)
        {
            switch (type)
            {
                case AnimationType.Interval:
                    return typeof(IntervalAnimationTween);
                case AnimationType.SceneReference:
                    return typeof(SceneReferenceAnimationTween);
                case AnimationType.ResetAnimation:
                    return typeof(ResetAnimationTween);
                case AnimationType.ToggleGameObject:
                    return typeof(ToggleGameObjectAnimationTween);
                case AnimationType.PlayParticleEffect:
                    return typeof(PlayParticleEffectAnimationTween);
                case AnimationType.InvokeCallback:
                    return typeof(InvokeCallbackAnimationTween);
                case AnimationType.FadeGraphic:
                    return typeof(FadeGraphicAnimationTween);
                case AnimationType.FadeCanvasGroup:
                    return typeof(FadeCanvasGroupAnimationTween);
                case AnimationType.Rotate:
                    return typeof(RotateAnimationTween);
                case AnimationType.PunchRotate:
                    return typeof(PunchRotateAnimationTween);
                case AnimationType.Scale:
                    return typeof(ScaleAnimationTween);
                case AnimationType.PunchScale:
                    return typeof(PunchScaleAnimationTween);
                case AnimationType.MoveAnchorPosition:
                    return typeof(MoveAnchorPositionAnimationTween);
                default:
                    return ((int) type).GetCustomAnimationTweenType();
            }
        }

        public static string GetAnimationTypeName(this AnimationType type)
        {
            if (Enum.IsDefined(typeof(AnimationType), type))
                return type.ToString();

            return ((int) type).GetCustomAnimationDisplayName();
        }
    }
}
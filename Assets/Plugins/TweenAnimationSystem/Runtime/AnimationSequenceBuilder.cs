using System.Collections.Generic;
using DG.Tweening;

namespace Plugins.TweenAnimationSystem
{
    internal class AnimationSequenceBuilder
    {
        internal static Sequence BuildSequence(IEnumerable<AnimationTweenData> tweenDataList)
        {
            var sequence = DOTween.Sequence();

            foreach (var tweenData in tweenDataList)
            {
                var tween = tweenData.CreateAnimation();

                switch (tweenData.CombinationType)
                {
                    case AnimationCombinationType.Append:
                        sequence.Append(tween);
                        break;
                    case AnimationCombinationType.JoinPrevious:
                        sequence.Join(tween);
                        break;
                }
            }

            return sequence;
        }
    }
}
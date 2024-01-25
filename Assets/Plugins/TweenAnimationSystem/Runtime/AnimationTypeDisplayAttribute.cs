using System;
using UnityEngine;

namespace Plugins.TweenAnimationSystem
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AnimationTypeDisplayAttribute : PropertyAttribute
    {
        public AnimationTypeDisplayAttribute()
        { }
    }
}
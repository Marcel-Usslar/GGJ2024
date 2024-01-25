using System;
using System.Collections.Generic;
using System.Linq;
using DebugLogger;
using Plugins.TweenAnimationSystem.Animations;
using Utility;

namespace Plugins.TweenAnimationSystem
{
    public static class CustomAnimationResolver
    {
        private const int K_customAnimationStartId = 1000000;
        private const string K_undefined = "Undefined";
        private const string K_negativeIdError =
            "Custom animations with negative ID detected. All custom animations IDs should be above 0!";
        private const string K_sharedIdError =
            "Multiple custom animations share the same ID, please check your custom animations!";

        private static IDictionary<int, ICustomAnimationTween> _customAnimationTypes;

        public static IEnumerable<int> CustomTypes
        {
            get
            {
                if (_customAnimationTypes == null)
                    _customAnimationTypes = UpdateCustomAnimationTypes();
                return _customAnimationTypes.Keys;
            }
        }

        public static Type GetCustomAnimationTweenType(this int type)
        {
            if (_customAnimationTypes == null)
                _customAnimationTypes = UpdateCustomAnimationTypes();

            if (_customAnimationTypes.ContainsKey(type))
                return _customAnimationTypes[type].GetType();

            Logger.Error($"Animation type {(AnimationType) type} was not set up. Returning dummy!");
            return typeof(DummyAnimationTween);
        }

        public static string GetCustomAnimationDisplayName(this int type)
        {
            if (_customAnimationTypes == null)
                _customAnimationTypes = UpdateCustomAnimationTypes();

            return _customAnimationTypes.ContainsKey(type)
                ? _customAnimationTypes[type].DisplayName
                : K_undefined;
        }

        private static IDictionary<int, ICustomAnimationTween> UpdateCustomAnimationTypes()
        {
            var customAnimationType = typeof(ICustomAnimationTween);

            var customAnimations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Contains(customAnimationType))
                .Select(type => Activator.CreateInstance(type) as ICustomAnimationTween)
                .ToList();

            if (customAnimations.Any(tween => tween.Id < 0))
            {
                Logger.Error(K_negativeIdError);
                return new Dictionary<int, ICustomAnimationTween>();
            }

            var dictionary = customAnimations
                .DistinctBy(tween => tween.Id)
                .ToDictionary(tween => K_customAnimationStartId + tween.Id, tween => tween);

            if (customAnimations.Count != dictionary.Keys.Count)
                Logger.Error(K_sharedIdError);


            return dictionary;
        }
    }
}
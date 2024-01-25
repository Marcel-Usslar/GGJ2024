using System.Linq;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Plugins.TweenAnimationSystem.Editor
{
    [CustomPropertyDrawer(typeof(AnimationTypeDisplayAttribute))]
    internal class AnimationTypeDisplayAttributeDrawer : PropertyDrawer
    {
        private int[] _animationTypes;
        private string[] _animationDisplayNames;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UpdateAnimationTypes();

            property.intValue = EditorGUI.IntPopup(position, "Type",
                property.intValue, _animationDisplayNames, _animationTypes);
        }

        private void UpdateAnimationTypes()
        {
            if (_animationTypes != null)
                return;

            var animationTypes = EnumHelper<AnimationType>.Iterator;
            var customTypes = CustomAnimationResolver.CustomTypes.ToList();

            _animationDisplayNames = animationTypes.Select(type => type.ToString())
                .Concat(customTypes.Select(type => type.GetCustomAnimationDisplayName())).ToArray();

            _animationTypes = animationTypes.Select(type => (int) type)
                .Concat(customTypes).ToArray();
        }
    }
}
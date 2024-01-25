using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public static class CustomEditorUtility
    {
        public const string KScriptField = "m_Script";

        /// <summary>
        /// Display property in the default editor way. (Supports all types or properties.)
        /// Best used for custom editors.
        /// </summary>
        public static void DisplayField(this SerializedProperty property, string label = null, string tooltip = null)
        {
            var content = new GUIContent(label ?? property.displayName, tooltip ?? property.tooltip);
            EditorGUILayout.PropertyField(property, content, true);
        }

        /// <summary>
        /// The Unity way of displaying the [Header] attribute inside the editor.
        /// Use EditorWindowUtility.DrawHeader() for center aligned header.
        /// </summary>
        [StringFormatMethod("header")]
        public static void DrawHeader(string header, params object[] args)
        {
            GUILayout.Space(5);

            var message = args.Length > 0 ? string.Format(header, args) : header;
            EditorGUILayout.LabelField(message, GUIStyleUtility.Bold);
        }

        public static void DrawDefaultInspector(this SerializedObject serializedObject, params string[] excludedFields)
        {
            if (!excludedFields.Contains(KScriptField))
                serializedObject.FindProperty(KScriptField).DisplayReadonlyProperty();

            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();

            var iterator = serializedObject.GetIterator();
            iterator.NextVisible(true);

            do
            {
                if (iterator.propertyPath == KScriptField || excludedFields.Contains(iterator.propertyPath))
                    continue;

                iterator.DisplayField();
            } while (iterator.NextVisible(false));

            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
        }
    }
}
using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Packages.EditorUtility
{
    public static class EditorWindowUtility
    {
        /// <summary>
        /// Displays a center aligned header.
        /// Use CustomEditorUtility.DrawHeader() for left aligned header.
        /// </summary>
        [StringFormatMethod("header")]
        public static void DrawHeader(string header, params object[] args)
        {
            GUILayout.Space(5);

            var message = args.Length > 0 ? string.Format(header, args) : header;
            EditorGUILayout.LabelField(message, GUIStyleUtility.CenteredBold);
        }
        public static void DrawUiLine(int thickness = 2, int padding = 10)
        {
            var color = new Color(0.7f, 0.7f, 0.7f);
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2.0f;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
            GUILayout.Space(5);
        }

        public static void DrawFolderSelection(string buttonText, Action<string> onFolderSelected)
        {
            if (!GUILayout.Button(buttonText))
                return;

            var folder = FolderUtility.SelectFolder();

            if (!string.IsNullOrEmpty(folder))
                onFolderSelected(folder);
        }

        public static void DrawMessageBox(string message, MessageType type = MessageType.Warning)
        {
            try
            {
                EditorGUILayout.HelpBox(message, type);
            }
            // catching errors since help-box does not work without other displayed elements inside custom editor
            catch { }
        }

        public static void DrawToggleButton(string label, ref bool buttonValue)
        {
            buttonValue = GUILayout.Toggle(buttonValue, label, new GUIStyle("Button"));
        }

        public static void DrawIdInput(string label, ref int id,
            int? minValue = null, int? maxValue = null, Action<int> onValueChanged = null)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(label, GUIStyleUtility.Bold);
            GUILayout.FlexibleSpace();
            DrawIdInput(ref id, minValue, maxValue, onValueChanged);

            EditorGUILayout.EndHorizontal();
        }
        private static void DrawIdInput(ref int id, int? minValue = null,
            int? maxValue = null, Action<int> onValueChanged = null)
        {
            var previousId = id;

            if (minValue.HasValue && id <= minValue)
                DisplayDisabled(() => GUILayout.Button("<"));
            else
                if (GUILayout.Button("<")) id--;

            id = EditorGUILayout.IntField(id, GUIStyleUtility.CenteredBoldTextField);

            if (maxValue.HasValue && id >= maxValue)
                DisplayDisabled(() => GUILayout.Button(">"));
            else
                if (GUILayout.Button(">")) id++;
            
            id = Mathf.Clamp(id, minValue ?? id, maxValue ?? id);

            if (previousId != id && onValueChanged != null)
                onValueChanged(id);
        }

        #region Field & Property display
        public static void DrawBoolField(string label, ref bool boolField, string tooltip = null)
        {
            EditorGUILayout.BeginHorizontal();

            DisplayLabel(label, tooltip);
            boolField = EditorGUILayout.Toggle(boolField);

            EditorGUILayout.EndHorizontal();
        }
        public static void DrawIntField(string label, ref int intField, string tooltip = null)
        {
            EditorGUILayout.BeginHorizontal();

            DisplayLabel(label, tooltip);
            intField = EditorGUILayout.IntField(intField);

            EditorGUILayout.EndHorizontal();
        }
        public static void DrawStringField(string label, ref string stringField, string tooltip = null)
        {
            EditorGUILayout.BeginHorizontal();

            DisplayLabel(label, tooltip);
            stringField = EditorGUILayout.TextField(stringField);

            EditorGUILayout.EndHorizontal();
        }
        public static void DrawColorField(string label, ref Color colorField, string tooltip = null)
        {
            EditorGUILayout.BeginHorizontal();

            DisplayLabel(label, tooltip);
            colorField = EditorGUILayout.ColorField(colorField);

            EditorGUILayout.EndHorizontal();
        }

        public static void DisplayReadonlyProperty(this Object config, string propertyName, string label = null, string tooltip = null)
        {
            var serializedObject = new SerializedObject(config);
            var serializedProperty = serializedObject.FindProperty(propertyName);

            serializedProperty.DisplayReadonlyProperty(label, tooltip);
        }
        public static void DisplayReadonlyProperty(this SerializedProperty property, string label = null, string tooltip = null)
        {
            DisplayDisabled(() => property.DisplayProperty(label, tooltip));
        }
        
        /// <summary>
        /// Displays the property. Not suited for Lists/Arrays.
        /// Best used inside custom windows.
        /// For a more editor like look please use CustomEditorUtility.DisplayField().
        /// </summary>
        public static void DisplayProperty(this Object config, string propertyName, string label = null, string tooltip = null)
        {
            var serializedObject = new SerializedObject(config);
            serializedObject.DisplayProperty(propertyName, label, tooltip);
        }
        public static void DisplayProperty(this SerializedObject serializedObject, string propertyName, string label = null, string tooltip = null)
        {
            var serializedProperty = serializedObject.FindProperty(propertyName);
            serializedProperty.DisplayProperty(label, tooltip);

            serializedObject.ApplyModifiedProperties();
        }
        public static void DisplayProperty(this SerializedProperty property, string label = null, string tooltip = null)
        {
            EditorGUILayout.BeginHorizontal();

            DisplayLabel(label ?? property.displayName, tooltip ?? property.tooltip);

            EditorGUILayout.PropertyField(property, GUIContent.none, true);

            EditorGUILayout.EndHorizontal();
        }

        public static void DisplayListProperty(this Object config, string listName)
        {
            var serializedObject = new SerializedObject(config);
            DisplayListProperty(serializedObject, listName);
        }
        public static void DisplayListProperty(this SerializedObject serializedObject, string listName)
        {
            EditorGUILayout.BeginHorizontal();
            
            var serializedProperty = serializedObject.FindProperty(listName);
            EditorGUILayout.PropertyField(serializedProperty, true);

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        public static int SortingLayerField(int sortingLayer)
        {
            var sortingLayerName = SortingLayerField(SortingLayer.IDToName(sortingLayer));
            return SortingLayer.NameToID(sortingLayerName);
        }
        /// <summary>
        /// https://github.com/nickgravelyn/UnityToolbag/blob/master/SortingLayer/Editor/SortingLayerExposedEditor.cs
        /// </summary>
        public static string SortingLayerField(string sortingLayer)
        {
            var sortingLayerNames = SortingLayer.layers.Select(l => l.name).ToArray();

            // Show the popup for the names
            var newLayerIndex = EditorGUILayout.Popup("Sorting Layer", Array.IndexOf(sortingLayerNames, sortingLayer), sortingLayerNames);

            // If the index changes, look up the ID for the new index to store as the new ID
            return sortingLayerNames[newLayerIndex];
        }

        private static void DisplayLabel(string label, string tooltip = null)
        {
            if (string.IsNullOrEmpty(tooltip))
                EditorGUILayout.LabelField(label);
            else
                EditorGUILayout.LabelField(new GUIContent(label, tooltip));
        }
        private static void DisplayDisabled(Action display)
        {
            GUI.enabled = false;
            display();
            GUI.enabled = true;
        }
        #endregion
    }
}
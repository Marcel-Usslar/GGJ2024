using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public static class GUIStyleUtility
    {
        public static readonly GUIStyle Bold;
        public static readonly GUIStyle CenteredBold;

        public static readonly GUIStyle CenteredBoldTextField;

        static GUIStyleUtility()
        {
            Bold = new GUIStyle(EditorStyles.boldLabel)
            {
                richText = true,
            };
            CenteredBold = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                richText = true,
            };

            CenteredBoldTextField = new GUIStyle(EditorStyles.textField)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                margin = new RectOffset(0, 0, 4, 0),
            };
        }
    }
}
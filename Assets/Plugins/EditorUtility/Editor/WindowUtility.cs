using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public static class WindowUtility
    {
        public static T ShowWindow<T>(string title, int minWidth = 100, int minHeight = 100) where T : EditorWindow
        {
            var window = CreateWindow<T>(title, minWidth, minHeight);
            window.Show();
            return window;
        }

        public static T CreateWindow<T>(string title, int minWidth = 100, int minHeight = 100) where T : EditorWindow
        {
            var window = (T) EditorWindow.GetWindow(typeof(T), true, title);

            window.name = title;

            window.ResizeWindow(minWidth, minHeight);

            return window;
        }

        public static void ResizeWindow(this EditorWindow window, int minWidth, int minHeight)
        {
            var positionX = (Screen.currentResolution.width - minWidth) / 2f;
            var positionY = (Screen.currentResolution.height - minHeight) / 2f;

            window.minSize = new Vector2(minWidth, minHeight);

            window.position = new Rect(positionX, positionY, minWidth, minHeight);
        }

        public static void ResizeWindowInPosition(this EditorWindow window, int minWidth, int minHeight)
        {
            var positionX = window.position.position.x;
            var positionY = window.position.position.y;

            window.minSize = new Vector2(minWidth, minHeight);

            window.position = new Rect(positionX, positionY, minWidth, minHeight);
        }

        public static void MoveWindowToScreenCenter(this EditorWindow window, int minWidth, int minHeight)
        {
            var windowPosition = window.position;

            var positionX = (Screen.currentResolution.width - minWidth) / 2f;
            var positionY = (Screen.currentResolution.height - minHeight) / 2f;

            windowPosition.position = new Vector2(positionX, positionY);

            window.position = windowPosition;
        }
    }
}

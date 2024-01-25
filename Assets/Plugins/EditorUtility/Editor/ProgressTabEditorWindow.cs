using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public abstract class ProgressTabEditorWindow : TabEditorWindow
    {
        private IEditorWindowProgressTab[] _tabs;

        protected abstract IEditorWindowProgressTab[] CreateProgressTabs();

        protected sealed override IEditorWindowTab[] CreateTabs()
        {
            _tabs = CreateProgressTabs();
            return _tabs.ToArray<IEditorWindowTab>();
        }

        internal override void DrawTabInfo()
        {
            var progress = _tabs.Select(CalculateProgress).ToList();
            DisplayProgressBar(progress);
        }

        private static float CalculateProgress(IEditorWindowProgressTab tab)
        {
            var tracker = new ProgressTracker();
            tab.TrackProgress(tracker);
            return tracker.CalculateProgress();
        }

        private static void DisplayProgressBar(IList<float> progresses)
        {
            var rect = EditorGUILayout.GetControlRect();
            var margin = rect.x;
            rect.width /= progresses.Count;

            GUILayout.BeginHorizontal();
            for (var i = 0; i < progresses.Count; i++)
            {
                rect.x = margin + i * rect.width;
                EditorGUI.ProgressBar(rect, progresses[i], Mathf.RoundToInt(100 * progresses[i]) + "%");
            }
            GUILayout.EndHorizontal();
        }
    }
}
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public abstract class TabEditorWindow : EditorWindow
    {
        private IEditorWindowTab[] _tabs;
        private bool _initialized;

        protected abstract int WindowWidth { get; }
        protected int SelectedTab { get; private set; }

        private void OnEnable()
        {
            _tabs = CreateTabs();
        }

        private void OnGUI()
        {
            Initialize();

            DrawHeaderContent();

            EditorGUILayout.Space();

            var previousTab = SelectedTab;
            SelectedTab = GUILayout.Toolbar(SelectedTab, _tabs.Select(tab => tab.Name).ToArray());
            DrawTabInfo();

            if (SelectedTab != previousTab)
            {
                OnTabChange();
                ResizeWindow();
            }

            EditorWindowUtility.DrawUiLine();

            DrawGenericContent();
            _tabs[SelectedTab].Display();

            EditorWindowUtility.DrawUiLine();

            DrawAction();
        }

        public void ResizeWindow()
        {
            this.ResizeWindowInPosition(WindowWidth, _tabs[SelectedTab].Height);
        }

        protected abstract IEditorWindowTab[] CreateTabs();
        protected virtual void InitializeWindow() { }
        protected virtual void DrawHeaderContent() { }
        internal virtual void DrawTabInfo() { }
        protected virtual void OnTabChange() { }
        protected virtual void DrawGenericContent() { }
        protected abstract void DrawAction();

        private void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            this.ResizeWindow(WindowWidth, _tabs[0].Height);
            this.MoveWindowToScreenCenter(WindowWidth, _tabs.Max(tab => tab.Height));

            InitializeWindow();
        }
    }
}
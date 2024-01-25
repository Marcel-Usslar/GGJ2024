using System.Linq;
using Packages.EditorUtility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using CustomButton;

namespace CustomButton.Editor
{
    [CustomEditor(typeof(ReactiveButton), true)]
    public class ReactiveButtonEditor : UnityEditor.Editor
    {
        private ReactiveButton _button;
        private GameObject _buttonGameObject;
        private ScrollRectUpdater _scrollRectUpdater;
        private DefaultButtonAnimator _animator;
        private DefaultButtonAudioPlayer _audioPlayer;

        public override void OnInspectorGUI()
        {
            if (_button == null)
                _button = (ReactiveButton) target;

            if (_buttonGameObject == null)
                _buttonGameObject = _button.gameObject;

            DrawDefaultInspector();

            DrawHitBoxWarning();
            DrawDecoratorsUpdater();
            DrawAnimatorButton();
            DrawAudioPlayerButton();
            DrawScrollRectUpdaterButton();
            DrawScrollRectWarning();
        }

        private void DrawHitBoxWarning()
        {
            var graphics = _buttonGameObject.GetComponentsInChildren<Graphic>(false);
            var colliders2D = _buttonGameObject.GetComponentsInChildren<Collider2D>(false);
            var colliders3D = _buttonGameObject.GetComponentsInChildren<Collider>(false);
            if ((graphics == null || graphics.Length == 0) &&
                (colliders2D == null || colliders2D.Length == 0) &&
                (colliders3D == null || colliders3D.Length == 0))
            {
                EditorWindowUtility.DrawMessageBox("No active hitbox for button was found!");
            }
        }

        private void DrawDecoratorsUpdater()
        {
            GUILayout.Space(10);

            if (GUILayout.Button("Update Decorators"))
                _button.EditorUpdateDecorators(_buttonGameObject.GetComponents<ButtonDecoratorComponent>().ToList());
        }

        private void DrawAnimatorButton()
        {
            if (_animator == null)
                _animator = _buttonGameObject.GetComponent<DefaultButtonAnimator>();

            if (_animator != null)
                return;

            GUILayout.Space(10);

            if (GUILayout.Button("Create Default Button Animator"))
                _animator = _buttonGameObject.AddUndoableComponent<DefaultButtonAnimator>();
        }

        private void DrawAudioPlayerButton()
        {
            if (_audioPlayer == null)
                _audioPlayer = _buttonGameObject.GetComponent<DefaultButtonAudioPlayer>();

            if (_audioPlayer != null)
                return;

            GUILayout.Space(10);

            if (GUILayout.Button("Create Default Audio Player"))
                _audioPlayer = _buttonGameObject.AddUndoableComponent<DefaultButtonAudioPlayer>();
        }

        private void DrawScrollRectUpdaterButton()
        {
            if (_scrollRectUpdater == null)
                _scrollRectUpdater = _buttonGameObject.GetComponent<ScrollRectUpdater>();

            if (_scrollRectUpdater != null)
                return;

            GUILayout.Space(10);

            if (GUILayout.Button("Create Scroll Rect Updater"))
            {
                _scrollRectUpdater = _buttonGameObject.AddUndoableComponent<ScrollRectUpdater>();
            }
        }

        private void DrawScrollRectWarning()
        {
            var scrollRect = _buttonGameObject.GetComponentInParent<ScrollRect>();

            if (_scrollRectUpdater == null && scrollRect != null)
            {
                EditorWindowUtility.DrawMessageBox("ScrollRect in parent was found. \nPlease add ScrollRectUpdater to allow scrolling inside ScrollRect.");
            }

            if (scrollRect == null && _scrollRectUpdater != null)
            {
                EditorWindowUtility.DrawMessageBox("Please make sure to only add a ScrollRectUpdater when the button is used inside a ScrollRect.", MessageType.Info);
            }
        }
    }
}
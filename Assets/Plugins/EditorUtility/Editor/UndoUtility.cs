using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Packages.EditorUtility
{
    public static class UndoUtility
    {
        public static T AddUndoableComponent<T>(this GameObject gameObject) where T : Component
        {
            return Undo.AddComponent<T>(gameObject);
        }

        public static void DestroyImmediate(this Object obj)
        {
            if (obj == null)
                return;

            Undo.DestroyObjectImmediate(obj);
        }

        public static void CreateUndoableAction(string name, Action undoableAction)
        {
            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName(name);

            undoableAction();

            Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
        }
    }
}
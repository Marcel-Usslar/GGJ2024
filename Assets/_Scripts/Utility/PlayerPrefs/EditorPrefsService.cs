using UnityEditor;

namespace Utility.PlayerPrefs
{
    public static class EditorPrefsService
    {
#if UNITY_EDITOR
        public static void SetStringPref(string name, string value)
        {
            EditorPrefs.SetString(name, value);
        }

        public static string GetStringPref(string name, string defaultValue)
        {
            return EditorPrefs.GetString(name, defaultValue);
        }

        public static void SetIntPref(string name, int value)
        {
            EditorPrefs.SetInt(name, value);
        }

        public static int GetIntPref(string name, int defaultValue = 0)
        {
            return EditorPrefs.GetInt(name, defaultValue);
        }

        public static void SetFloatPref(string name, float value)
        {
            EditorPrefs.SetFloat(name, value);
        }

        public static float GetFloatPref(string name, float defaultValue = 0.0f)
        {
            return EditorPrefs.GetFloat(name, defaultValue);
        }

        public static void SetBoolPref(string name, bool value)
        {
            SetIntPref(name, value ? 1 : 0);
        }

        public static bool GetBoolPref(string name, bool defaultValue)
        {
            return EditorPrefs.GetInt(name, defaultValue ? 1 : 0) > 0;
        }
#endif
    }
}
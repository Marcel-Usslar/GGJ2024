using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Utility.PlayerPrefs
{
    public class PlayerPrefsService
    {
        public static void SetStringPref(string name, string value)
        {
            UnityEngine.PlayerPrefs.SetString(name, value);
            UnityEngine.PlayerPrefs.Save();
        }

        public static string GetStringPref(string name, string defaultValue)
        {
            return UnityEngine.PlayerPrefs.GetString(name, defaultValue);
        }

        public static void SetIntPref(string name, int value)
        {
            UnityEngine.PlayerPrefs.SetInt(name, value);
            UnityEngine.PlayerPrefs.Save();
        }

        public static int GetIntPref(string name, int defaultValue = 0)
        {
            return UnityEngine.PlayerPrefs.GetInt(name, defaultValue);
        }

        public static void SetFloatPref(string name, float value)
        {
            UnityEngine.PlayerPrefs.SetFloat(name, value);
            UnityEngine.PlayerPrefs.Save();
        }

        public static float GetFloatPref(string name, float defaultValue = 0)
        {
            return UnityEngine.PlayerPrefs.GetFloat(name, defaultValue);
        }

        public static void SetBoolPref(string name, bool value)
        {
            SetIntPref(name, value ? 1 : 0);
        }

        public static bool GetBoolPref(string name, bool defaultValue)
        {
            return UnityEngine.PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) > 0;
        }

        public static void SetDateTimePref(string name, DateTime time)
        {
            UnityEngine.PlayerPrefs.SetString(name, JsonConvert.SerializeObject(time));
        }

        public static DateTime GetDateTimePref(string name, DateTime defaultValue)
        {
            var value = UnityEngine.PlayerPrefs.GetString(name, string.Empty);
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            try
            {
                return JsonConvert.DeserializeObject<DateTime>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        private static T GetPref<T>(string name)
        {
            var json = UnityEngine.PlayerPrefs.GetString(name);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static void SetPref<T>(string name, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            UnityEngine.PlayerPrefs.SetString(name, json);
        }

        public void LogAllPlayerPrefs()
        {
            var allPrefs = typeof(PlayerPrefsKeys).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(field => field.IsLiteral)
                .Select(field => field.GetValue(null) as string)
                .Where(UnityEngine.PlayerPrefs.HasKey)
                .ToDictionary(key => key, GetValueByKey);

            // _logger.Debug("Player Prefs:\n{0}", JsonConvert.SerializeObject(allPrefs, Formatting.Indented));
        }

        private static object GetValueByKey(string key)
        {
            if (!UnityEngine.PlayerPrefs.HasKey(key))
                return null;

            var stringValue = UnityEngine.PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(stringValue))
                return stringValue;

            var floatValue = UnityEngine.PlayerPrefs.GetFloat(key, float.NaN);
            if (!float.IsNaN(floatValue))
                return floatValue;

            var intValue = UnityEngine.PlayerPrefs.GetInt(key);
            return intValue;
        }
    }
}
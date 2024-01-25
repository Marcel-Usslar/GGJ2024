using UnityEngine;

namespace Utility
{
    public static class StringExtensions
    {
        public static string Colorize(this string stringToColorize, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{stringToColorize}</color>";
        }
    }
}
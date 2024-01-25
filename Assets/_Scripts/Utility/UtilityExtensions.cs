using System.Collections;

namespace Utility
{
    public static class UtilityExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable enumerable)
        {
            return enumerable == null || enumerable.GetEnumerator().MoveNext() == false;
        }
    }
}
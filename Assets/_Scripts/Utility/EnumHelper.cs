using System;
using System.Linq;

namespace Utility
{
    public static class EnumHelper<T>
    {
        private static T[] _enumerable;

        public static T[] Iterator => _enumerable ??= Enum.GetValues(typeof(T)) as T[];

        public static T[] IteratorExcept(params T[] valuesToExclude)
        {
            return Iterator.Where(x => !valuesToExclude.Contains(x)).ToArray();
        }

        public static int Length => Iterator.Length;

        public static void ForEach(Action<T> action)
        {
            Iterator.ForEach(action);
        }
    }
}
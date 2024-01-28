using System.Collections;

namespace Utility
{
    public static class UtilityExtensions
    {
        private const double K_maximumPrecision = 1E-5;

        /// <summary>
        /// Checks if both values are almost equal to each other within a certain precision.
        /// This comparison will lose precision when comparing with values below 1.
        /// </summary>
        public static bool AlmostEqual(this double x, double y)
        {
            return System.Math.Abs(x - y) <= Epsilon(x, y);
        }

        /// <summary>
        /// Checks if both values are almost equal to each other within a certain precision.
        /// This comparison will lose precision when comparing with values below 1.
        /// </summary>
        public static bool AlmostEqual(this float x, double y)
        {
            return System.Math.Abs(x - y) <= Epsilon(x, y);
        }

        /// <summary>
        /// Ensures that the value returned is never below the maximum intended precision.
        /// Through this, comparison against 0 is possible but values below 1 will lose precision in their calculation.
        /// This edge case is not relevant for our game as we mainly calculate with large numbers.
        /// </summary>
        private static double Epsilon(double x, double y)
        {
            var maxXY = System.Math.Max(System.Math.Abs(x), System.Math.Abs(y));
            return System.Math.Max(maxXY * K_maximumPrecision, K_maximumPrecision);
        }

        public static bool IsNullOrEmpty(this IEnumerable enumerable)
        {
            return enumerable == null || enumerable.GetEnumerator().MoveNext() == false;
        }
    }
}
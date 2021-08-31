using System;
namespace TestApp.Extensions
{
    public static class StringExtension
    {
        public static double CastDouble(this string target)
        {
            double defaultValue = 0.0;

            if (double.TryParse(target, out defaultValue))
            {
                return Math.Min(defaultValue, double.MaxValue); ;
            }

            return 0.0;
        }

        public static int CastInt(this string target)
        {
            int defaultValue = 0;

            if (int.TryParse(target, out defaultValue))
            {
                return Math.Min(defaultValue, int.MaxValue);
            }

            return 0;
        }
    }
}

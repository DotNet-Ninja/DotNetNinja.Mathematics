// Author:  .Net Ninja
// Created: 2021/07/25 10:56 PM

namespace DotNetNinja.Mathematics
{
    public static class Extensions
    {
        public static long GreatestCommonFactor(this long value, long other)
        {
            long value1 = value;
            long value2 = other;
            while (value2 != 0)
            {
                var temp = value2;
                value2 = value1 % value2;
                value1 = temp;
            }
            return value1;
        }

        public static long LowestCommonMultiple(this long value, long other)
        {
            return (value / value.GreatestCommonFactor(other)) * other;
        }
    }
}
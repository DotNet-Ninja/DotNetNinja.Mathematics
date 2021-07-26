// Author:  .Net Ninja
// Created: 2021/07/24 11:19 PM

using Xunit;

namespace DotNetNinja.Mathematics.Tests
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData(27, 63, 9)]
        [InlineData(105, 15, 15)]
        [InlineData(100, 25, 25)]
        [InlineData(120, 36, 12)]
        public void GreatestCommonFactor_ReturnsExpectedFactor(long value1, long value2, long expected)
        {
            var result = value1.GreatestCommonFactor(value2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(27, 63, 189)]
        [InlineData(105, 15, 105)]
        [InlineData(100, 25, 100)]
        [InlineData(120, 36, 360)]
        public void LowestCommonMultiple_ReturnsExpectedMultiple(long value1, long value2, long expected)
        {
            var result = value1.LowestCommonMultiple(value2);
            Assert.Equal(expected, result);
        }
    }
}
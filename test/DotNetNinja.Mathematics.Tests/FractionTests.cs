using System;
using Xunit;

namespace DotNetNinja.Mathematics.Tests
{
    public class FractionTests
    {
        [Fact]
        public void Instantiation_WithNumeratorAndDenomination_ReturnsExpectedFraction()
        {
            var fraction = new Fraction(3, 4);

            Assert.Equal(3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);
        }

        [Fact]
        public void Instantiation_WithNegativeNumeratorAndPositiveDenomination_ReturnsExpectedFraction()
        {
            var fraction = new Fraction(-3, 4);

            Assert.Equal(-3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);
        }
        
        [Fact]
        public void Instantiation_WithPositiveNumeratorAndNegativeDenomination_ReturnsExpectedFraction()
        {
            var fraction = new Fraction(3, -4);

            Assert.Equal(-3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);
        }
        
        [Fact]
        public void Instantiation_WithNegativeNumeratorAndNegativeDenomination_ReturnsExpectedFraction()
        {
            var fraction = new Fraction(-3, -4);

            Assert.Equal(3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);
        }

        [Fact]
        public void Instantiation_WithZeroDenominator_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => new Fraction(1, 0));
        }

        [Fact]
        public void Instantiation_WithNumeratorOnly_ReturnsExpectedFraction()
        {
            var fraction = new Fraction(2);

            Assert.Equal(2, fraction.Numerator);
            Assert.Equal(1, fraction.Denominator);
        }

        [Fact]
        public void Instantiation_WithInvalidString_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => new Fraction("NOT VALID"));
        }

        [Theory]
        [InlineData("12/17", 12, 17)]
        [InlineData("-12/17", -12, 17)]
        [InlineData("12/-17", -12, 17)]
        [InlineData("-12/-17", 12, 17)]
        [InlineData("3 -12/-17", 63, 17)]
        public void Instantiation_WithValidString_ReturnsExpectedFraction(string input, long numerator, long denominator)
        {
            var fraction = new Fraction(input);

            Assert.Equal(numerator, fraction.Numerator);
            Assert.Equal(denominator, fraction.Denominator);
        }

        [Fact]
        public void ToSimplified_WhenCannotBeSimplified_ReturnsSameFraction()
        {
            var fraction = new Fraction(1, 2);

            var simplified = fraction.ToSimplified();

            Assert.Equal(fraction, simplified);
        }

        [Fact]
        public void ToSimplified_WhenCanBeSimplified_ReturnsSimplestFraction()
        {
            var fraction = new Fraction(-12, 24);

            var simplified = fraction.ToSimplified();

            Assert.Equal(-1, simplified.Numerator);
            Assert.Equal(2, simplified.Denominator);
        }

        [Theory]
        [InlineData(6, 9, "6/9")]
        [InlineData(-6, 9, "-6/9")]
        [InlineData(26, 9, "26/9")]
        [InlineData(27, 9, "27/9")]
        public void ToString_ReturnsExpectedValue(long numerator, long denominator, string expected)
        {
            var fraction = new Fraction(numerator, denominator);

            var result = fraction.ToString();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(6, 9, "6/9")]
        [InlineData(-6, 9, "-6/9")]
        [InlineData(26, 9, "2 8/9")]
        [InlineData(27, 9, "3")]
        [InlineData(-27, 9, "-3")]
        [InlineData(-26, 9, "-2 8/9")]
        public void ToProperString_ReturnsExpectedValue(long numerator, long denominator, string expected)
        {
            var fraction = new Fraction(numerator, denominator);

            var result = fraction.ToProperString();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1,2, 0.5f)]
        [InlineData(1,3, 0.33333333f)]
        [InlineData(3,9, 0.33333333f)]
        [InlineData(-1,2, -0.5f)]
        [InlineData(6,2, 3.0f)]
        [InlineData(7,2, 3.5f)]
        public void ToSingle_ReturnsExpectedValue(long numerator, long denominator, float expected)
        {
            var fraction = new Fraction(numerator, denominator);

            var result = fraction.ToSingle();

            Assert.Equal(expected, result);
        }

        
        [Theory]
        [InlineData(1,2, 0.5)]
        [InlineData(1,3, 0.3333333333333333)]
        [InlineData(3,9, 0.3333333333333333)]
        [InlineData(-1,2, -0.5)]
        [InlineData(6,2, 3.0)]
        [InlineData(15,4, 3.75)]
        public void ToDouble_ReturnsExpectedValue(long numerator, long denominator, double expected)
        {
            var fraction = new Fraction(numerator, denominator);

            var result = fraction.ToDouble();

            Assert.Equal(expected, result);
        }

        
        [Theory]
        [InlineData(1,2, "0.5")]
        [InlineData(1,3, "0.3333333333333333333333333333")]
        [InlineData(3,9, "0.3333333333333333333333333333")]
        [InlineData(-1,2, "-0.5")]
        [InlineData(6,2, "3.0")]
        [InlineData(15,4, "3.75")]
        public void ToDecimal_ReturnsExpectedValue(long numerator, long denominator, string value)
        {
            var expected = Convert.ToDecimal(value);
            var fraction = new Fraction(numerator, denominator);

            var result = fraction.ToDecimal();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void EqualsObject_WhenEqual_ReturnsTrue()
        {
            Fraction fraction = new Fraction(1, 2);
            object obj = new Fraction(1, 2);

            var result = fraction.Equals(obj);

            Assert.True(result);
        }
        
        [Fact]
        public void EqualsObject_WhenNotEqual_ReturnsFalse()
        {
            Fraction fraction = new Fraction(1, 2);
            object obj = new Fraction(1, 3);

            var result = fraction.Equals(obj);

            Assert.False(result);
        }
                
        [Fact]
        public void EqualsObject_WhenNotFraction_ReturnsFalse()
        {
            Fraction fraction = new Fraction(1, 2);
            object obj = "new Fraction(1, 2)";

            var result = fraction.Equals(obj);

            Assert.False(result);
        }

        [Fact]
        public void Equals_WhenEqual_ReturnsTrue()
        {
            Fraction fraction = new Fraction(1, 2);
            Fraction obj = new Fraction(1, 2);

            var result = fraction.Equals(obj);

            Assert.True(result);
        }
        
        [Fact]
        public void Equals_WhenNotEqual_ReturnsFalse()
        {
            Fraction fraction = new Fraction(1, 2);
            Fraction obj = new Fraction(1, 3);

            var result = fraction.Equals(obj);

            Assert.False(result);
        }

        [Fact]
        public void Equals2X_WhenEqual_ReturnsTrue()
        {
            Fraction fraction1 = new Fraction(1, 2);
            Fraction fraction2 = new Fraction(1, 2);

            var result = fraction1.Equals(fraction1, fraction2);

            Assert.True(result);
        }
        
        [Fact]
        public void Equals2X_WhenNotEqual_ReturnsFalse()
        {
            Fraction fraction1 = new Fraction(1, 2);
            Fraction fraction2 = new Fraction(1, 4);

            var result = fraction1.Equals(fraction1, fraction2);

            Assert.False(result);
        }

        [Fact]
        public void EqualsLong_WhenEqual_ReturnsTrue()
        {
            var fraction = new Fraction(3, 1);

            var result = fraction.Equals(3);

            Assert.True(result);
        }

        [Fact]
        public void EqualsLong_WhenNotEqual_ReturnsFalse()
        {
            var fraction = new Fraction(3, 2);

            var result = fraction.Equals(3);

            Assert.False(result);
        }
        
        [Fact]
        public void EqualsFloat_WhenEqual_ReturnsTrue()
        {
            var fraction = new Fraction(3, 1);

            var result = fraction.Equals(3.0f);

            Assert.True(result);
        }

        [Fact]
        public void EqualsFloat_WhenNotEqual_ReturnsFalse()
        {
            var fraction = new Fraction(3, 2);

            var result = fraction.Equals(3.0f);

            Assert.False(result);
        }
                
        [Fact]
        public void EqualsDouble_WhenEqual_ReturnsTrue()
        {
            var fraction = new Fraction(3, 1);

            var result = fraction.Equals(3.0);

            Assert.True(result);
        }

        [Fact]
        public void EqualsDouble_WhenNotEqual_ReturnsFalse()
        {
            var fraction = new Fraction(3, 2);

            var result = fraction.Equals(3.0);

            Assert.False(result);
        }
                
        [Fact]
        public void EqualsDecimal_WhenEqual_ReturnsTrue()
        {
            var fraction = new Fraction(3, 1);

            var result = fraction.Equals(3.0M);

            Assert.True(result);
        }

        [Fact]
        public void EqualsDecimal_WhenNotEqual_ReturnsFalse()
        {
            var fraction = new Fraction(3, 2);

            var result = fraction.Equals(3.0M);

            Assert.False(result);
        }

        [Fact]
        public void CompareToFraction_WhenGreaterThanOther_ReturnsPositive()
        {
            var fraction = new Fraction(3, 4);
            var other = new Fraction(1, 2);

            var result = fraction.CompareTo(other);

            Assert.Equal(1, result);
        }
        
        [Fact]
        public void CompareToFraction_WhenLessThanOther_ReturnsNegative()
        {
            var fraction = new Fraction(1, 4);
            var other = new Fraction(1, 2);

            var result = fraction.CompareTo(other);

            Assert.Equal(-1, result);
        }
        
        [Fact]
        public void CompareToFraction_WhenEqualToOther_ReturnsZero()
        {
            var fraction = new Fraction(3, 4);
            var other = new Fraction(3, 4);

            var result = fraction.CompareTo(other);

            Assert.Equal(0, result);
        }

        [Fact]
        public void CompareToLong_WhenGreaterThanOther_ReturnsPositive()
        {
            var fraction = new Fraction(6, 4);
            var other = 1L;

            var result = fraction.CompareTo(other);

            Assert.Equal(1, result);
        }
        
        [Fact]
        public void CompareToLong_WhenLessThanOther_ReturnsNegative()
        {
            var fraction = new Fraction(1, 4);
            var other = 1L;

            var result = fraction.CompareTo(other);

            Assert.Equal(-1, result);
        }
        
        [Fact]
        public void CompareToLong_WhenEqualToOther_ReturnsZero()
        {
            var fraction = new Fraction(8, 4);
            var other = 2L;

            var result = fraction.CompareTo(other);

            Assert.Equal(0, result);
        }
        
        [Fact]
        public void CompareToFloat_WhenGreaterThanOther_ReturnsPositive()
        {
            var fraction = new Fraction(6, 4);
            var other = 1f;

            var result = fraction.CompareTo(other);

            Assert.Equal(1, result);
        }
        
        [Fact]
        public void CompareToFloat_WhenLessThanOther_ReturnsNegative()
        {
            var fraction = new Fraction(1, 4);
            var other = 1f;

            var result = fraction.CompareTo(other);

            Assert.Equal(-1, result);
        }
        
        [Fact]
        public void CompareToFloat_WhenEqualToOther_ReturnsZero()
        {
            var fraction = new Fraction(8, 4);
            var other = 2f;

            var result = fraction.CompareTo(other);

            Assert.Equal(0, result);
        }
        
        [Fact]
        public void CompareToDouble_WhenGreaterThanOther_ReturnsPositive()
        {
            var fraction = new Fraction(6, 4);
            var other = 1.0;

            var result = fraction.CompareTo(other);

            Assert.Equal(1, result);
        }
        
        [Fact]
        public void CompareToDouble_WhenLessThanOther_ReturnsNegative()
        {
            var fraction = new Fraction(1, 4);
            var other = 1.0;

            var result = fraction.CompareTo(other);

            Assert.Equal(-1, result);
        }
        
        [Fact]
        public void CompareToDouble_WhenEqualToOther_ReturnsZero()
        {
            var fraction = new Fraction(8, 4);
            var other = 2.0;

            var result = fraction.CompareTo(other);

            Assert.Equal(0, result);
        }
        
        [Fact]
        public void CompareToDecimal_WhenGreaterThanOther_ReturnsPositive()
        {
            var fraction = new Fraction(6, 4);
            var other = 1M;

            var result = fraction.CompareTo(other);

            Assert.Equal(1, result);
        }
        
        [Fact]
        public void CompareToDecimal_WhenLessThanOther_ReturnsNegative()
        {
            var fraction = new Fraction(1, 4);
            var other = 1M;

            var result = fraction.CompareTo(other);

            Assert.Equal(-1, result);
        }
        
        [Fact]
        public void CompareToDecimal_WhenEqualToOther_ReturnsZero()
        {
            var fraction = new Fraction(8, 4);
            var other = 2M;

            var result = fraction.CompareTo(other);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetHashCode_WithSameNumeratorAndDenominator_ReturnsConsistentValue()
        {
            var fraction1 = new Fraction(1, 2);
            var fraction2 = new Fraction(1, 2);

            var hash1 = fraction1.GetHashCode();
            var hash2 = fraction2.GetHashCode();

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_WithDifferentNumerators_ReturnsDifferentValue()
        {
            var fraction1 = new Fraction(3, 2);
            var fraction2 = new Fraction(1, 2);

            var hash1 = fraction1.GetHashCode();
            var hash2 = fraction2.GetHashCode();

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_WithDifferentDenominators_ReturnsDifferentValue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 2);

            var hash1 = fraction1.GetHashCode();
            var hash2 = fraction2.GetHashCode();

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GetHashCodeWithObject_WithSameNumeratorAndDenominator_ReturnsConsistentValue()
        {
            var fraction1 = new Fraction(1, 2);
            var fraction2 = new Fraction(1, 2);
            
            var hash1 = fraction1.GetHashCode(fraction2);
            var hash2 = fraction2.GetHashCode(fraction1);

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCodeWithObject_WithDifferentNumerators_ReturnsDifferentValue()
        {
            var fraction1 = new Fraction(3, 2);
            var fraction2 = new Fraction(1, 2);
            
            var hash1 = fraction1.GetHashCode(fraction2);
            var hash2 = fraction2.GetHashCode(fraction1);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GetHashCodeWithObject_WithDifferentDenominators_ReturnsDifferentValue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 2);

            var hash1 = fraction1.GetHashCode(fraction2);
            var hash2 = fraction2.GetHashCode(fraction1);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void EqualsOperator_WhenEqual_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 == fraction2;

            Assert.True(result);
        }
        
        [Fact]
        public void EqualsOperator_WhenEqualWithDifferentDenominator_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 6);

            var result = fraction1 == fraction2;

            Assert.True(result);
        }

        [Fact]
        public void EqualsOperator_WhenNotEqual_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 3);

            var result = fraction1 == fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void NotEqualsOperator_WhenEqual_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 != fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void NotEqualsOperator_WhenEqualWithDifferentDenominator_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 6);

            var result = fraction1 != fraction2;

            Assert.False(result);
        }

        [Fact]
        public void NotEqualsOperator_WhenNotEqual_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 3);

            var result = fraction1 != fraction2;

            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOperator_WhenGreaterThanOther_ReturnsTrue()
        {
            var fraction1 = new Fraction(2, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 > fraction2;

            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOperator_WhenLessThanOther_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 3);

            var result = fraction1 > fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThanOperator_WhenEqualToOther_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 > fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void LessThanOperator_WhenGreaterThanOther_ReturnsFalse()
        {
            var fraction1 = new Fraction(2, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 < fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void LessThanOperator_WhenLessThanOther_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 3);

            var result = fraction1 < fraction2;

            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOperator_WhenEqualToOther_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 < fraction2;

            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqualToOperator_WhenGreaterThanOther_ReturnsTrue()
        {
            var fraction1 = new Fraction(2, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 >= fraction2;

            Assert.True(result);
        }
        
        [Fact]
        public void GreaterThanOrEqualToOperator_WhenLessThanOther_ReturnsFalse()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 3);

            var result = fraction1 >= fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void GreaterThanOrEqualToOperator_WhenEqualToOther_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 >= fraction2;

            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqualToOperator_WhenGreaterThanOther_ReturnsFalse()
        {
            var fraction1 = new Fraction(2, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 <= fraction2;

            Assert.False(result);
        }
        
        [Fact]
        public void LessThanOrEqualToOperator_WhenLessThanOther_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(2, 3);

            var result = fraction1 <= fraction2;

            Assert.True(result);
        }
        
        [Fact]
        public void LessThanOrEqualToOperator_WhenEqualToOther_ReturnsTrue()
        {
            var fraction1 = new Fraction(1, 3);
            var fraction2 = new Fraction(1, 3);

            var result = fraction1 <= fraction2;

            Assert.True(result);
        }

        [Theory]
        [InlineData(1, 2, 1, 3, 5, 6)]
        [InlineData(5, 8, 3, 4, 11, 8)]
        [InlineData(1, 3, 6, 9, 1, 1)]
        [InlineData(3, 4, -5, 8, 1, 8)]
        public void AdditionOperator_ReturnsExpectedResult(long numerator1, long denominator1, long numerator2,
            long denominator2, long numeratorExpected, long denominatorExpected)
        {
            var expected = new Fraction(numeratorExpected, denominatorExpected);
            var value1 = new Fraction(numerator1, denominator1);
            var value2 = new Fraction(numerator2, denominator2);

            var result = value1 + value2;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 5, 6, -1, 3)]
        [InlineData(5, 6, 1, 2, 1, 3)]
        [InlineData(-5, 6, 1, 2, -4, 3)]
        public void SubtractionOperator_ReturnsExpectedResult(long numerator1, long denominator1, long numerator2,
            long denominator2, long numeratorExpected, long denominatorExpected)
        {
            var expected = new Fraction(numeratorExpected, denominatorExpected);
            var value1 = new Fraction(numerator1, denominator1);
            var value2 = new Fraction(numerator2, denominator2);

            var result = value1 - value2;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 1, 2, 1, 4)]
        [InlineData(1, 3, 2, 3, 2, 9)]
        [InlineData(-5, 6, 1, 2, -5, 12)]
        public void MultiplicationOperator_ReturnsExpectedResult(long numerator1, long denominator1, long numerator2,
            long denominator2, long numeratorExpected, long denominatorExpected)
        {
            var expected = new Fraction(numeratorExpected, denominatorExpected);
            var value1 = new Fraction(numerator1, denominator1);
            var value2 = new Fraction(numerator2, denominator2);

            var result = value1 * value2;

            Assert.Equal(expected, result);
        }

        
        [Theory]
        [InlineData(1, 2, 1, 2, 1, 1)]
        [InlineData(1, 3, 2, 3, 1, 2)]
        [InlineData(-5, 6, 1, 2, -5, 3)]
        public void DivisionOperator_ReturnsExpectedResult(long numerator1, long denominator1, long numerator2,
            long denominator2, long numeratorExpected, long denominatorExpected)
        {
            var expected = new Fraction(numeratorExpected, denominatorExpected);
            var value1 = new Fraction(numerator1, denominator1);
            var value2 = new Fraction(numerator2, denominator2);

            var result = value1 / value2;

            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("1/2", 1, 2)]
        [InlineData("3/2", 3, 2)]
        [InlineData("1 1/2", 3, 2)]
        //[InlineData("3", 3, 1)]
        public void InstantiatingFromString_SetsExpectedValues(string value, long numerator, long denominator)
        {
            Fraction fraction = value;

            Assert.Equal(numerator, fraction.Numerator);
            Assert.Equal(denominator, fraction.Denominator);
        }
    }
}

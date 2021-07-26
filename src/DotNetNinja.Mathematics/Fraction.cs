using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetNinja.Mathematics
{
public readonly struct Fraction: IEquatable<Fraction>, 
                                    IEqualityComparer<Fraction>,
                                    IEquatable<long>,
                                    IEquatable<float>,
                                    IEquatable<double>,
                                    IEquatable<decimal>,
                                    IComparable<Fraction>,
                                    IComparable<long>,
                                    IComparable<float>,
                                    IComparable<double>,
                                    IComparable<decimal>
    {
        private const string ValidationPattern =@"^[\-\+]{0,1}\d{0,}\s{0,}[\-\+]{0,1}\d{1,}/[\-\+]{0,1}\d{1,}$";

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0) throw new DivideByZeroException("Denominator cannot be zero.");
            if (denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }            
            Numerator = numerator;
            Denominator = denominator;
        }
        
        public Fraction(long value):this(value, 1)
        {
        }

        public Fraction(string value)
        {
            var data = value.Trim();
            if (!Regex.IsMatch(data, ValidationPattern, RegexOptions.Singleline))
            {
                throw new FormatException($"Value '{value}' is not in the expected format.");
            }
            
            var components = data.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            var fractionIndex = 0;
            var whole = 0;
            if (!components[0].Contains("/") && int.TryParse(components[0], out whole))
            {
                fractionIndex = 1;
            }
            var fractionComponents = components[fractionIndex].Split('/');
            var denominator = int.Parse(fractionComponents[1]);
            var numerator = int.Parse(fractionComponents[0]);
            if (denominator < 0)
            {
                // ReSharper disable once IntVariableOverflowInUncheckedContext
                denominator *= -1;
                numerator *= -1;
            }
            Numerator =(whole < 0)
                ? whole * denominator - numerator
                : whole * denominator + numerator;
            Denominator = denominator;
        }
        
        public long Numerator { get; }
        public long Denominator { get; }
        
        public Fraction ToSimplified()
        {
            var gcd = GreatestCommonDivisor(this);
            return new Fraction(Numerator/gcd, Denominator/gcd);
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public string ToProperString()
        {
            var numerator = Numerator;
            var negative = string.Empty;
            if (Numerator < 0)
            {
                negative = "-";
                numerator *= -1;
            }
            var whole = numerator / Denominator;
            numerator = numerator % Denominator;
            var fraction = (numerator!=0)?$"{numerator}/{Denominator}":string.Empty;
            var wholeString = (whole != 0) ? $"{whole} " : string.Empty;
            return $"{negative}{wholeString}{fraction}".Trim();
        }

        public float ToSingle()
        {
            return (float)Numerator / Denominator;
        }

        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }

        public decimal ToDecimal()
        {
            return (decimal)Numerator / Denominator;
        }
        
        private long GreatestCommonDivisor(Fraction value)
        {
            var smallest = (value.Numerator > value.Denominator) ? value.Denominator : value.Numerator;
            var largest = (value.Numerator > value.Denominator) ? value.Numerator : value.Denominator;
            var candidates = GetPrimeFactors(smallest);
            var factors = FindDivisors(largest, candidates);
            return factors.Aggregate(1L, (current, factor) => current * factor);
        }

        private IEnumerable<long> GetPrimeFactors(long value)
        {
            if (value < 0) yield return -1;
            var temp = Math.Abs(value);
            var hasCandidates = false;
            var candidate = 2;
            while (candidate <= temp)
            {
                if (temp % candidate == 0)
                {
                    temp /= candidate;
                    hasCandidates = true;
                    yield return candidate;
                }
                else
                {
                    candidate++;
                }
            }
            if(!hasCandidates) yield return value;
        }

        private IEnumerable<long> FindDivisors(long value, IEnumerable<long> candidates)
        {
            var temp = value;
            foreach (var candidate in candidates)
            {
                if (temp % candidate == 0)
                {
                    temp /= candidate;
                    yield return candidate;
                }
            }
        }

        internal static (Fraction, Fraction) ConvertToCommonDenominator(Fraction value1, Fraction value2)
        {
            if (value1.Denominator == value2.Denominator) return (value1, value2);
            var lowestCommonMultiple = value1.Denominator.LowestCommonMultiple(value2.Denominator);
            var multiplier1 = lowestCommonMultiple / value1.Denominator;
            var multiplier2 = lowestCommonMultiple / value2.Denominator;
            return (new Fraction(value1.Numerator * multiplier1, value1.Denominator * multiplier1),
                new Fraction(value2.Numerator * multiplier2, value2.Denominator * multiplier2));
        }
        
        public override bool Equals(object obj)
        {
            return obj is Fraction other && Equals(other);
        }

        public bool Equals(Fraction other)
        {
            (Fraction left, Fraction right) = ConvertToCommonDenominator(this, other);
            return left.Numerator == right.Numerator && left.Denominator == right.Denominator;
        }
        
        public bool Equals(Fraction x, Fraction y)
        {
            (Fraction left, Fraction right) = ConvertToCommonDenominator(x, y);
            return left.Numerator == right.Numerator && left.Denominator == right.Denominator;
        }

        public bool Equals(long other)
        {
            return Equals(new Fraction(other));
        }

        public bool Equals(float other)
        {
            return ToSingle().Equals(other);
        }

        public bool Equals(double other)
        {
            return ToDouble().Equals(other);
        }

        public bool Equals(decimal other)
        {            
            return ToDecimal().Equals(other);
        }

        public int CompareTo(Fraction other)
        {
            (Fraction fraction1, Fraction fraction2) = ConvertToCommonDenominator(this, other);
            return fraction1.Numerator.CompareTo(fraction2.Numerator);
        }

        public int CompareTo(long other)
        {
            var fraction = new Fraction(other);
            return CompareTo(fraction);
        }

        public int CompareTo(float other)
        {
            return ToSingle().CompareTo(other);
        }

        public int CompareTo(double other)
        {
            return ToDouble().CompareTo(other);
        }

        public int CompareTo(decimal other)
        {
            return ToDecimal().CompareTo(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }
        
        public int GetHashCode(Fraction obj)
        {
            return HashCode.Combine(obj.Numerator, obj.Denominator);
        }

        public static bool operator ==(Fraction left, Fraction right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return !(left == right);
        }

        public static bool operator <(Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Fraction left, Fraction right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static Fraction operator +(Fraction left, Fraction right)
        {
            (Fraction value1, Fraction value2) = Fraction.ConvertToCommonDenominator(left, right);
            return (new Fraction(value1.Numerator + value2.Numerator, value1.Denominator)).ToSimplified();
        }
        
        public static Fraction operator -(Fraction left, Fraction right)
        {
            (Fraction value1, Fraction value2) = Fraction.ConvertToCommonDenominator(left, right);
            return (new Fraction(value1.Numerator - value2.Numerator, value1.Denominator)).ToSimplified();
        }
        
        public static Fraction operator *(Fraction left, Fraction right)
        {
            return (new Fraction(left.Numerator * right.Numerator, left.Denominator * right.Denominator)).ToSimplified();
        }
        
        public static Fraction operator /(Fraction left, Fraction right)
        {
            return (new Fraction(left.Numerator * right.Denominator, left.Denominator * right.Numerator)).ToSimplified();
        }
        
        public static implicit operator Fraction(long value)
        {
            return new Fraction(value);
        }

        public static implicit operator Fraction(string value)
        {
            return new Fraction(value);
        }
    }
}

﻿using System;
namespace Majanias.GregYoung
{
    public class Probability
    {
        private readonly float _value;
        private const float _epsilon = 0.0000001f;

        public Probability(decimal value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentOutOfRangeException("value");
            _value = (float)value;
        }

        public Probability(float value)
        {
            if (value >= 0 && value <= 1)
                _value = value;
            else
                throw new ArgumentOutOfRangeException("value");
        }

        public Probability CombinedWith(Probability other)
        {
            return new Probability(_value * other._value);
        }

        public Probability Either(Probability other)
        {
            return new Probability(_value + other._value - (_value * other._value));
        }

        public Probability InverseOf()
        {
            return new Probability(1 - _value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Probability))
                return false;

            return this == (Probability)obj;
        }

        public static bool operator ==(Probability a, Probability b)
        {
            return !(a < b) && !(a > b);
        }

        public static bool operator !=(Probability a, Probability b)
        {
            return (a < b) || (a > b);
        }

        public static bool operator <(Probability a, Probability b)
        {
            if (Object.ReferenceEquals(a, b))
                return false;

            if ((object)a == null)
                return true;

            if ((object)b == null)
                return false;

            return (a._value - b._value) < -_epsilon;
        }

        public static bool operator >(Probability a, Probability b)
        {
            if (Object.ReferenceEquals(a, b))
                return false;

            if ((object)a == null)
                return false;

            if ((object)b == null)
                return true;

            return (a._value - b._value) > _epsilon;
        }

        public static bool operator <=(Probability a, Probability b)
        {
            return !(a > b);
        }

        public static bool operator >=(Probability a, Probability b)
        {
            return !(a < b);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}

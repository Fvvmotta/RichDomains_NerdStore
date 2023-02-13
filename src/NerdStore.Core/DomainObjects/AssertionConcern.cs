using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NerdStore.Core.DomainObjects
{
    public class AssertionConcern
    {
        public static void AssertArgumentIfEquals(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfNotEquals(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfNotEquals(string pattern, string stringValue, string message)
        {
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(stringValue))
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentExpression(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentRange(decimal value, decimal minimum, decimal maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfLessThanOrEqualsMinimun(double value, double minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfLessThanOrEqualsMinimun(float value, float minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfLessThanOrEqualsMinimun(int value, int minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfLessThanOrEqualsMinimun(long value, long minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfLessThanOrEqualsMinimun(decimal value, decimal minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfFalse(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArgumentIfTrue(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertStateIfFalse(bool boolValue, string message)
        {
            if (boolValue)
            {
                throw new DomainException(message);
            }
        }
        public static void AssertStateIfTrue(bool boolValue, string message)
        {
            if (!boolValue)
            {
                throw new DomainException(message);
            }
        }
    }
}

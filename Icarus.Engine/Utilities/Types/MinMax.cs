using System;

namespace Icarus.Engine.Utilities.Types
{
    /// <summary>
    /// A data structure that allows you to define a constrained, comparable value.  Constraints are inclusive. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinMax<T> where T : IComparable
    {
        private T _current;
        private T _minimum;
        private T _maximum;

        /// <summary>
        /// Sets or gets the current value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T Current
        {
            get => _current;
            set
            {
                if (value.CompareTo(Maximum) > 0 || value.CompareTo(Minimum) < 0)
                    throw new ArgumentOutOfRangeException(nameof(Current), value,
                        $"Can not set the current value to be less than the minimum or greater than the maximum: {Minimum}...{Maximum}");

                _current = value;
            }
        }

        /// <summary>
        /// Sets or gets the current minimum.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T Minimum
        {
            get => _minimum;
            set
            {
                if (value.CompareTo(Maximum) > 0)
                    throw new ArgumentOutOfRangeException(nameof(Minimum), value,
                        $"Can not set minimum to greater than or equal to the current maximum: {Maximum}");

                _minimum = value;
            }
        }

        /// <summary>
        /// Sets or gets the current maximum.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T Maximum
        {
            get => _maximum;
            set
            {
                if (value.CompareTo(Minimum) < 0)
                    throw new ArgumentOutOfRangeException(nameof(Maximum), value,
                        $"Can not set the maximum to less than or equal to the current minimum: {Minimum}");

                _maximum = value;
            }
        }

        /// <summary>
        /// Attempts to set the value.  If the proposed value would violate the constraints, returns false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TrySet(T value)
        {
            if (!CanSet(value))
                return false;
            
            Current = value;
            return true;
        }

        /// <summary>
        /// Returns true/false indicating if the given value is valid for this object's value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CanSet(T value)
        {
            return value.CompareTo(Maximum) <= 0 && value.CompareTo(Minimum) >= 0;
        }
    }
}
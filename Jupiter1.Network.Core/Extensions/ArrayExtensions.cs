using System;

namespace Jupiter1.Network.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Assign<T>(this T[] source, T value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Length <= 0)
                throw new ArgumentException(nameof(source));

            var destination = new T[source.Length];
            for (var i = 0; i < destination.Length; ++i)
            {
                destination[i] = value;
            }

            return destination;
        }

        public static T[] Assign<T>(this T[] source, Func<T> func)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Length <= 0)
                throw new ArgumentException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            var destination = new T[source.Length];
            for (var i = 0; i < destination.Length; ++i)
            {
                destination[i] = func();
            }

            return destination;
        }
    }
}
using System;

namespace Jupiter1.Network.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static void Assign<T>(this T[] source, Func<T> func)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            for (var i = 0; i < source.Length; ++i)
                source[i] = func();
        }
    }
}
namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class Array.
    /// </summary>
    public class Array
    {
        /// <summary>
        /// Get the empty array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T[].</returns>
        public static T[] Empty<T>()
        {
#if NET45
            return EmptyArray<T>.Value;
#else
            return System.Array.Empty<T>();
#endif
        }

        private static class EmptyArray<T>
        {
#pragma warning disable CA1825
            internal static readonly T[] Value = new T[0];
#pragma warning restore CA1825
        }
    }
}

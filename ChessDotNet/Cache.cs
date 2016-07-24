namespace ChessDotNet
{
    /// <summary>
    /// The Cache class is used internally to cache information such as: is this player in check? It's still a public class because you might want to use it for derived classes for chess variants.
    /// </summary>
    /// <typeparam name="T">The type of the data that has to be cached.</typeparam>
    public class Cache<T>
    {
        public T Value
        {
            get;
            private set;
        }

        public int CachedAt
        {
            get;
            private set;
        }

        public Cache(T value, int cachedAt)
        {
            Value = value;
            CachedAt = cachedAt;
        }

        public T UpdateCache(T newValue, int cachedAt)
        {
            Value = newValue;
            CachedAt = cachedAt;
            return newValue;
        }
    }
}

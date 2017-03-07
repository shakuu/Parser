namespace Parser.HttpContextUtilities.Contracts
{
    public interface IHttpContextCacheProvider
    {
        object this[string index] { get; }

        void Add(string key, object value);

        void Remove(string key);
    }
}

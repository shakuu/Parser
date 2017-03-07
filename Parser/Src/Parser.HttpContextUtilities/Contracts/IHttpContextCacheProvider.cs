namespace Parser.HttpContextUtilities.Contracts
{
    public interface IHttpContextCacheProvider
    {
        object this[string index] { get; set; }

        void Add(string key, object value);

        void Remove(string key);
    }
}

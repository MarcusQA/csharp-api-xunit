namespace csharp_api_xunit
{
    public abstract class BaseApiTests
    {
        protected const string _defaultBaseUri = "https://jsonplaceholder.typicode.com";
        protected Dictionary<string, string> headers;

        public BaseApiTests()
        {
            headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
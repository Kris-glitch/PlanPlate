
namespace PlanPlate.Network
{
    public class HttpFactoryService : IHttpFactory
    {

        static readonly string BaseUrl = "https://www.themealdb.com/api/json/v1/1";

        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(BaseUrl)
        };

        public HttpClient CreateHttpClient()
        {
            return _httpClient;
        }
    }
}

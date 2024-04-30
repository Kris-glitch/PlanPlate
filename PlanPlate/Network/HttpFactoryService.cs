
using System.Net.Http.Headers;

namespace PlanPlate.Network
{
    public class HttpFactoryService : IHttpFactory
    {

        private readonly Lazy<HttpClient> _httpClient;

        public HttpFactoryService()
        {
           
            _httpClient = new Lazy<HttpClient>(() =>
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public HttpClient CreateHttpClient()
        {
            return _httpClient.Value;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Communication
{
    public class CommunicationProvider : ICommunicationProvider
    {
        private const string ContentType = "application/json";

        private readonly HttpClient _httpClient;
        //private readonly string _remoteServiceBaseUrl;

        public CommunicationProvider(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
           // _httpClient.BaseAddress = new Uri("http://example.com/");
        }

        public async Task<T_OUT> SendAsync<T_OUT, T_IN>(string url,HttpMethod method,T_IN param)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            T_OUT response = default(T_OUT);

            if (param != null)
            {
                var jsonParam = ConvertToJson<T_IN>(param);
                request.Content = new StringContent(jsonParam, Encoding.UTF8, ContentType);
            }

            var httpResponse = await _httpClient.SendAsync(request);
            //if ((int)httpReplay.StatusCode < 200 || (int)httpReplay.StatusCode >= 500)
            //{
            //    //TODO: log error
            //}
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<T_OUT>(responseContent);
            }

            return response;
        }

        private string ConvertToJson<T_IN>(T_IN param)
        {
            string jsonString = JsonConvert.SerializeObject(param, Formatting.Indented);
            return jsonString;
        }
    }
}

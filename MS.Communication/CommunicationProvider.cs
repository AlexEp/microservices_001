using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MS.Communication
{
    public class CommunicationProvider : ICommunicationProvider
    {
        private const string ContentType = "application/json";

   
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly BasicHttpClient _httpClient;
        //private readonly string _remoteServiceBaseUrl;

        public CommunicationProvider(BasicHttpClient httpClient, IConfiguration configuration, ILogger logger )
        {
            _configuration = configuration;
            this._logger = logger;
           // _httpClientFactory = httpClientFactory;
            _httpClient = httpClient; //_httpClientFactory.CreateClient(); //Create default  httpClient
        }

        public async Task<T_OUT> SendAsync<T_OUT, T_IN>(string url,HttpMethod method,T_IN param)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            T_OUT response = default(T_OUT);
            string jsonParam ="";
            if (param != null)
            {
                jsonParam = ConvertToJson<T_IN>(param);
                request.Content = new StringContent(jsonParam, Encoding.UTF8, ContentType);
            }

            //Log ...
            var id = Dns.GetHostName(); // get container id
            var ip = Dns.GetHostEntry(id).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            var serverName = @Assembly.GetEntryAssembly().GetName().Name;
        
            _logger.LogError($"HTTP REQUEST : {serverName} [id={id} ip={ip}]  ---> {url}");
            //_logger.LogTrace(jsonParam);

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

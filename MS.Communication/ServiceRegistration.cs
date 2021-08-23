using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MS.Communication;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace MS.Communication
{
    public static class AppCommunicationRegistration
    {
        public static IServiceCollection AddCommunicationProvider(this IServiceCollection services, IConfiguration configuration,ILogger logger)
        {
            services.AddHttpClient(); //Add IHttpClientFactory
            services.AddScoped<ICommunicationProvider>(sp => new CommunicationProvider(sp.GetRequiredService<IHttpClientFactory>(), configuration, logger));
          
            return services;
        }
    }
}

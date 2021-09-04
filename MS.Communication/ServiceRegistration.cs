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
            services.AddTransient<LoggingDelegatingHandler>();

            services.AddHttpClient<BasicHttpClient>()
              .AddHttpMessageHandler<LoggingDelegatingHandler>();

            services.AddScoped<ICommunicationProvider>(sp => new CommunicationProvider(sp.GetService<BasicHttpClient>(), configuration, 
                sp.GetRequiredService<ILogger<CommunicationProvider>>()));
               
              
          
            return services;
        }
    }
}

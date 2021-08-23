using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MS.Logger
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddMSLogger(this IServiceCollection services, IConfiguration configuration)
        { 

            return services;
        }
    }
}

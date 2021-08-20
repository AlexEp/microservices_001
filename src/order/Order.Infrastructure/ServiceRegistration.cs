using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Order.Infrastructure.Communication;

namespace Order.Infrastructure
{
    public static class OrderServiceRegistration
    {
        public static IServiceCollection AddInfraAll(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfraLoggers(configuration);
            services.AddInfraTrace(configuration);
            //services.AddInfraMapper(configuration);
            //services.AddInfraData(configuration);
            services.AddInfraCache(configuration);
            services.AddInfraCommunication(configuration);

            return services;
        }

        //public static IServiceCollection AddInfraMapper(this IServiceCollection services, IConfiguration configuration)
        //{

        //    var mappingConfig = new MapperConfiguration(mc => {
        //        mc.AddProfile(new MappingProfile());
        //    });
        //    IMapper mapper = mappingConfig.CreateMapper();
        //    services.AddSingleton(mapper);

        //    return services;
        //}

        //public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<InventoryContext>(options =>
        //              options.UseSqlServer(configuration.GetConnectionString("InventoryConnectionString")));

        //    services.AddScoped<IProductRepository, ProductRepository>();

        //    return services;
        //}

        public static IServiceCollection AddInfraLoggers(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: add loggers
            return services;
        }

        public static IServiceCollection AddInfraCommunication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICommunicationProvider,CommunicationProvider>();
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddScoped<IInventoryService>(sp => new InventoryService(configuration["ApiSettings:InventoryUrl"], sp.GetService<ICommunicationProvider>()));


            return services;
        }
        public static IServiceCollection AddInfraTrace(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: add Trace
            return services;
        }
        public static IServiceCollection AddInfraCache(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: add Trace
            return services;
        }


    }
}

using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using MS.Communication;
using MicrosoftLogging = Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using MS.Logger;

namespace Order.Infrastructure
{
    public static class OrderServiceRegistration
    {
        public static IServiceCollection AddInfraAll(this IServiceCollection services, IConfiguration configuration, MicrosoftLogging.ILogger logger)
        {
            services.AddInfraTrace(configuration);
            //services.AddInfraMapper(configuration);
            //services.AddInfraData(configuration);
            services.AddInfraCache(configuration);
            services.AddInfraCommunication(configuration, logger);

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

        public static IHostBuilder UseMSExtentions(this IHostBuilder host)
        {
            host.UseSerilog(SeriLogger.Configure);
            return host;
        }

        public static IServiceCollection AddInfraCommunication(this IServiceCollection services, IConfiguration configuration, MicrosoftLogging.ILogger logger)
        {

            services.AddCommunicationProvider(configuration, logger); //Add general Communication provider from  MS.Communication
      
            //Add services proxies
            services.AddScoped<IInventoryService>(sp => new InventoryService(configuration["ApiSettings:InventoryUrl"], sp.GetService<ICommunicationProvider>(), configuration));

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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Inventory.Infrastructure.Data.Context;
using Inventory.Infrastructure.Data.Repositories;
using AutoMapper;
using Inventory.Infrastructure.Mappings;

namespace Inventory.Infrastructure.Data
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfraAll(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfraLoggers(configuration);
            services.AddInfraTrace(configuration);
            services.AddInfraMapper(configuration);
            services.AddInfraData(configuration);
            services.AddInfraCache(configuration);
            services.AddInfraCommunication(configuration);

            return services;
        }

        public static IServiceCollection AddInfraMapper(this IServiceCollection services, IConfiguration configuration) {

            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InventoryContext>(options =>
                      options.UseSqlServer(configuration.GetConnectionString("InventoryConnectionString")));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddInfraLoggers(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: add loggers
            return services;
        }

        public static IServiceCollection AddInfraCommunication(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: add Connection
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

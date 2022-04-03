using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopbridge_base.Core.Implementations;
using Shopbridge_base.Core.Interfaces;
using Shopbridge_base.Data;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Modules.Implementation;
using Shopbridge_base.Domain.Modules.Interfaces;
using Shopbridge_base.Domain.Services.Interfaces;
using Shopbridge_base.Filters;
using System;

namespace Shopbridge_base.Domain.Services.Implementations
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddShoppingBridgeCore(this IServiceCollection services, Action<DbContextOptionsBuilder> dbConfiguration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (dbConfiguration == null) throw new ArgumentNullException(nameof(dbConfiguration));

            ConfigureDotNetCoreServices(services);

            ConfigureShoppingServices(services, dbConfiguration);

            ConfigureLocalServices(services);

            return services;
        }

        public static IServiceCollection ConfigureLocalServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductModule, ProductModule>();
            return services;
        }
        private static void ConfigureDotNetCoreServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllers(config =>
            {
                config.Filters.Add<LogActionFilter>();
            });
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void ConfigureShoppingServices(IServiceCollection services, Action<DbContextOptionsBuilder> dbConfiguration)
        {
            services.AddDbContextPool<Shopbridge_Context>(dbConfiguration);
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<Shopbridge_Context, Shopbridge_Context>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IEFService<>), typeof(EFService<>));
            //register any other services which is related to other microservice
        }
    }
}

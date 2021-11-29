using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ModShop.Infrustracture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustracture(this IServiceCollection services, IConfiguration configuration)
        {


            //services.AddHangfire(config =>
            //{
            //    config.UseSqlServerStorage(configuration.GetConnectionString("WebScraperDb"));

            //});
            //services.AddHttpClient();

            return services;
        }
    }
}

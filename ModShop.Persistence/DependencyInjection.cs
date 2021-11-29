using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ModShop.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ModShop.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConsole((options) => { })
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information);
            });
            var connection = configuration.GetConnectionString("MyConnectionString");
            services.AddDbContext<AppDb>(options =>
                           options.UseSqlServer(connection).UseLoggerFactory(loggerFactory),
                ServiceLifetime.Transient);

            services.AddTransient<IAppDb>(provider => provider.GetService<AppDb>());

            return services;
        }
    }
}

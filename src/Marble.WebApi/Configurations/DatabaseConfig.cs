using System;
using Marble.Infrastructure.Identity.Context;
using Marble.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marble.WebApi.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            services.AddDbContext<AuthDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));
            
            services.AddDbContext<MarbleDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly(typeof(MarbleDbContext).Assembly.FullName)));
            
            services.AddDbContext<EventStoreContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    assembly =>
                        assembly.MigrationsAssembly(typeof(EventStoreContext).Assembly.FullName)));
        }
        
        public static IApplicationBuilder UseCustomizedDatabase(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            serviceScope.ServiceProvider.InitDb<AuthDbContext>();
            serviceScope.ServiceProvider.InitDb<MarbleDbContext>();
            serviceScope.ServiceProvider.InitDb<EventStoreContext>();
            return app;
        }

        private static void InitDb<T>(this IServiceProvider serviceProvider)
            where T : DbContext
        {
            var context = serviceProvider.GetRequiredService<T>();
            context.Database.Migrate();
        }
    }
}
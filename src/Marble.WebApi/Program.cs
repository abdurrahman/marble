using System;
using System.Threading.Tasks;
using Marble.Infrastructure.Data;
using Marble.Infrastructure.Data.Context;
using Marble.Infrastructure.Identity;
using Marble.Infrastructure.Identity.Context;
using Marble.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Marble.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    // Seed identity mock data
                    var authDbContext = services.GetRequiredService<AuthDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await AuthDbSeed.SeedAsync(userManager, roleManager, authDbContext);
                    
                    // Seed product mock data
                    var marbleContext = services.GetRequiredService<MarbleDbContext>();
                    await DbContextSeed.SeedAsync(marbleContext);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
using Marble.Application.Mapping;
using Marble.Application.Services;
using Marble.Core.Bus;
using Marble.Core.Data;
using Marble.Core.Events;
using Marble.Core.Notifications;
using Marble.Core.ObjectMapper;
using Marble.Domain.CommandHandlers;
using Marble.Domain.Commands;
using Marble.Domain.Events;
using Marble.Domain.Repositories;
using Marble.Infrastructure.Data.EventSourcing;
using Marble.Infrastructure.Data.Repositories;
using Marble.Infrastructure.Identity.Configurations;
using Marble.Infrastructure.Identity.Models;
using Marble.Infrastructure.Identity.UoW;
using Marble.WebApi.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marble.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // Setting DbContexts
            services.AddDatabaseConfiguration(Configuration);
            
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            services.Configure<JwtConfig>(Configuration.GetSection(nameof(JwtConfig)));
            
            // ASP.NET Identity Settings & JWT
            services.AddIdentityConfiguration(Configuration);
            
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));
            
            // ----- Health check -----
            services.AddCustomizedHealthCheck(Configuration, _env);
            
            RegisterServices(services);
           
            // Swagger Config
            services.AddSwaggerConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomizedDatabase(_env);
            
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
                HealthCheckConfig.UseCustomizedHealthCheck(endpoints, _env);
            });
            
            app.UseSwaggerSetup();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            
            services.AddSingleton<IObjectMapper, MapsterObjectMapper>();
            
            // Application
            services.AddScoped<IProductService, ProductService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<ProductCreatedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductUpdatedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<ProductRemovedEvent>, ProductEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateNewProductCommand, bool>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, bool>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProductCommand, bool>, ProductCommandHandler>();
            
            // Infra - Data
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            
            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
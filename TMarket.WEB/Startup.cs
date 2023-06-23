using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using System;
using System.Reflection;
using TMarket.Application.Services.Abstract;
using TMarket.Application.Services.Concrete;
using TMarket.Persistence;
using TMarket.Persistence.DapperRepo.Abstract;
using TMarket.Persistence.DapperRepo.Concrete;
using TMarket.Persistence.Repositories.Abstract;
using TMarket.Persistence.Repositories.Concrete;
using TMarket.Persistence.UnitOfWork;
using TMarket.WEB.Helpers.CustomMiddlewares;
using TMarket.WEB.Helpers.Filters;

namespace TMarket.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MarketDbContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();


            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IService)))
                    .Where(c => typeof(IService).IsAssignableFrom(c))
                    .AsPublicImplementedInterfaces();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR((typeof(Startup)));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
            .AddFluentValidation(mvcConfig =>
                mvcConfig.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<IProductProcessor, ProductProccesor>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TMarket API", Version = "v1" });
            });

            // Hangfire
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<MarketDbContext>();
                dbContext.Database.Migrate();
                dbContext.SeedData();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<ErrorLoggingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TMarket API V1");
            });

            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

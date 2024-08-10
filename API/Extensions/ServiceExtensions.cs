using Domain.Repositories;
using Services.Contracts;
using Services;
using Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using API.Validators;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        { 
           services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) 
            => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services)
            => services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<RepositoryContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Persistence")));

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<ProductForCreateValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductForUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<StoreForCreateValidator>();
            services.AddValidatorsFromAssemblyContaining<StoreForUpdateValidator>();
            services.AddValidatorsFromAssemblyContaining<StockItemForCreateValidator>();
            services.AddValidatorsFromAssemblyContaining<StockItemForUpdateValidator>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
            => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();
                
                string[] xmlNames = { $"{Assembly.GetExecutingAssembly().GetName().Name}.xml", "Shared.xml" };

                foreach (string xml in xmlNames)
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
                    if (File.Exists(xmlPath))
                        c.IncludeXmlComments(xmlPath);
                }

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Loja Estoque API",
                    Version = "v1",
                    Description = "API de controle de estoque para lojas"
                });
            });
        }
    }
}

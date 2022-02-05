using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetBankaGold.Services.Core.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Enums;
using Tura.AddressBook.Infrastructures.Extensions;
using Tura.AddressBook.Interfaces.Repositories;
using Tura.AddressBook.Repositories;
using Tura.AddressBook.Services;
using Tura.AddressBook.Services.Interfaces;
using Tura.AddressBook.Services.Interfaces.Repositories;
using Tura.AddressBook.Services.Interfaces.Services;
using Tura.AddressBook.Services.Validation;

namespace Tura.AddressBook.Api
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

            services.AddDbContext<AddressBookContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("AddressBookDbConnection")));


            services.AddControllers(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest)
             .AddNewtonsoftJson(opt =>
             {
                 opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             });

            var types = typeof(IBaseValidator).Assembly.GetTypes().Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(x => x.Name == typeof(IBaseValidator).Name)).ToList();

            services.AddControllers(options =>
            {
                options.MaxIAsyncEnumerableBufferLimit = 16000;
                //options.Filters.Add(new ErrorHandlingFilter());

            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            })
            .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });


            services.ConfigureAPIs(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tura.AddressBook.Api", Version = "v1" });
            });


            services.AddTransient<IValidatorInterceptor, ValidationHandling>();
            //Repos
            services.AddScoped<IPersonalContactRepository, PersonalContactRepository>();
            services.AddScoped<IPersonalRepository, PersonalRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IRabbitMQApiRepository, RabbitMQApiRepository>(); 

            //Service
            services.AddScoped<IPersonalContactService, PersonalContactService>();
            services.AddScoped<IPersonalService, PersonalService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddMvc().AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
                //fv.RegisterValidatorsFromAssemblyContaining<PersonalContactValidator>();

                foreach (var type in types)
                {
                    fv.RegisterValidatorsFromAssemblyContaining(type);
                }
            }); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tura.AddressBook.Api v1"));
            }

            app.ConfigureExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            
        }
    }


    public static class ConfigurationExtensions
    {
        /// <summary></summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureAPIs(this IServiceCollection services, IConfiguration configuration)
        {
            Func<string, string> correctURL = delegate (string address)
            {
                return address.EndsWith("/") ? address : $"{address}/";
            };

            services.AddHttpClient(HttpClients.RabbitMQAPI, (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(correctURL(configuration["RabbitMQApiUrl"]));
            });


            return services;
        }
    }
}

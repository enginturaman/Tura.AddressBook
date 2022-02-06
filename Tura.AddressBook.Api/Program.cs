using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore;

namespace Tura.AddressBook.Api
{
    public class Program
    {
        private static readonly string ExecutingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CreateLoggerFromSettings(configuration);
            try
            {
                Log.Information($"Configuring host for {ExecutingAssemblyName}.");
                var webHost = BuildWebHost(configuration, args);

                Log.Information($"Building host for {ExecutingAssemblyName}.");
                var configuredHost = webHost.Build();

                Log.Information($"Running host for {ExecutingAssemblyName}.");
                configuredHost.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Program {ExecutingAssemblyName} terminated unexpectedly.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseSerilog()
                .UseDefaultServiceProvider(options =>
                 options.ValidateScopes = false);

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
        }

        private static Serilog.ILogger CreateLoggerFromSettings(IConfiguration configuration) =>
            new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("Source", ExecutingAssemblyName)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)           
                .CreateLogger();
    }
}

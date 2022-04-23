using HelperLibrary.StringFormatHelpers;
using HelperLibrary.VaultHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PSP_Reversi_MM_Winforms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger.Information("Application Starting");

            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ServiceCollection(services);
                    using ServiceProvider serviceProvider = services.BuildServiceProvider();
                })
                .Build();
        }

        private static void ServiceCollection(IServiceCollection services)
        {
            services.AddSingleton<IFileParser, FileParser>();
            services.AddSingleton<IVaultDataService, VaultDataService>();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.json.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}

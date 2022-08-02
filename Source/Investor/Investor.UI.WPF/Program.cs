using Investor.UI.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Investor.UI.WPF
{
    public class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // Configure and build the host at startup..

            var builder = Host.CreateDefaultBuilder(args)
                              .ConfigureServices(ConfigureServices);

            using var host = builder.Build();
            App app = host.Services.GetRequiredService<App>();

            host.Start();
            app.Run();
            host.WaitForShutdown();
        }

        /// <summary>
        /// Services configuration.
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            // Application Entry.
            services.AddSingleton<App>();

            // Application Services.
            services.AddApplicationServices()
                .AddApiServer(hostContext.Configuration["ApiServerAddress"])
                .AddUI<UIService>();


            // TODO: Study the pros & cons of adding MainViewModel as a service.
            //services.AddSingleton<MainViewModel>();
        }

    }
}

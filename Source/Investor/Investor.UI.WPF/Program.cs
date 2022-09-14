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
            var wpf = host.Services.GetRequiredService<IWpfService>();

            host.Start();
            wpf.RunApplication();
            host.WaitForShutdown();

            // IMPORTANT: Mark classes not targeted for inheritance as sealed.
        }

        /// <summary>
        /// Services configuration.
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            // Wpf Service (App Entry).
            services.AddSingleton<IWpfService, WpfService>();

            // Application Services.
            services.AddApplicationServices()
                .AddApiServer(hostContext.Configuration["ApiServerAddress"])
                .AddUI<UIService>();
        }

    }
}

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
            var app = host.Services.GetRequiredService<IApplicationEntry>();

            host.Start();
            app.RunApplication();
            host.WaitForShutdown();

            // IMPORTANT: Mark classes not targeted for inheritance as sealed.
        }

        /// <summary>
        /// Services configuration.
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            // Application Entry.
            services.AddSingleton<IApplicationEntry, ApplicationEntry>();

            // Application Services.
            services.AddApplicationServices()
                .AddApiServer(hostContext.Configuration["ApiServerAddress"] // TODO: Use dynamic configuration binding to count for runtime configuration editing.
                    ?? throw new InvalidOperationException("Couldn't resolve api server address from configuration providers."))
                .AddUI<UIService>();
        }

    }
}

using InvestorAPI.Core;
using InvestorAPI.JsonConverters;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI
{
    public class Program
    {

        #region Application Entry

        public static void Main(string[] args)
        {
            // Initialize the application builder.
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder);


            // Build the web application.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            ConfigurePipeline(app);


            // Run the applicaion.
            app.Run();
        }

        #endregion

        #region Configuration

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // TODO: Use CancellationToken in API endpoints.
            // TODO: Try use SkipWhile for pagination.

            builder.Services
                .AddControllers(options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new EnumJsonConverter()));


            // Add core services.
            builder.Services
                .AddCoreServices()
                .AddSqlServerDb(builder.Configuration.GetConnectionString("DefaultConnection"));
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
        }

        #endregion

    }
}
using InvestorAPI.Data;
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
            // TODO: Study the possibility of changing the way that MVC formatters handle errors.

            builder.Services
                .AddControllers(options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new EnumJsonConverter()));


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                sqlOptions.CommandTimeout(60)));


            builder.Services.AddAutoMapper(typeof(Program));


            // Add data access repositories.
            builder.Services.AddApplicationRepositories();
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
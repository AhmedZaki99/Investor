using InvestorAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI
{
    public class Program
    {
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


        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options =>
                options.SuppressAsyncSuffixInActionNames = false);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IBrandStore, BrandStore>();
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
        }

    }
}
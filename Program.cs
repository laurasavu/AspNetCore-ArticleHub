using Microsoft.EntityFrameworkCore;
using Project.DTO;

namespace Project
    
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Enable CORS to allow communication with the frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.AllowAnyMethod()
                          .AllowAnyHeader();
                          
                });
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            var app = builder.Build();

            // Apply the CORS policy
            app.UseCors("AllowFrontend");

            app.UseRouting();
            app.MapControllers();
            app.UseAuthorization();
            app.Run();
        }
    }
}

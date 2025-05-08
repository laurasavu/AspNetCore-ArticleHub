using Microsoft.EntityFrameworkCore;
using Project.DTO;
using Microsoft.Extensions.FileProviders;

namespace Project
    
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            

            builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
           

            var app = builder.Build(); 
            //
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            });
              app.UseRouting();
          
           
            app.MapControllers();
          
            
            
            app.Run();
        }
    }
}

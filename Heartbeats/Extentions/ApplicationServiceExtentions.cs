using DAL.Data;
using Heartbeats.Media;
using Microsoft.EntityFrameworkCore;

namespace Heartbeats.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<MediaService>();

            return services;
        }
    }
}

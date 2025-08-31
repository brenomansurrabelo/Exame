using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Exame.Infrastructure
{
    public static class Initializer
    {
        public static void AddDbContext(this IServiceCollection services, string? connection)
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(connection, options => options.MigrationsAssembly("Exame.Infrastructure")));

            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IUoW, UoW>();
        }
    }
}

using Exame.Domain;
using Exame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Exame.Services
{
    public static class Initializer
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}

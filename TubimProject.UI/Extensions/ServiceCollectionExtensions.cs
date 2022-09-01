using Microsoft.EntityFrameworkCore;

namespace TubimProject.UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddPersistenceContexts(configuration); 
        }
        private static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Infrastructure.Repositories.ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LogConnection"),b=>b.MigrationsAssembly("TubimProject.UI")));
            services.AddDbContext<Infrastructure.Repositories.KurumlarContext>(options => options.UseSqlServer(configuration.GetConnectionString("KurumlarConnection"), b => b.MigrationsAssembly("TubimProject.UI")));
        }
    }

}

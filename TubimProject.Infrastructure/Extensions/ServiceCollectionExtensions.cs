using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Cache;
using TubimProject.Application.Interfaces.Context;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.KurumlarKodTables;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;
using TubimProject.Application.Interfaces.Repositories.Modules.Notification;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayDetayModule;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;
using TubimProject.Application.Interfaces.Repositories.Modules.SucTanimModule;
using TubimProject.Application.Interfaces.Repositories.Modules.SupheliModule;
using TubimProject.Application.Interfaces.SignalR;
using TubimProject.Infrastructure.Cache;
using TubimProject.Infrastructure.Configuration;
using TubimProject.Infrastructure.Repositories;
using TubimProject.Infrastructure.Repositories.Modules.KurumlarKodTables;
using TubimProject.Infrastructure.Repositories.Modules.LogModule;
using TubimProject.Infrastructure.Repositories.Modules.MaddeModule;
using TubimProject.Infrastructure.Repositories.Modules.NotificationModule;
using TubimProject.Infrastructure.Repositories.Modules.OlayDetayModule;
using TubimProject.Infrastructure.Repositories.Modules.OlayModule;
using TubimProject.Infrastructure.Repositories.Modules.SucTanimModule;
using TubimProject.Infrastructure.Repositories.Modules.SupheliModule;
using TubimProject.Infrastructure.SignalR;

namespace TubimProject.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IKurumlarContext, KurumlarContext>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services
               .ConfigureAudit(configuration)
               .AddMvc(options =>
               {
                   options.AddAudit();
                   options.EnableEndpointRouting = false;
               });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddScoped(typeof(IBaseGenericRepo<>), typeof(BaseGenericRepo<>));
            services.AddScoped<ISeriLogService, SerilogService>();
            services.AddScoped<ICrashReportService, CrashReportService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOlayService, OlayService>();
            services.AddScoped<IOlayDetayService, OlayDetayService>();
            services.AddScoped<IMaddeService, MaddeService>();
            services.AddScoped<ISucTanimService, SucTanimService>();
            services.AddScoped<IMaddeTurleriService, MaddeTurleriService>();
            services.AddScoped<IKurumlarService, KurumlarService>();
            services.AddScoped<ISupheliService, SupheliService>();
            #endregion Repositories


            #region Caching
            services.AddScoped<ICacheService, CacheService>();
            #endregion


       
        }
    }

}

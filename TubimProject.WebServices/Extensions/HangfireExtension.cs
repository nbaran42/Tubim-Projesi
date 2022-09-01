using Hangfire;
using Hangfire.SqlServer;
using TubimProject.Application.Interfaces.Job;

namespace TubimProject.WebServices.Extensions
{
    public static class HangfireExtension
    {
        public static JobConfiguration AddJobManager(this IServiceCollection services, IConfiguration onfiguration)
        {


            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(onfiguration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));
            services.AddHangfireServer();
            services.AddScoped<RecurringJobManager>();

            return new JobConfiguration(services);
        }

        public static IApplicationBuilder StartRecurringJobs(this IApplicationBuilder app)
        {
            var manager = app.ApplicationServices.CreateScope().ServiceProvider.GetService<IRecurringJonManager>();
            manager.Start();
            return app;
        }
    }


    public class JobConfiguration
    {
        private readonly IServiceCollection services;

        internal JobConfiguration(IServiceCollection services)
        {
            this.services = services;
        }

        public JobConfiguration AddrecurringJob<TJob>() where TJob : IRecurringJob
        {
            services.AddScoped(typeof(IRecurringJob), typeof(TJob));
            return this;
        }
    }
}

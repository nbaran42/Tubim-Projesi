using Audit.Core;
using Audit.EntityFramework;
using Audit.Mvc;
using Audit.SqlServer.Providers;
using Audit.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TubimProject.Infrastructure.Repositories;

namespace TubimProject.Infrastructure.Configuration
{
    public static class AuditConfiguration
    {
        private const string CorrelationIdField = "CorrelationId";

        /// <summary>
        /// Add the global audit filter to the MVC pipeline
        /// </summary>
        public static MvcOptions AddAudit(this MvcOptions mvcOptions)
        {
            // Configure the global Action Filter
            mvcOptions.AddAuditFilter(a => a
                    .LogAllActions()
                    .WithEventType("MVC:{verb}:{controller}:{action}")
                    .IncludeModelState()
                    .IncludeRequestBody()
                    .IncludeHeaders(true)
                    .IncludeResponseHeaders(true)
                    .IncludeResponseBody());
            return mvcOptions;
        }

        /// <summary>
        /// Global Audit configuration
        /// </summary>
        public static IServiceCollection ConfigureAudit(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            Audit.Core.Configuration.Setup()
             .UseSqlServer(sql => sql
                 .ConnectionString(configuration.GetConnectionString("LogConnection"))
                 .TableName("UT_AUDIT")
                 //.TableName(ev => ev is AuditEventEntityFramework ? "UT_AUDIT" : "UT_SELECT") 
                 .IdColumnName("Id")
                 .JsonColumnName("Data")
                 .CustomColumn("AuditDate", e => DateTime.Now)


                 .Schema("dbo"));

            // Entity framework audit output configuration
            Audit.EntityFramework.Configuration.Setup()
                .ForContext<ApplicationDbContext>(_ => _
                    .AuditEventType("EF:{context}"))
                .UseOptOut();

            return serviceCollection;
        }

        public static void UseAuditMiddleware(this IApplicationBuilder app)
        {
            // Configure the Middleware
            app.UseAuditMiddleware(_ => _
                .FilterByRequest(r => !r.Path.Value.EndsWith("favicon.ico"))
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseBody()
                .WithEventType("HTTP:{verb}:{url}"));
        }

        /// <summary>
        /// Add a RequestId so the audit events can be grouped per request
        /// </summary>
        public static void UseAuditCorrelationId(this IApplicationBuilder app, IHttpContextAccessor ctxAccesor)
        {

            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
             {
                 var httpContext = ctxAccesor.HttpContext;
                 scope.Event.CustomFields[CorrelationIdField] = httpContext.TraceIdentifier;
             });
        }
    }
}













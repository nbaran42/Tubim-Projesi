global using TubimProject.Application.DTOs.Identity.Roles;
global using TubimProject.UI.Extensions;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using TubimProject.UI.Services;
using TubimProject.UI.Middlewares;
using TubimProject.Application.Interfaces.Shared;
using TubimProject.Application.Extensions;
using TubimProject.Infrastructure.Extensions;
using TubimProject.Infrastructure.Configuration;
using Serilog;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using TubimProject.UI.Jobs.Implementations;
using TubimProject.Application.Interfaces.Job;

var builder = WebApplication.CreateBuilder(args);


// Serilog
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));


var contextAccessor = new HttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver=new DefaultContractResolver(); });
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = "Cookies";
    //      opts.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", opts =>
{
    opts.LoginPath = "/Login/Index";
    opts.AccessDeniedPath = "/Home/AccessDenied";
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IApiResourceHttpClient, ApiResourceHttpClient>();
builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddScoped<IRecurringJonManager, TubimProject.UI.Services.RecurringJobManager>();
builder.Services.AddKendo();
builder.Services.AddLocalization();
builder.Services.AddPersistenceContexts(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddJobManager(builder.Configuration)
        .AddrecurringJob<SetOlayCacheJob>();
builder.Services.AddMemoryCache();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("tr-TR");

    var cultures = new CultureInfo[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR")
    };

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
var app = builder.Build();



app.UseRequestLocalization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuditCorrelationId(contextAccessor);
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});
IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
app.UseHangfireDashboard("/tubimjobs", new DashboardOptions
{
    Authorization = new[]
{
    new HangfireCustomBasicAuthenticationFilter
    {
         User = _configuration.GetSection("HangfireSettings:UserName").Value,
         Pass = _configuration.GetSection("HangfireSettings:Password").Value
    }
    },
    DashboardTitle="Tubim Project Recurring Jobs by Necip BARAN"
});
app.UseHangfireServer(new BackgroundJobServerOptions());
app.StartRecurringJobs();
app.UseSerilogRequestLogging();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

using Hangfire;
using HangfireBasicAuthenticationFilter;
using MediatR;
using Serilog;
using System.Reflection;
using TubimProject.Application.Extensions;
using TubimProject.Application.Interfaces.Job;
using TubimProject.Application.Interfaces.Shared;
using TubimProject.Application.Interfaces.WebServices;
using TubimProject.Infrastructure.Extensions;
using TubimProject.UI.Services;
using TubimProject.WebServices.Extensions;
using TubimProject.WebServices.Jobs.Implementations;
using TubimProject.WebServices.ServiceCollections;
using TubimProject.WebServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddScoped<IServiceContext, ServiceContext>();
builder.Services.AddPersistenceContexts(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddJobManager(builder.Configuration)
        .AddrecurringJob<JandarmaServiceJob>();

builder.Services.AddScoped<IRecurringJonManager, TubimProject.WebServices.Services.RecurringJobManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
app.UseHangfireDashboard("/servicejobs", new DashboardOptions
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
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

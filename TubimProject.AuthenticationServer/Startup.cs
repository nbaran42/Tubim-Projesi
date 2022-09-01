// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using TubimProject.AuthenticationServer.Data;
using TubimProject.AuthenticationServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using System;

namespace TubimProject.AuthenticationServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalApiAuthentication();

            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit=true;
                opt.Password.RequiredLength=5;
                opt.Password.RequireNonAlphanumeric=false;
                opt.Password.RequireUppercase=true;
                opt.Password.RequireLowercase=true;



                opt.Lockout.MaxFailedAccessAttempts=3;
                opt.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
                opt.Lockout.AllowedForNewUsers=true;

                opt.User.RequireUniqueEmail=true;


                opt.SignIn.RequireConfirmedPhoneNumber=false;
                opt.SignIn.RequireConfirmedEmail=false;


            });



            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            }).AddDeveloperSigningCredential()
                .AddConfigurationStore(conf =>
                    {
                        conf.ConfigureDbContext=c => c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOpts => sqlOpts.MigrationsAssembly(assemblyName));
                    })
                .AddOperationalStore(conf =>
                    {
                        conf.ConfigureDbContext=c => c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOpts => sqlOpts.MigrationsAssembly(assemblyName));
                    })

                .AddAspNetIdentity<ApplicationUser>();

            // not recommended for production - you need to store your key material somewhere secure
          builder.AddDeveloperSigningCredential();

            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
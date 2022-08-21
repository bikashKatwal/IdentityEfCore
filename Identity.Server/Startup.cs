using Identity.Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Identity.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<AppDbContext>(config =>
            {
                //config.UseSqlServer(connectionString);
                config.UseInMemoryDatabase("MemoryDb");
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Auth/Login";
            });

            services.AddIdentityServer()
                 .AddAspNetIdentity<IdentityUser>()
                 //  .AddConfigurationStore(options =>
                 //  {
                 //      options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                 //          sql => sql.MigrationsAssembly(migrationsAssembly));
                 //  })
                 //.AddOperationalStore(options =>
                 //{
                 //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                 //        sql => sql.MigrationsAssembly(migrationsAssembly));
                 //})
                 .AddInMemoryIdentityResources(Configuation.IdentityResources)
                 .AddInMemoryApiResources(Configuation.ApiResources)
                 .AddInMemoryApiScopes(Configuation.ApiScopes)
                 .AddInMemoryClients(Configuation.Clients)
                 .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

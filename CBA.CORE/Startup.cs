using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.Data;
using CBA.Data.Implementations;
using CBA.Data.Interfaces;
using CBA.DATA;
using CBA.DATA.Implementations;
using CBA.DATA.Interfaces;
using CBA.Services.Implementations;
using CBA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

//using CBA.Data;


namespace CBA.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            //config => {
            //    var policy = new AuthorizationPolicyBuilder()
            //                    .RequireAuthenticatedUser()
            //                    .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //}

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>();
            services.AddTransient<AppUserSeedData>();
            services.AddTransient<IService, Service>();
            services.AddTransient<ICustomerDao, CustomerDao>();
            services.AddTransient<ICustomerAccountDao, CustomerAccountDao>();
            services.AddTransient<IGLAccountDao, GLAccountDao>();
            services.AddTransient<IGLCategoryDao, GLCategoryDao>();
            services.AddTransient<IAccountTypeManagementDao, AccountTypeManagementDao>();
            services.AddTransient<ITellerDao, TellerDao>();
            services.AddTransient<IBalanceSheetDao, BalanceSheetDao>();


            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppUserSeedData seeder)
        {
            if (env.IsDevelopment())
            {
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");

                app.UseDeveloperExceptionPage();
                seeder.SeedAdminUserAndRoles();
            }
            else
            {
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "Views/Client")),
                RequestPath = "/StaticFiles"
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using AutoMapper;
using E_commerce.Core;
using E_commerce.Core.Helper;
using E_commerce.Core.Interfaces.IRepositores;
using E_commerce.Core.Interfaces.IRepositories;
using E_commerce.Core.Interfaces.IServices;
using E_commerce.Core.Interfaces.IUnitOfWork;
using E_commerce.Core.Models;
using E_commerce.Core.Services;
using E_commerce.Core.Services.AllServices;
using E_commerce.Core.Settings;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Interfaces.IServices;
using E_commerce.Ef.Data;
using E_commerce.Ef.Repositores;
using E_commerce.Ef.Repositories;
using E_commerce.Ef.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Web
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
            services.Configure<MailSetting>(Configuration.GetSection("MailSetteng"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), m =>
                        m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddRazorPages();

            services.AddAutoMapper(typeof(MappingProfile));

            /// ADD UNIT OF WOR 
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // ADD SERVICES
            services.AddTransient<IAdminServices, AdminServices>();
            services.AddTransient<IBranchServices, BranchServices>();
            services.AddTransient<ICityServices, CityServices>();
            services.AddTransient<ICountryServices, CountryServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IDateServices, DateServices>();

            services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,
                CloseButton = true
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseNToastNotify();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}
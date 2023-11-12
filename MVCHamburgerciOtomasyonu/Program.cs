using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.DataAccess.Context;
using MVCHamburgerciOtomasyonu.DataAccess.Repositories;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.Describers;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using NToastNotify;
using System;

namespace MVCHamburgerciOtomasyonu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddAuthorization();
           
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.LoadServiceLayerExtension();
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews()
                  .AddNToastNotifyToastr(new ToastrOptions()
                  {
                      PositionClass = ToastPositions.TopRight,
                      TimeOut = 3000,
                  })
             .AddRazorRuntimeCompilation();


            builder.Services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            })
             .AddRoleManager<RoleManager<AppRole>>()
             .AddErrorDescriber<CustomIdentityErrorDescriber>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = new PathString("/Auth/Login");
                config.LogoutPath = new PathString("/Auth/Logout");
                config.Cookie = new CookieBuilder
                {
                    Name = "MVCHamburgerciOtomasyonu",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest 
                };
                config.SlidingExpiration = true;
                config.ExpireTimeSpan = TimeSpan.FromDays(7);
                config.AccessDeniedPath = new PathString("/Auth/AccessDenied");
            });

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Auth/Error404/");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
			app.UseSession();
			app.UseAuthentication();
			app.UseAuthorization();
			

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=AdminDashboard}/{id?}");

            app.Run();
        }
    }
}
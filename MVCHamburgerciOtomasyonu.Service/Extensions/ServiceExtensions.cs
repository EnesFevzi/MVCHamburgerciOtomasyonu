using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MVCHamburgerciOtomasyonu.Service.FluentValidations;
using MVCHamburgerciOtomasyonu.Service.Helpers.Images;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Service.Services.Concrete;
using System.Globalization;
using System.Reflection;

namespace MVCHamburgerciOtomasyonu.Service.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuSizeService, MenuSizeService>();
            services.AddScoped<IExtraService, ExtraService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddAutoMapper(assembly);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews()
                .AddFluentValidation(opt =>
                {
                    opt.RegisterValidatorsFromAssemblyContaining<MenuValidator>();
                    opt.DisableDataAnnotationsValidation = true;
                    opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
                });

            return services;


        }
    }
}

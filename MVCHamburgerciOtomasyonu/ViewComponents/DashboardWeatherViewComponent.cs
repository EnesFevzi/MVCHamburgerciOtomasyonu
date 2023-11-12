using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;

namespace MVCHamburgerciOtomasyonu.Web.ViewComponents
{
    public class DashboardWeatherViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public DashboardWeatherViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var weatherInfo = await _userService.GetWeatherInfo();
            return View(weatherInfo);
        }
    }
}

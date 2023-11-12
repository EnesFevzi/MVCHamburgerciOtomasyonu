using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Users;

namespace MVCHamburgerciOtomasyonu.Web.ViewComponents
{
    public class DashboardBodyViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public DashboardBodyViewComponent(UserManager<AppUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser = await userManager.GetUserAsync(HttpContext.User);
            var map = mapper.Map<UserDto>(loggedInUser);

            var role = string.Join("", await userManager.GetRolesAsync(loggedInUser));
            map.Role = role;

            return View(map);
        }
    }
}

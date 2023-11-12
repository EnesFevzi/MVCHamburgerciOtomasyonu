using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using System.Security.Claims;

namespace MVCHamburgerciOtomasyonu.Service.Services.Concrete
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IRepository<MenuSize> _menusizeRepository;
        private readonly IRepository<Extra> _extraRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly UserManager<AppUser> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public DashboardService(IRepository<Menu> menuRepository, IHttpContextAccessor httpContextAccessor, IRepository<MenuSize> menusizeRepository, IRepository<Extra> extraRepository, IRepository<Order> orderRepository, UserManager<AppUser> userRepository)
        {
            _menuRepository = menuRepository;
            _menusizeRepository = menusizeRepository;
            _extraRepository = extraRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task<int> GetTotalExtraCount()
        {
            var extra = await _extraRepository.CountAsync(x => !x.IsDeleted);
            return extra == 0 ? 0 : extra;
        }

        public async Task<int> GetTotalMenuCount()
        {
            var menu = await _menuRepository.CountAsync(x => !x.IsDeleted);
            return menu == 0 ? 0 : menu;
        }

        public async Task<int> GetTotalMenuSizeCount()
        {
            var menuSize = await _menusizeRepository.CountAsync(x => !x.IsDeleted);
            return menuSize == 0 ? 0 : menuSize;
        }

        public async Task<List<int>> GetYearlyOrderCounts()
        {
            var orders = await _orderRepository.GetAllAsync(x => !x.IsDeleted);

            var startDate = DateTime.Now.Date;
            startDate = new DateTime(startDate.Year, 1, 1);

            List<int> datas = new();

            for (int i = 1; i <= 12; i++)
            {
                var startedDate = new DateTime(startDate.Year, i, 1);
                var endedDate = startedDate.AddMonths(1);
                var data = orders.Where(x => x.CreatedDate >= startedDate && x.CreatedDate < endedDate).Count();
                datas.Add(data);
            }
            return datas;
        }
        public async Task<List<int>> GetYearlyUserOrderCounts()
        {
            var user = _user.GetLoggedInUserId();
            var orders = await _orderRepository.GetAllAsync(x => x.UserID == user && !x.IsDeleted);

            var startDate = DateTime.Now.Date;
            startDate = new DateTime(startDate.Year, 1, 1);

            List<int> datas = new();

            for (int i = 1; i <= 12; i++)
            {
                var startedDate = new DateTime(startDate.Year, i, 1);
                var endedDate = startedDate.AddMonths(1);
                var data = orders.Where(x => x.CreatedDate >= startedDate && x.CreatedDate < endedDate).Count();
                datas.Add(data);
            }
            return datas;
        }

        public async Task<int> GetUserOrderCount()
        {
            var mail = _user.GetLoggedInEmail();
            var user = await _userRepository.FindByNameAsync(mail);
            if (user != null)
            {
                var orderCount = await _orderRepository.CountAsync(order => order.UserID == user.Id && !order.IsDeleted);
                return orderCount == 0 ? 0 : orderCount;
            }

            return 0;
        }

        public async Task<int> GetUserOrderCompletedCount()
        {
            var mail = _user.GetLoggedInEmail();
            var user = await _userRepository.FindByNameAsync(mail);
            if (user != null)
            {
                var orderCount = await _orderRepository.CountAsync(order => order.UserID == user.Id && order.Status == "Tamamlandı" && !order.IsDeleted);
                return orderCount == 0 ? 0 : orderCount;
            }

            return 0;
        }

        public async Task<int> GetUserOrderCancelCount()
        {
            var mail = _user.GetLoggedInEmail();
            var user = await _userRepository.FindByNameAsync(mail);
            if (user != null)
            {
                var orderCount = await _orderRepository.CountAsync(order => order.UserID == user.Id && order.Status == "İptal Edildi" && !order.IsDeleted);
                return orderCount == 0 ? 0 : orderCount;
            }

            return 0;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Entity.Enums;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;
using MVCHamburgerciOtomasyonu.Service.DTOs.Orders;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Helpers.Images;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Image> _ımageRepository;
        private readonly IRepository<Extra> _extraRepository;
        private readonly IRepository<Menu> _menuRepository;
        private readonly IRepository<MenuSize> _menuSizeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IRepository<Menu> menuRepository, IRepository<Extra> extraRepository, IRepository<Image> ımageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRepository<MenuSize> menuSizeRepository)
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
            _ımageRepository = ımageRepository;
            _extraRepository = extraRepository;
            _menuRepository = menuRepository;
            _menuSizeRepository = menuSizeRepository;
        }
        public async Task CreateOrderAsync(OrderAddDto orderAddDto)
        {
            var map = _mapper.Map<Order>(orderAddDto);
            if (orderAddDto.SelectedExtras != null)
            {
                foreach (var item in orderAddDto.SelectedExtras)
                {
                    var findExtra = await _extraRepository.GetByGuidAsync(item);
                    map.Extras.Add(findExtra);
                }
            }

            var selectedMenu = await _menuRepository.GetByGuidAsync(orderAddDto.MenuId);
            var selectedSize = await _menuSizeRepository.GetByGuidAsync(orderAddDto.MenuSizeId);
            var orderPrice = CalculateOrderPrice(selectedMenu, selectedSize, orderAddDto.Quantity, map.Extras);
            map.Price = orderPrice;
            var userID = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInEmail();
            map.UserID = userID;
            map.CreatedBy = userEmail;

            await _orderRepository.AddAsync(map);
        }

        public async Task<decimal> CalculateOrder(OrderAddDto orderAddDto)
        {
            var map = _mapper.Map<Order>(orderAddDto);
            foreach (var item in orderAddDto.SelectedExtras)
            {
                var findExtra = await _extraRepository.GetByGuidAsync(item);
                map.Extras.Add(findExtra);
            }


            var selectedMenu = await _menuRepository.GetByGuidAsync(orderAddDto.MenuId);
            var selectedSize = await _menuSizeRepository.GetByGuidAsync(orderAddDto.MenuSizeId);
            var orderPrice = CalculateOrderPrice(selectedMenu, selectedSize, orderAddDto.Quantity, map.Extras);
            map.Price = orderPrice;

            return map.Price;
        }
        public decimal CalculateOrderPrice(Menu selectedMenu, MenuSize selectedSize, int quantity, List<Extra> selectedExtras)
        {
            decimal menuPrice = selectedMenu.Price;
            decimal sizePrice = selectedSize.PriceModifier;
            decimal extrasPrice = selectedExtras.Sum(extra => extra.Price);


            decimal orderPrice = (menuPrice + sizePrice) * quantity + extrasPrice;

            return orderPrice;
        }
        public async Task<string> UpdateOrderAsync(OrderUpdateDto OrderUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _menuRepository.GetAsync(x => x.MenuID == OrderUpdateDto.MenuId);
            OrderUpdateDto.Menu = menu;
            var order = await _orderRepository.GetAsync(x => x.OrderID == OrderUpdateDto.OrderID && !x.IsDeleted, x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map(OrderUpdateDto, order);
            if (OrderUpdateDto.SelectedExtras != null)
            {
                foreach (var item in OrderUpdateDto.SelectedExtras)
                {
                    var findExtra = await _extraRepository.GetByGuidAsync(item);
                    if (findExtra != null && !map.Extras.Any(extra => extra.ExtraID == item))
                    {
                        map.Extras.Add(findExtra);
                    }
                }
            }
            var selectedMenu = await _menuRepository.GetByGuidAsync(OrderUpdateDto.MenuId);
            var selectedSize = await _menuSizeRepository.GetByGuidAsync(OrderUpdateDto.MenuSizeId);
            var orderPrice = CalculateOrderPrice(selectedMenu, selectedSize, OrderUpdateDto.Quantity, map.Extras);
            map.Price = orderPrice;
            order.ModifiedDate = DateTime.Now;
            order.ModifiedBy = userEmail;
            await _orderRepository.UpdateAsync(map);

            return "Sipariş";
        }

        public async Task<List<OrderDto>> GetAllOrdersDeletedAsync()
        {
            var menus = await _orderRepository.GetAllAsync(x => x.IsDeleted);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }
        public async Task<List<OrderDto>> GetUserOrdersNonDeletedAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var menus = await _orderRepository.GetAllAsync(o => o.UserID == userID && !o.IsDeleted && o.Status != "İptal Edildi", x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }
        public async Task<List<OrderDto>> GetUserOrdersDeletedAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var menus = await _orderRepository.GetAllAsync(o => o.UserID == userID && o.IsDeleted && o.Status == "İptal Edildi", x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }
        public async Task<List<OrderDto>> GetAllOrdersNonDeletedAsync()
        {
            var menus = await _orderRepository.GetAllAsync(x => !x.IsDeleted);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }


        public async Task<OrderDto> GetOrderWithUserNonDeletedAsync(Guid orderID)
        {
            var menu = await _orderRepository.GetAsync(x => !x.IsDeleted && x.OrderID == orderID, x => x.User, x => x.Extras);
            var map = _mapper.Map<OrderDto>(menu);
            return map;
        }

        public async Task<string> SafeDeleteOrderAsync(Guid orderID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _orderRepository.GetByGuidAsync(orderID);
            menu.IsDeleted = true;
            menu.DeletedDate = DateTime.Now;
            menu.DeletedBy = userEmail;
            await _orderRepository.UpdateAsync(menu);
            return "Menu";
        }

        public async Task<string> UndoDeleteOrderAsync(Guid orderID)
        {
            var menu = await _orderRepository.GetByGuidAsync(orderID);
            menu.IsDeleted = false;
            menu.DeletedDate = null;
            menu.DeletedBy = null;
            await _orderRepository.UpdateAsync(menu);

            return menu.Menu.Name;
        }

        public async Task<OrderDto> GetOrder(Guid orderID)
        {
            var menu = await _orderRepository.GetByGuidAsync(orderID);
            var map = _mapper.Map<OrderDto>(menu);
            return map;
        }

        public async Task<string> ChangeStatusCancel(Guid orderID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var order = await _orderRepository.GetAsync(x => x.OrderID == orderID && !x.IsDeleted, x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            order.ModifiedDate = DateTime.Now;
            order.ModifiedBy = userEmail;
            order.Status = "İptal Edildi";
            await _orderRepository.UpdateAsync(order);

            return order.Menu.Name;
        }

        public async Task<string> ChangeStatusPreparing(Guid orderID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var order = await _orderRepository.GetAsync(x => x.OrderID == orderID && !x.IsDeleted, x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            order.ModifiedDate = DateTime.Now;
            order.ModifiedBy = userEmail;
            order.Status = "Hazırlanıyor";
            await _orderRepository.UpdateAsync(order);

            return order.Menu.Name;
        }

        public async Task<string> ChangeStatusOntheRoad(Guid orderID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var order = await _orderRepository.GetAsync(x => x.OrderID == orderID && !x.IsDeleted, x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            order.ModifiedDate = DateTime.Now;
            order.ModifiedBy = userEmail;
            order.Status = "Yolda";
            await _orderRepository.UpdateAsync(order);

            return order.Menu.Name;
        }
        public async Task<string> ChangeStatusCompleted(Guid orderID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var order = await _orderRepository.GetAsync(x => x.OrderID == orderID && !x.IsDeleted, x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            order.ModifiedDate = DateTime.Now;
            order.ModifiedBy = userEmail;
            order.Status = "Tamamlandı";
            await _orderRepository.UpdateAsync(order);

            return order.Menu.Name;
        }

        public async Task<List<OrderDto>> GetCancelOrdersNonDeletedAsync()
        {
            var menus = await _orderRepository.GetAllAsync(x => !x.IsDeleted && x.Status == "İptal Edildi", x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }

        public async Task<List<OrderDto>> GetOnRoadOrdersNonDeletedAsync()
        {
            var menus = await _orderRepository.GetAllAsync(x => !x.IsDeleted && x.Status == "Yolda", x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }

        public async Task<List<OrderDto>> GetPreparingOrdersNonDeletedAsync()
        {
            var menus = await _orderRepository.GetAllAsync(x => !x.IsDeleted && x.Status == "Hazırlanıyor", x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }



        public async Task<List<OrderDto>> GetCompletedOrdersNonDeletedAsync()
        {
            var menus = await _orderRepository.GetAllAsync(x => !x.IsDeleted && x.Status == "Tamamlandı", x => x.User, x => x.MenuSize, x => x.Menu, x => x.Extras);
            var map = _mapper.Map<List<OrderDto>>(menus);
            return map;
        }


    }
}

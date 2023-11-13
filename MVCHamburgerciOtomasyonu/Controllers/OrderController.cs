using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Orders;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Service.Services.Concrete;
using MVCHamburgerciOtomasyonu.Web.Consts;
using MVCHamburgerciOtomasyonu.Web.ResultMessages;
using NToastNotify;


namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
   
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;
        private readonly IExtraService _extraService;
        private readonly IMenuSizeService _menuSizeService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IValidator<Order> _validator;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> userManager;

        public OrderController(IOrderService orderService, IExtraService extraService, IMenuService menuService, IMenuSizeService menuSizeService, IMapper mapper, IToastNotification toastNotification, IValidator<Order> validator, IUserService userService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _menuService = menuService;
            _menuSizeService = menuSizeService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _validator = validator;
            _extraService = extraService;
            _userService = userService;
            this.userManager = userManager;
        }

        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> AdminShowOrdersWithCancel()
        {
            var order =  await _orderService.GetCancelOrdersNonDeletedAsync();
            return View(order);
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> AdminShowOrdersWithOnRoad()
        {
            var order = await _orderService.GetOnRoadOrdersNonDeletedAsync();
            return View(order);
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> AdminShowOrdersWithPreparing()
        {
            var order = await _orderService.GetPreparingOrdersNonDeletedAsync();
            return View(order);
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> AdminShowOrdersWithCompleted()
        {
            var order = await _orderService.GetCompletedOrdersNonDeletedAsync();
            return View(order);
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}")]
        public async Task<IActionResult> AdminShowOrdersWithCame()
        {
            var order = await _orderService.GetCameOrdersNonDeletedAsync();
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatusPreparing(Guid orderId)
        {
             await _orderService.ChangeStatusPreparing(orderId);
            _toastNotification.AddSuccessToastMessage("Sipariş Durumu Hazırlanıyor Olarak Değiştirildi", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("AdminShowOrdersWithPreparing");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatusOnRoad(Guid orderId)
        {
            await _orderService.ChangeStatusOntheRoad(orderId);
            _toastNotification.AddSuccessToastMessage("Sipariş Durumu Yolda Olarak Değiştirildi", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("AdminShowOrdersWithOnRoad");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatusOnCancel(Guid orderId)
        {
            await _orderService.ChangeStatusCancel(orderId);
            _toastNotification.AddSuccessToastMessage("Sipariş Durumu İptal Olarak Değiştirildi", new ToastrOptions { Title = "İşlem Başarılı" });
            var user = await _userService.GetUserAsync();
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains(RoleConsts.Superadmin))
            {
                return RedirectToAction("AdminShowOrdersWithCancel");
            }
            if (roles.Contains(RoleConsts.User))
            {
                return RedirectToAction("UserOrder", "Order");
            }
            return View();
           
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStatusCompleted(Guid orderId)
        {
            await _orderService.ChangeStatusCompleted(orderId);
            _toastNotification.AddSuccessToastMessage("Sipariş Durumu Tamamlandı Olarak Değiştirildi", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("AdminShowOrdersWithCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> CalculateOrderPrice([FromBody] OrderAddDto orderAddDto)
        {
            var calculate = await _orderService.CalculateOrder(orderAddDto);

            return Ok(new { calculate });
        }
        [Authorize(Roles = $"{RoleConsts.User}")]
        public async Task<IActionResult> UserOrder()
        {
            var getUsersOrders = await _orderService.GetUserOrdersNonDeletedAsync();
            return View(getUsersOrders);
        }
        public async Task<IActionResult> DeleteUserOrder()
        {
            var getUsersOrders = await _orderService.GetUserOrdersDeletedAsync();
            return View(getUsersOrders);
        }
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.User}")]
        public async Task<IActionResult> Add()
        {
            var menus = await _menuService.GetAllMenusNonDeletedAsync();
            var menuSize = await _menuSizeService.GetAllMenuSizesNonDeletedAsync();
            var extras = await _extraService.GetAllExtrasNonDeletedAsync();
            return View(new OrderAddDto { Menus = menus, MenuSizes = menuSize, Extras = extras });
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.User}")]
        public async Task<IActionResult> Add(OrderAddDto orderAddDto)
        {
            var map = _mapper.Map<Order>(orderAddDto);
            var result = await _validator.ValidateAsync(map);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);


            }
            else
            {
                await _orderService.CreateOrderAsync(orderAddDto);
                _toastNotification.AddSuccessToastMessage("Sipariş Başarıyla Oluşturuldu", new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("UserOrder", "Order");
            }

            var menus = await _menuService.GetAllMenusNonDeletedAsync();
            var menuSize = await _menuSizeService.GetAllMenuSizesNonDeletedAsync();
            var extras = await _extraService.GetAllExtrasNonDeletedAsync();
            return View(new OrderAddDto { Menus = menus, MenuSizes = menuSize, Extras = extras });
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.User}")]
        public async Task<IActionResult> Update(Guid orderId)
        {
            var order = await _orderService.GetOrderWithUserNonDeletedAsync(orderId);
            var menus = await _menuService.GetAllMenusNonDeletedAsync();
            var menuSizes = await _menuSizeService.GetAllMenuSizesNonDeletedAsync();
            var extras = await _extraService.GetAllExtrasNonDeletedAsync();

            var orderEditDto = _mapper.Map<OrderUpdateDto>(order);

            orderEditDto.SelectedExtras = order.Extras.Select(extra => extra.ExtraID).ToList();
            orderEditDto.Menus = menus;
            orderEditDto.MenuSizes = menuSizes;
            orderEditDto.Extras = extras;

            return View(orderEditDto);
        }


        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.User}")]
        public async Task<IActionResult> Update(OrderUpdateDto ordeUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.UpdateOrderAsync(ordeUpdateDto);
                _toastNotification.AddSuccessToastMessage("Sipariş Başarıyla Güncellendi", new ToastrOptions { Title = "İşlem Başarılı" });

                return RedirectToAction("UserOrder", "Order");
            }
            else
            {
                var menus = await _menuService.GetAllMenusNonDeletedAsync();
                var menuSizes = await _menuSizeService.GetAllMenuSizesNonDeletedAsync();
                var extras = await _extraService.GetAllExtrasNonDeletedAsync();
                _toastNotification.AddErrorToastMessage("Sipariş Güncellenemedi", new ToastrOptions { Title = "İşlem Başarısız" });
                ordeUpdateDto.Menus = menus;
                ordeUpdateDto.MenuSizes = menuSizes;
                ordeUpdateDto.Extras = extras;
                return View(ordeUpdateDto);
            }


        }
        public async Task<IActionResult> Delete(Guid orderId)
        {
            var title = await _orderService.SafeDeleteOrderAsync(orderId);
            _toastNotification.AddSuccessToastMessage("Sipariş Silindi", new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("UserOrder", "Order");
        }

        public async Task<IActionResult> UndoDelete(Guid orderId)
        {
            var title = await _menuService.UndoDeleteMenuAsync(orderId);
            _toastNotification.AddSuccessToastMessage("Sipariş Geri Alındı", new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("UserOrder", "Order");
        }


    }

}

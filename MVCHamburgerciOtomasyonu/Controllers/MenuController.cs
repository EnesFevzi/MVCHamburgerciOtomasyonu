using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Web.Consts;
using MVCHamburgerciOtomasyonu.Web.ResultMessages;
using NToastNotify;

namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
	[Authorize]
	public class MenuController : Controller
	{
		private readonly IMenuService _menuService;
		private readonly IMenuSizeService _menuSizeService;
		private readonly IMapper _mapper;
		private readonly IToastNotification _toastNotification;
		private readonly IValidator<Menu> _validator;
		private readonly IValidator<MenuAddDto> _menuAddvalidator;

		public MenuController(IMenuService menuService, IMapper mapper, IToastNotification toastNotification, IValidator<Menu> validator, IValidator<MenuAddDto> menuAddvalidator, IMenuSizeService menuSizeService)
		{
			_menuService = menuService;
			_mapper = mapper;
			_toastNotification = toastNotification;
			_validator = validator;
			_menuAddvalidator = menuAddvalidator;
			_menuSizeService = menuSizeService;
		}
		
		public async Task<IActionResult> Index()
		{
			var menus = await _menuService.GetAllMenusWithImageNonDeletedAsync();
			return View(menus);
		}

        public async Task<IActionResult> DeletedMenu()
        {
            var articles = await _menuService.GetAllMenusWithImageDeletedAsync();
            return View(articles);
        }
        [HttpGet]
		public async Task<IActionResult> Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(MenuAddDto menuAddDto)
		{
			var map = _mapper.Map<Menu>(menuAddDto);
			var result = await _validator.ValidateAsync(map);
			var result2 = await _menuAddvalidator.ValidateAsync(menuAddDto);

			if (!result.IsValid)
			{
				result.AddToModelState(this.ModelState);
			

			}
			if (!result2.IsValid)
			{
                result2.AddToModelState(this.ModelState);
				
			}
			else
			{
                await _menuService.CreateMenuAsync(menuAddDto);
                _toastNotification.AddSuccessToastMessage(Messages.Menu.Add(menuAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Menu");
            }
           
            return View();


        }
        [HttpGet]
     
        public async Task<IActionResult> Update(Guid menuId)
        {
            var menu = await _menuService.GetMenuWithImageAndMenuSizeNonDeletedAsync(menuId);
            var menuUpdateDto = _mapper.Map<MenuUpdateDto>(menu);
            return View(menuUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MenuUpdateDto menuUpdateDto)
        {
            var map = _mapper.Map<Menu>(menuUpdateDto);
            var result = await _validator.ValidateAsync(map);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

            }
            else
            {
                var title = await _menuService.UpdateMenuAsync(menuUpdateDto);
                _toastNotification.AddSuccessToastMessage(Messages.Menu.Update(title), new ToastrOptions() { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Menu");
            }

            var menusizes = await _menuSizeService.GetAllMenuSizesNonDeletedAsync();
            return View(menuUpdateDto);
        }
        public async Task<IActionResult> Delete(Guid menuId)
        {
            var title = await _menuService.SafeDeleteMenuAsync(menuId);
            _toastNotification.AddSuccessToastMessage(Messages.Menu.Delete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Menu");
        }
     
        public async Task<IActionResult> UndoDelete(Guid menuId)
        {
            var title = await _menuService.UndoDeleteMenuAsync(menuId);
            _toastNotification.AddSuccessToastMessage(Messages.Menu.UndoDelete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Menu");
        }

    }
}


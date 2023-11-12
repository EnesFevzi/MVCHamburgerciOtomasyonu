using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Web.ResultMessages;
using NToastNotify;

namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
    public class MenuSizeController : Controller
    {
        private readonly IMapper _mapper; 
        private readonly IValidator<MenuSize> _validator;
        private readonly IMenuSizeService _menuSizeService;
        private readonly IToastNotification _toastNotification;

        public MenuSizeController(IMapper mapper, IValidator<MenuSize> validator, IMenuSizeService menuSizeService, IToastNotification toastNotification)
        {
            _mapper = mapper;
            _validator = validator;
            _menuSizeService = menuSizeService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var menuSizes = await _menuSizeService.GetAllMenuSizesNonDeletedAsync();
            return View(menuSizes);
        }
        public async Task<IActionResult> DeletedMenuSize()
        {
            var menuSizes = await _menuSizeService.GetAllMenuSizesDeletedAsync();
            return View(menuSizes);
        }
        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] MenuSizeAddDto menuSizeAddDto)
        {
            var map = _mapper.Map<MenuSize>(menuSizeAddDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _menuSizeService.CreateMenuSizeAsync(menuSizeAddDto);
                _toastNotification.AddSuccessToastMessage(Messages.MenuSize.Add(menuSizeAddDto.SizeName), new ToastrOptions { Title = "İşlem Başarılı" });

                return Json(Messages.MenuSize.Add(menuSizeAddDto.SizeName));
            }
            else
            {
                _toastNotification.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "İşlem Başarısız" });
                return Json(result.Errors.First().ErrorMessage);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MenuSizeAddDto menuSizeAddDto)
        {
            var map = _mapper.Map<MenuSize>(menuSizeAddDto);
            var result = await _validator.ValidateAsync(map);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
            }
            else
            {
                await _menuSizeService.CreateMenuSizeAsync(menuSizeAddDto);
                _toastNotification.AddSuccessToastMessage(Messages.MenuSize.Add(menuSizeAddDto.SizeName), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "MenuSize");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid menuSizeId)
        {
            var menu = await _menuSizeService.GetMenuSizeWithUserNonDeletedAsync(menuSizeId);
            var menuUpdateDto = _mapper.Map<MenuSizeUpdateDto>(menu);
            return View(menuUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MenuSizeUpdateDto menuSizeUpdateDto)
        {
            var map = _mapper.Map<MenuSize>(menuSizeUpdateDto);
            var result = await _validator.ValidateAsync(map);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

            }
            else
            {
                var title = await _menuSizeService.UpdateMenuSizeAsync(menuSizeUpdateDto);
                _toastNotification.AddSuccessToastMessage(Messages.MenuSize.Update(title), new ToastrOptions() { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "MenuSize");
            }

            return View();
        }
        public async Task<IActionResult> Delete(Guid menuSizeId)
        {
            var title = await _menuSizeService.SafeDeleteMenuSizeAsync(menuSizeId);
            _toastNotification.AddSuccessToastMessage(Messages.MenuSize.Delete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "MenuSize");
        }

        public async Task<IActionResult> UndoDelete(Guid menuSizeId)
        {
            var title = await _menuSizeService.UndoDeleteMenuSizeAsync(menuSizeId);
            _toastNotification.AddSuccessToastMessage(Messages.MenuSize.UndoDelete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "MenuSize");
        }
    }
}

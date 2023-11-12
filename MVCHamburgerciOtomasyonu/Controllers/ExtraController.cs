using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Extras;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.FluentValidations;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Service.Services.Concrete;
using MVCHamburgerciOtomasyonu.Web.ResultMessages;
using NToastNotify;

namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
    public class ExtraController : Controller
    {
        private readonly IExtraService _extraService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IValidator<Extra> _extravalidator;
        private readonly IValidator<ExtraAddDto> _extraDtovalidator;

        public ExtraController(IExtraService extraService, IMapper mapper, IToastNotification toastNotification, IValidator<Extra> extravalidator, IValidator<ExtraAddDto> extraDtovalidator)
        {
            _extraService = extraService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _extravalidator = extravalidator;
            _extraDtovalidator = extraDtovalidator;
        }
        public async Task<IActionResult> Index()
        {
            var extras = await _extraService.GetAllExtrasWithImageNonDeletedAsync();
            return View(extras);
        }

        public async Task<IActionResult> DeletedExtra()
        {
            var articles = await _extraService.GetAllExtrasWithImageDeletedAsync();
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExtraAddDto menuAddDto)
        {
            var map = _mapper.Map<Extra>(menuAddDto);
            var result = await _extravalidator.ValidateAsync(map);
            var result2 = await _extraDtovalidator.ValidateAsync(menuAddDto);

            if (!result.IsValid || !result2.IsValid)
            {
                result.AddToModelState(this.ModelState);


            }
            else
            {
                await _extraService.CreateExtraAsync(menuAddDto);
                _toastNotification.AddSuccessToastMessage(Messages.Extra.Add(menuAddDto.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Extra");
            }

            return View();


        }
        [HttpGet]

        public async Task<IActionResult> Update(Guid extraId)
        {
            var extra = await _extraService.GetExtraWithUserNonDeletedAsync(extraId);
            var extraUpdateDto = _mapper.Map<ExtraUpdateDto>(extra);
            return View(extraUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ExtraUpdateDto extraUpdateDto)
        {
            var map = _mapper.Map<Extra>(extraUpdateDto);
            var result = await _extravalidator.ValidateAsync(map);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

            }
            else
            {
                var title = await _extraService.UpdateExtraAsync(extraUpdateDto);
                _toastNotification.AddSuccessToastMessage(Messages.Extra.Update(title), new ToastrOptions() { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Extra");
            }

            
            return View();
        }
        public async Task<IActionResult> Delete(Guid extraId)
        {
            var title = await _extraService.SafeDeleteExtraAsync(extraId);
            _toastNotification.AddSuccessToastMessage(Messages.Extra.Delete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Extra");
        }

        public async Task<IActionResult> UndoDelete(Guid extraId)
        {
            var title = await _extraService.UndoDeleteExtraAsync(extraId);
            _toastNotification.AddSuccessToastMessage(Messages.Extra.UndoDelete(title), new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Extra");
        }
    }
}

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
    public class MenuSizeService : IMenuSizeService
	{
		private readonly IRepository<MenuSize> _menuSizeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public MenuSizeService(IRepository<MenuSize> menuSizeRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IImageHelper imageHelper)
        {
            _menuSizeRepository = menuSizeRepository;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        public async Task CreateMenuSizeAsync(MenuSizeAddDto menuSizeAddDto)
		{
			var map = _mapper.Map<MenuSize>(menuSizeAddDto);
			var userID = _user.GetLoggedInUserId();
			var userEmail = _user.GetLoggedInEmail();

			map.UserID = userID;
			map.CreatedBy = userEmail;
			await _menuSizeRepository.AddAsync(map);
		}
        public async Task<MenuSizeDto> GetMenuSize(Guid menuSizeID)
        {
            var menu = await _menuSizeRepository.GetByGuidAsync(menuSizeID);
            var map = _mapper.Map<MenuSizeDto>(menu);
            return map;
        }

        public async Task<string> UpdateMenuSizeAsync(MenuSizeUpdateDto menuSizeUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menuSize = await _menuSizeRepository.GetAsync(x => x.MenuSizeID == menuSizeUpdateDto.MenuSizeID && !x.IsDeleted);
            var map = _mapper.Map(menuSizeUpdateDto, menuSize);
            menuSize.ModifiedDate = DateTime.Now;
            menuSize.ModifiedBy = userEmail;
            await _menuSizeRepository.UpdateAsync(map);

            return menuSize.SizeName;
        }

        public async Task<List<MenuSizeDto>> GetAllMenuSizesDeletedAsync()
		{
            var menuSizes = await _menuSizeRepository.GetAllAsync(x => x.IsDeleted);
            var map = _mapper.Map<List<MenuSizeDto>>(menuSizes);
            return map;
        }

		public async Task<List<MenuSizeDto>> GetAllMenuSizesNonDeletedAsync()
		{
			var menuSizes = await _menuSizeRepository.GetAllAsync(x => !x.IsDeleted);
			var map = _mapper.Map<List<MenuSizeDto>>(menuSizes);
			return map;
		}


		public async Task<MenuSizeDto> GetMenuSizeWithUserNonDeletedAsync(Guid menuSizeID)
		{
            var menuSize = await _menuSizeRepository.GetAllAsync(x => x.MenuSizeID== menuSizeID ,x=>x.User);
            var map = _mapper.Map<MenuSizeDto>(menuSize);
            return map;
        }

		public async Task<string> SafeDeleteMenuSizeAsync(Guid menuSizeID)
		{
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _menuSizeRepository.GetByGuidAsync(menuSizeID);
            menu.IsDeleted = true;
            menu.DeletedDate = DateTime.Now;
            menu.DeletedBy = userEmail;
            await _menuSizeRepository.UpdateAsync(menu);
            return menu.SizeName;
        }

		public async Task<string> UndoDeleteMenuSizeAsync(Guid menuSizeID)
		{
            var menu = await _menuSizeRepository.GetByGuidAsync(menuSizeID);
            menu.IsDeleted = false;
            menu.DeletedDate = null;
            menu.DeletedBy = null;
            await _menuSizeRepository.UpdateAsync(menu);

            return menu.SizeName;
        }

		
	}
}

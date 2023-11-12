using AutoMapper;
using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Entity.Enums;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Helpers.Images;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using System.Security.Claims;

namespace MVCHamburgerciOtomasyonu.Service.Services.Concrete
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _repository;
        private readonly IRepository<Image> _ımageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public MenuService(IRepository<Menu> repository, IRepository<Image> ımageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IImageHelper imageHelper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
            _imageHelper = imageHelper;
            _ımageRepository = ımageRepository;

        }

        public async Task CreateMenuAsync(MenuAddDto menuAddDto)
        {
            var map = _mapper.Map<Menu>(menuAddDto);
            var userID = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInEmail();

            var imageUpload = await _imageHelper.Upload(menuAddDto.Name, menuAddDto.Photo, ImageType.Menu);
            Image image = new(imageUpload.FullName, menuAddDto.Photo.ContentType, userEmail);
            await _ımageRepository.AddAsync(image);
            map.Image = image;
            map.UserID = userID;
            map.CreatedBy = userEmail;

            await _repository.AddAsync(map);


        }
        public async Task<string> UpdateMenuAsync(MenuUpdateDto MenuUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _repository.GetAsync(x => x.MenuID == MenuUpdateDto.MenuID, x => x.User, x => x.Image);
            //var map = _mapper.Map<Menu>(menu);

            if (MenuUpdateDto.Photo != null)
            {
                _imageHelper.Delete(menu.Image.FileName);
                var imageUpload = await _imageHelper.Upload(MenuUpdateDto.Name, MenuUpdateDto.Photo, ImageType.Menu);
                Image image = new(imageUpload.FullName, MenuUpdateDto.Photo.ContentType, userEmail);
                await _ımageRepository.AddAsync(image);
                MenuUpdateDto.ImageID = image.ImageID;

            }
            else
            {
                if (menu.Image != null)
                {
                    MenuUpdateDto.ImageID = menu.ImageID;
                    MenuUpdateDto.Image = menu.Image;
                }

            }
            _mapper.Map(MenuUpdateDto, menu);

            menu.ModifiedDate = DateTime.Now;
            menu.ModifiedBy = userEmail;
            await _repository.UpdateAsync(menu);
            return menu.Name;
        }

        public async Task<List<MenuDto>> GetAllMenusDeletedAsync()
        {
            var menus = await _repository.GetAllAsync(x => x.IsDeleted);
            var map = _mapper.Map<List<MenuDto>>(menus);
            return map;
        }

        public async Task<MenuDto> GetMenu(Guid menuID)
        {
            var menu = await _repository.GetByGuidAsync(menuID);
            var map = _mapper.Map<MenuDto>(menu);
            return map;
        }
        public async Task<List<MenuDto>> GetAllMenusNonDeletedAsync()
        {
            var menus = await _repository.GetAllAsync(x => !x.IsDeleted);
            var map = _mapper.Map<List<MenuDto>>(menus);
            return map;
        }

        public async Task<MenuDto> GetMenuWithImageAndMenuSizeNonDeletedAsync(Guid menuID)
        {
            var menu = await _repository.GetAsync(x => !x.IsDeleted && x.MenuID == menuID, x => x.User,x=>x.Image);
            var map = _mapper.Map<MenuDto>(menu);
            return map;
        }

        public async Task<List<MenuDto>> GetAllMenusWithImageDeletedAsync()
        {
            var menus = await _repository.GetAllAsync(x => x.IsDeleted, x => x.Image);
            var map = _mapper.Map<List<MenuDto>>(menus);
            return map;
        }

        public async Task<List<MenuDto>> GetAllMenusWithImageNonDeletedAsync()
        {
            var menus = await _repository.GetAllAsync(x => !x.IsDeleted, x => x.Image);
            var map = _mapper.Map<List<MenuDto>>(menus);
            return map;
        }

        public async Task<MenuDto> GetMenuWithUserNonDeletedAsync(Guid menuID)
        {
            var menu = await _repository.GetAsync(x => !x.IsDeleted && x.MenuID == menuID, x => x.User);
            var map = _mapper.Map<MenuDto>(menu);
            return map;
        }

        public async Task<string> SafeDeleteMenuAsync(Guid MenuID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _repository.GetByGuidAsync(MenuID);
            menu.IsDeleted = true;
            menu.DeletedDate = DateTime.Now;
            menu.DeletedBy = userEmail;
            await _repository.UpdateAsync(menu);
            return menu.Name;
        }

        public async Task<string> UndoDeleteMenuAsync(Guid MenuID)
        {
            var menu = await _repository.GetByGuidAsync(MenuID);
            menu.IsDeleted = false;
            menu.DeletedDate = null;
            menu.DeletedBy = null;
            await _repository.UpdateAsync(menu);

            return menu.Name;
        }


    }
}

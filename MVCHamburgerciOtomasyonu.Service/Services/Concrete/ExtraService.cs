using AutoMapper;
using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Entity.Enums;
using MVCHamburgerciOtomasyonu.Service.DTOs.Extras;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Helpers.Images;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using System.Security.Claims;

namespace MVCHamburgerciOtomasyonu.Service.Services.Concrete
{
    public class ExtraService : IExtraService
    {
        private readonly IRepository<Extra> _extraRepository;
        private readonly IRepository<Image> _ımageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public ExtraService(IRepository<Extra> extraRepository, IRepository<Image> ımageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IImageHelper imageHelper)
        {
            _extraRepository = extraRepository;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
            _imageHelper = imageHelper;
            _ımageRepository = ımageRepository;
        }
        public async Task CreateExtraAsync(ExtraAddDto extraAddDto)
        {
            var map = _mapper.Map<Extra>(extraAddDto);
            var userID = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInEmail();

            var imageUpload = await _imageHelper.Upload(extraAddDto.Name, extraAddDto.Photo, ImageType.Extra);
            Image image = new(imageUpload.FullName, extraAddDto.Photo.ContentType, userEmail);
            await _ımageRepository.AddAsync(image);
            map.Image = image;
            map.UserID = userID;
            map.CreatedBy = userEmail;

            await _extraRepository.AddAsync(map);
        }

        public async Task<string> UpdateExtraAsync(ExtraUpdateDto extraUpdateDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var extra = await _extraRepository.GetAsync(x => x.ExtraID == extraUpdateDto.ExtraID, x => x.User, x => x.Image);

            if (extraUpdateDto.Photo != null)
            {
                _imageHelper.Delete(extra.Image.FileName);
                var imageUpload = await _imageHelper.Upload(extraUpdateDto.Name, extraUpdateDto.Photo, ImageType.Menu);
                Image image = new(imageUpload.FullName, extraUpdateDto.Photo.ContentType, userEmail);
                await _ımageRepository.AddAsync(image);
                extraUpdateDto.ImageID = image.ImageID;

            }
            else
            {
                if (extra.Image != null)
                {
                    extraUpdateDto.ImageID = extra.ImageID;
                    extraUpdateDto.Image = extra.Image;
                }

            }
            _mapper.Map(extraUpdateDto, extra);

            extra.ModifiedDate = DateTime.Now;
            extra.ModifiedBy = userEmail;
            await _extraRepository.UpdateAsync(extra);
            return extra.Name;
        }

        public async Task<List<ExtraDto>> GetAllExtrasDeletedAsync()
        {
            var menus = await _extraRepository.GetAllAsync(x => x.IsDeleted);
            var map = _mapper.Map<List<ExtraDto>>(menus);
            return map;
        }

        public async Task<List<ExtraDto>> GetAllExtrasNonDeletedAsync()
        {
            var menus = await _extraRepository.GetAllAsync(x => !x.IsDeleted);
            var map = _mapper.Map<List<ExtraDto>>(menus);
            return map;
        }

        public async Task<List<ExtraDto>> GetAllExtrasWithImageDeletedAsync()
        {
            var menus = await _extraRepository.GetAllAsync(x => x.IsDeleted, x => x.Image);
            var map = _mapper.Map<List<ExtraDto>>(menus);
            return map;
        }

        public async Task<List<ExtraDto>> GetAllExtrasWithImageNonDeletedAsync()
        {
            var extras = await _extraRepository.GetAllAsync(x => !x.IsDeleted, x => x.Image);
            var map = _mapper.Map<List<ExtraDto>>(extras);
            return map;
        }

        public async Task<ExtraDto> GetExtraWithUserNonDeletedAsync(Guid extraID)
        {
            var extra = await _extraRepository.GetAsync(x => x.ExtraID == extraID && !x.IsDeleted, x => x.Image, x => x.User);
            var map = _mapper.Map<ExtraDto>(extra);
            return map;
        }

        public async Task<string> SafeDeleteExtraAsync(Guid ExtraID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _extraRepository.GetByGuidAsync(ExtraID);
            menu.IsDeleted = true;
            menu.DeletedDate = DateTime.Now;
            menu.DeletedBy = userEmail;
            await _extraRepository.UpdateAsync(menu);
            return menu.Name;
        }

        public async Task<string> UndoDeleteExtraAsync(Guid ExtraID)
        {
            var menu = await _extraRepository.GetByGuidAsync(ExtraID);
            menu.IsDeleted = false;
            menu.DeletedDate = null;
            menu.DeletedBy = null;
            await _extraRepository.UpdateAsync(menu);

            return menu.Name;
        }


    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Entity.Enums;
using MVCHamburgerciOtomasyonu.Service.DTOs.Users;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Helpers.Images;
using MVCHamburgerciOtomasyonu.Service.Models;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVCHamburgerciOtomasyonu.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IImageHelper _imageHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ClaimsPrincipal _user;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<Image> _ımageRepository;
        public UserService(IRepository<AppUser> userRepository, IRepository<Image> ımageRepository, IImageHelper imageHelper, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {

            _imageHelper = imageHelper;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _ımageRepository = ımageRepository;
            _userRepository = userRepository;
        }
        public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);
            map.UserName = userAddDto.Email;
            var result = await _userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);
            if (result.Succeeded)
            {
                var findRole = await _roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                await _userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
                return result;
        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId)
        {
            var user = await GetAppUserByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return (result, user.Email);
            else
                return (result, null);
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        public async Task<List<UserDto>> GetUsersWithoutSuperadminRoleAsync()
        {
            var usersWithRole = await _userManager.GetUsersInRoleAsync("Superadmin");
            var allUsers = await _userManager.Users.ToListAsync();

            var usersWithoutSuperadminRole = allUsers.Where(user => !usersWithRole.Contains(user)).ToList();

            var userDtos = _mapper.Map<List<UserDto>>(usersWithoutSuperadminRole);

            return userDtos;
        }

        public async Task<List<AppRole>> GetRolesWithoutSuperadminAsync()
        {
            var superadminRole = await _roleManager.FindByNameAsync("SuperAdmin");
            var roles = await _roleManager.Roles.Where(r => r.Id != superadminRole.Id).ToListAsync();
            return roles;
        }

        public async Task<List<UserDto>> GetAllUsersWithRoleAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var map = _mapper.Map<List<UserDto>>(users);


            foreach (var item in map)
            {
                var findUser = await _userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await _userManager.GetRolesAsync(findUser));
                item.Role = role;
            }

            return map;
        }

        public async Task<List<UserDto>> GetUsersWithUserRoleAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            var userDtos = _mapper.Map<List<UserDto>>(users);

            return userDtos;
        }
        public async Task<List<UserDto>> GetWorkersWithUserRoleAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Worker");
            var userDtos = _mapper.Map<List<UserDto>>(users);

            return userDtos;
        }

        public async Task<AppUser> GetAppUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userID);
            var getImage = await _userRepository.GetAsync(x => x.Id == userID, x => x.Image);
            var role = string.Join("", await GetUserRoleAsync(user));
            var map = _mapper.Map<UserProfileDto>(getImage);
            map.Image.FileName = getImage.Image.FileName;
            return map;
        }

        public async Task<AppUser> GetUserAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userID);
          
            return user;
        }

        public async Task<UserDto> GetUserProfileAsyncWitRoleAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userID);
            var getImage = await _userRepository.GetAsync(x => x.Id == userID, x => x.Image);
            var role = string.Join("", await GetUserRoleAsync(user));
            var map = _mapper.Map<UserDto>(getImage);
            map.Image.FileName = getImage.Image.FileName;
            map.Role = role;
            return map;
        }
        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return string.Join("", await _userManager.GetRolesAsync(user));
        }
        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUserRoleAsync(user);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await _roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await _userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            else
                return result;
        }

        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {
            var userId = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userId);

            if (userProfileDto.CurrentPassword != null)
            {
                var isVerified = await _userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
                if (isVerified && userProfileDto.NewPassword != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                        _mapper.Map(userProfileDto, user);

                        if (userProfileDto.Photo != null)
                            user.ImageID = await UploadImageForUser(userProfileDto);

                        await _userManager.UpdateAsync(user);
                        return true;
                    }
                    else
                        return false;
                }
                else if (isVerified)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    _mapper.Map(userProfileDto, user);

                    if (userProfileDto.Photo != null)
                        user.ImageID = await UploadImageForUser(userProfileDto);

                    await _userManager.UpdateAsync(user);
                   
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private async Task<Guid> UploadImageForUser(UserProfileDto userProfileDto)
        {
            var userEmail = _user.GetLoggedInEmail();

            var imageUpload = await _imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.User);
            Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, userEmail);
            await _ımageRepository.AddAsync(image);

            return image.ImageID;
        }

        public async Task<WeatherInfo> GetWeatherInfo()
        {
            string api = "64d476e683236d625b8f0a39392c240a";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid=" + api;
            XDocument document = XDocument.Load(connection);
            var temperature = decimal.Parse(document.Descendants("temperature").ElementAt(0).Attribute("value").Value);

            temperature  = Math.Round(temperature);
            
            string condition = string.Empty;

            if (temperature >= 30)
            {
                condition = "Bugün hava çok sıcak!";
            }
            else if (temperature >= 20)
            {
                condition = "Bugün hava ılıman.";
            }
            else if (temperature >= 10)
            {
                condition = "Bugün hava serin.";
            }
            else
            {
                condition = "Bugün hava soğuk.";
            }

            return new WeatherInfo
            {
                Condition = condition,
                Temperature = temperature
            };
        }

        
    }
}

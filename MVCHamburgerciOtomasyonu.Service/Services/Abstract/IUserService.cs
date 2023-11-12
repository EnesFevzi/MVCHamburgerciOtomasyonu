using Microsoft.AspNetCore.Identity;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Users;
using MVCHamburgerciOtomasyonu.Service.Models;

namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersWithRoleAsync();
        Task<List<UserDto>> GetUsersWithUserRoleAsync();
        Task<List<UserDto>> GetWorkersWithUserRoleAsync();
        Task<List<AppRole>> GetAllRolesAsync();
        Task<AppUser> GetUserAsync();
        Task<List<UserDto>> GetUsersWithoutSuperadminRoleAsync();
        Task<List<AppRole>> GetRolesWithoutSuperadminAsync();
        Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId);
        Task<AppUser> GetAppUserByIdAsync(Guid userId);
        Task<string> GetUserRoleAsync(AppUser user);
        Task<UserProfileDto> GetUserProfileAsync();
        Task<UserDto> GetUserProfileAsyncWitRoleAsync();
        Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto);
        Task<WeatherInfo> GetWeatherInfo();
    }
}

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Users;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Web.Consts;
using MVCHamburgerciOtomasyonu.Web.ResultMessages;
using NToastNotify;


namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IMapper _mapper;
		private readonly IToastNotification _toastNotification;
		private readonly IValidator<AppUser> _validator;
		private readonly IUserService _userService;

		public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, IToastNotification toastNotification, IValidator<AppUser> validator, IUserService userService)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
			_toastNotification = toastNotification;
			_validator = validator;
			_userService = userService;

		}
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Index()
		{
			var result = await _userService.GetUsersWithUserRoleAsync();
			return View(result);
		}
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Worker()
		{
			var result = await _userService.GetWorkersWithUserRoleAsync();
			return View(result);
		}
		[HttpGet]
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Add()
		{
			var roles = await _userService.GetRolesWithoutSuperadminAsync();

			return View(new UserAddDto { Roles = roles });
		}
		[HttpPost]
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Add(UserAddDto userAddDto)
		{
			var map = _mapper.Map<AppUser>(userAddDto);
			var validation = await _validator.ValidateAsync(map);
			var roles = await _roleManager.Roles.ToListAsync();
			if (validation.IsValid)
			{
				var result = await _userService.CreateUserAsync(userAddDto);
				if (result.Succeeded)
				{
					_toastNotification.AddSuccessToastMessage(Messages.User.Add(userAddDto.Email), new ToastrOptions { Title = "İşlem Başarılı" });
					return RedirectToAction("Index", "User");
				}
				else
				{
					result.AddToIdentityModelState(this.ModelState);
					validation.AddToModelState(this.ModelState);
					return View(new UserAddDto { Roles = roles });

				}
			}
			return View(new UserAddDto { Roles = roles });
		}
		[HttpGet]
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Update(Guid userId)
		{
			var user = await _userService.GetAppUserByIdAsync(userId);
			var roles = await _userService.GetAllRolesAsync();
			var map = _mapper.Map<UserUpdateDto>(user);
			map.Roles = roles;
			return View(map);
		}
		[HttpPost]
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
		{
			var user = await _userService.GetAppUserByIdAsync(userUpdateDto.Id);

			if (user != null)
			{
				var roles = await _userService.GetAllRolesAsync();
				if (ModelState.IsValid)
				{
					var map = _mapper.Map(userUpdateDto, user);
					var validation = await _validator.ValidateAsync(map);

					if (validation.IsValid)
					{
						user.UserName = userUpdateDto.Email;
						user.SecurityStamp = Guid.NewGuid().ToString();
						var result = await _userService.UpdateUserAsync(userUpdateDto);
						if (result.Succeeded)
						{
							_toastNotification.AddSuccessToastMessage(Messages.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "İşlem Başarılı" });
							return RedirectToAction("Index", "User");
						}
						else
						{
							result.AddToIdentityModelState(this.ModelState);
							return View(new UserUpdateDto { Roles = roles });
						}
					}
					else
					{
						validation.AddToModelState(this.ModelState);
						return View(new UserUpdateDto { Roles = roles });
					}
				}
			}
			return NotFound();
		}
		[Authorize(Roles = $"{RoleConsts.Superadmin}")]
		public async Task<IActionResult> Delete(Guid userId)
		{
			var result = await _userService.DeleteUserAsync(userId);

			if (result.identityResult.Succeeded)
			{
				_toastNotification.AddSuccessToastMessage(Messages.User.Delete(result.email), new ToastrOptions { Title = "İşlem Başarılı" });
				return RedirectToAction("Index", "User", new { Area = "Admin" });
			}
			else
			{
				result.identityResult.AddToIdentityModelState(this.ModelState);
			}
			return NotFound();
		}

		[HttpGet]
		[Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.User}")]
		public async Task<IActionResult> Profile()
		{
			var profile = await _userService.GetUserProfileAsync();
			return View(profile);

		}
		[HttpPost]
		[Authorize(Roles = $"{RoleConsts.Superadmin},{RoleConsts.User}")]
		public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
		{
			if (ModelState.IsValid && userProfileDto.CurrentPassword != null)
			{
				var result = await _userService.UserProfileUpdateAsync(userProfileDto);
				if (result)
				{
					_toastNotification.AddSuccessToastMessage("Profil güncelleme işlemi tamamlandı", new ToastrOptions { Title = "İşlem Başarılı" });
                    var user = await _userService.GetUserAsync();
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(RoleConsts.Superadmin))
                    {
                        return RedirectToAction("AdminDashboard", "Dashboard");
                    }
                    if (roles.Contains(RoleConsts.User))
                    {
                        return RedirectToAction("UserDashboard", "Dashboard");
                    }
					return View();
                    
				}
				else
				{
					var profile = await _userService.GetUserProfileAsync();
					_toastNotification.AddErrorToastMessage("Profil güncelleme işlemi tamamlanamadı,Eksik Bilgiler Mevcut", new ToastrOptions { Title = "İşlem Başarısız" });
					return View(profile);
				}
			}
			else
			{


				var profile = await _userService.GetUserProfileAsync();
				_toastNotification.AddErrorToastMessage("Profil güncelleme işlemi tamamlanamadı,Mevcut Şifreyi Girmeniz Gerekmektedir.", new ToastrOptions { Title = "İşlem Başarısız" });
				return View(profile);
			}
		}
	}
}

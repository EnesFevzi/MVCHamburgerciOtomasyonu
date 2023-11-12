using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
	public interface IMenuService
	{
		Task<List<MenuDto>> GetAllMenusNonDeletedAsync();
		Task<List<MenuDto>> GetAllMenusWithImageNonDeletedAsync();
		Task<List<MenuDto>> GetAllMenusDeletedAsync();
		Task<MenuDto> GetMenu(Guid menuID);
        Task<MenuDto> GetMenuWithImageAndMenuSizeNonDeletedAsync(Guid menuID);
        Task<List<MenuDto>> GetAllMenusWithImageDeletedAsync();
		Task<MenuDto> GetMenuWithUserNonDeletedAsync(Guid menuID);
		Task CreateMenuAsync(MenuAddDto menuAddDto);
		Task<string> UpdateMenuAsync(MenuUpdateDto MenuUpdateDto);
		Task<string> SafeDeleteMenuAsync(Guid MenuID);
		Task<string> UndoDeleteMenuAsync(Guid MenuID);
	}
}

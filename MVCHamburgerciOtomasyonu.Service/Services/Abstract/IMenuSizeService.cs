using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;
using MVCHamburgerciOtomasyonu.Service.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
    public interface IMenuSizeService
	{
		Task<List<MenuSizeDto>> GetAllMenuSizesNonDeletedAsync();
		Task<List<MenuSizeDto>> GetAllMenuSizesDeletedAsync();
		Task<MenuSizeDto> GetMenuSizeWithUserNonDeletedAsync(Guid menuSizeID);
		Task<MenuSizeDto> GetMenuSize(Guid menuSizeID);
        Task CreateMenuSizeAsync(MenuSizeAddDto menuSizeAddDto);
		Task<string> UpdateMenuSizeAsync(MenuSizeUpdateDto MenuSizeUpdateDto);
		Task<string> SafeDeleteMenuSizeAsync(Guid menuSizeID);
		Task<string> UndoDeleteMenuSizeAsync(Guid menuSizeID);
	}
}

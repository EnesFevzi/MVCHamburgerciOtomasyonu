using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Menus
{
    public class MenuAddDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		//public Guid MenuSizeId { get; set; }

  //      public IList<MenuSizeDto>? MenuSizes { get; set; } = new List<MenuSizeDto>();
        public IFormFile? Photo { get; set; }
	}
}

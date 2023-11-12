using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Menus
{
	public class MenuUpdateDto
	{
		public Guid MenuID { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		//public Guid MenuSizeId { get; set; }
  //      public MenuSize MenuSize { get; set; }
        public Guid? ImageID { get; set; }
		public Image Image { get; set; }
		public IFormFile? Photo { get; set; }
		//public IList<MenuSizeDto> MenuSizes { get; set; }
	}
}

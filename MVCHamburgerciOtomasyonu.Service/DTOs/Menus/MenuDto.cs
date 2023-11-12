using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Menus
{
	public class MenuDto
	{
		public Guid MenuID { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

        public string NameAndPrice => $"{Name} - {Price} ₺";
        //public MenuSize MenuSize { get; set; }
        public Image Image { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }


	}
}

using MVCHamburgerciOtomasyonu.Service.DTOs.Extras;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Orders
{
    public class OrderAddDto
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
      
       
       public Guid MenuId { get; set; }




        public Guid MenuSizeId { get; set; }
        public IList<MenuSizeDto> MenuSizes { get; set; }






        public IList<MenuDto> Menus { get; set; }
       // public List<Guid> SelectedMenus { get; set; }
      



        public List<Guid> SelectedExtras { get; set; }
        public IList<ExtraDto> Extras { get; set; }
    }
}

using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Models
{
    public class OrderViewModel
    {
        public int Quantity { get; set; } // Adet
        public Guid SelectedMenuId { get; set; } // Kullanıcının seçtiği menü
        public List<Guid> SelectedExtraIds { get; set; } // Kullanıcının seçtiği ekstraların kimlikleri
        public List<Menu> Menus { get; set; } // Menü seçenekleri
        public List<Extra> Extras { get; set; } // Ekstra seçenekleri

        public OrderViewModel()
        {
            Menus = new List<Menu>();
            Extras = new List<Extra>();
        }
    }
}

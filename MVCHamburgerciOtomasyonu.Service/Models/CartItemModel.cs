using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Models
{
    public class CartItemModel
    {
        public Guid menuID { get; set; }
        public string menuName { get; set; }
        public int quantity { get; set; }
        public List<Extra> extras { get; set; }
    }
}


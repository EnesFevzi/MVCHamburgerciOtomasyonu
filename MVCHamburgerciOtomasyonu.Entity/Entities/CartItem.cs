using MVCHamburgerciOtomasyonu.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class CartItem : EntityBase
    {
        public Guid CartItemID { get; set; }
        public Guid MenuID { get; set; }
        public Menu Menu { get; set; }
        public string MenuName { get; set; }
        public int Quantity { get; set; }
        public List<Extra> Extras { get; set; }
        public decimal Price { get; set; }
    }

}


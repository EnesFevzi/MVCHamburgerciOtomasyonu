using MVCHamburgerciOtomasyonu.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class MenuSize : EntityBase
    {
        public Guid MenuSizeID { get; set; }
        public string SizeName { get; set; }

        public decimal PriceModifier { get; set; }
        public List<Order> Orders { get; set; }

        public Guid? UserID { get; set; }
        public AppUser User { get; set; }
    }
}

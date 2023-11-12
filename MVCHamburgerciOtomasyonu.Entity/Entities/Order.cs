using MVCHamburgerciOtomasyonu.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class Order : EntityBase
    {
        public Order()
        {
            Extras = new List<Extra>();
            //Menus = new List<Menu>();
        }

        public Guid OrderID { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? MenuSizeID { get; set; }
        public MenuSize MenuSize { get; set; }
        public Guid? MenuID { get; set; }
        public Menu Menu { get; set; }
        //public virtual List<Menu> Menus { get; set; }

        public virtual List<Extra> Extras { get; set; }
        public Guid? UserID { get; set; }
        public AppUser User { get; set; }

        public virtual bool IsCompleted { get; set; } = false;
        public string Status { get; set; } = "Sipariş Alındı";
    }
}

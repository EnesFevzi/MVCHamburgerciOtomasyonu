using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Orders
{
    public class OrderDto
    {
        public Guid OrderID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public MenuSize MenuSize { get; set; }
        public Menu Menu { get; set; }
        public  DateTime CreatedDate { get; set; }
        public virtual IList<Extra> Extras { get; set; } = new List<Extra>();

        public string? Status { get; set; }
        public bool IsCompleted { get; set; }
    }
}

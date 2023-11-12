using MVCHamburgerciOtomasyonu.Core.Entities.Concrete;
using System.Drawing;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class Menu : EntityBase
    {
        public Guid MenuID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string NameAndPrice => $"{Name} - {Price} ₺";
        public List<Order> Orders { get; set; }
        public Guid? ImageID { get; set; }
        public Image Image { get; set; }

		public Guid? UserID { get; set; }
		public AppUser User { get; set; }
	}
}

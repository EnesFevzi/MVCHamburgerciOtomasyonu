using MVCHamburgerciOtomasyonu.Core.Entities.Concrete;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class Extra : EntityBase
    {
        public Guid ExtraID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid? ImageID { get; set; }
        public Image Image { get; set; }
        public Guid? UserID { get; set; }
        public AppUser User { get; set; }
    }
}

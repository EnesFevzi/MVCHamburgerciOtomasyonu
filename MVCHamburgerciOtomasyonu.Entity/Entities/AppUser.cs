using Microsoft.AspNetCore.Identity;
using MVCHamburgerciOtomasyonu.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class AppUser : IdentityUser<Guid>, IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? ImageID { get; set; } = Guid.Parse("01673030-C382-45F8-84DC-A095BF6A7532");

		public Image Image { get; set; }
    }
}

using MVCHamburgerciOtomasyonu.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Entity.Entities
{
    public class Image : EntityBase
    {

        public Image()
        {
            Users = new HashSet<AppUser>();
            Menus = new HashSet<Menu>();
            Extras = new HashSet<Extra>();
        }
		public Image(string fileName, string fileType, string createdBy)
		{
			FileName = fileName;
			FileType = fileType;
			CreatedBy = createdBy;
		}

		public Guid ImageID { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }

        public ICollection<AppUser> Users { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<Extra> Extras { get; set; }

    }
}

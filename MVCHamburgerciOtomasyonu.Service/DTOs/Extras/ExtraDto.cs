using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Extras
{
    public class ExtraDto
    {
        public Guid ExtraID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Image Image { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

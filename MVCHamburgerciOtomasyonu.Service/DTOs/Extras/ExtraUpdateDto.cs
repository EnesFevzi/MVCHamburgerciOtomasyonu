using Microsoft.AspNetCore.Http;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Extras
{
    public class ExtraUpdateDto
    {
        public Guid ExtraID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Guid? ImageID { get; set; }
        public Image Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}

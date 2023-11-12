using Microsoft.AspNetCore.Http;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Extras
{
    public class ExtraAddDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Photo { get; set; }
    }
}

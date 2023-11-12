using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes
{
    public class MenuSizeUpdateDto
    {
        public Guid MenuSizeID { get; set; }
        public string SizeName { get; set; }

        public decimal? PriceModifier { get; set; }
    }
}

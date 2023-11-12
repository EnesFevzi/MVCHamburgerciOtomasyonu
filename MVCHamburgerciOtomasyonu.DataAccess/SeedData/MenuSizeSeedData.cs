using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.DataAccess.SeedData
{
    public class MenuSizeSeedData : IEntityTypeConfiguration<MenuSize>
    {
        public void Configure(EntityTypeBuilder<MenuSize> builder)
        {
            builder.HasData(
                new MenuSize
                {
                    MenuSizeID = Guid.Parse("9A9CDFAC-F735-47CF-A24D-F93EC613E09B"),
                    SizeName = "Normal Boy",
                    PriceModifier = 0M
                },
                new MenuSize
                {
                    MenuSizeID = Guid.Parse("14B9C7DF-6CF5-4A99-AE1C-C0A59EE4102C"),
                    SizeName = "Büyük Boy",
                    PriceModifier = 15.00M
                },
                new MenuSize
                {
                    MenuSizeID = Guid.Parse("DE6C42D5-1CB2-430D-8E48-D4FE931D4843"),
                    SizeName = "King Boy",
                    PriceModifier = 20.00M
                }
            );
        }
    }
}

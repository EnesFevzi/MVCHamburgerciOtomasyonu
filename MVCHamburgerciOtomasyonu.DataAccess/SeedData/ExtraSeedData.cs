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
    public class ExtraSeedData : IEntityTypeConfiguration<Extra>
    {
        public void Configure(EntityTypeBuilder<Extra> builder)
        {

            builder.HasData(new Extra
            {
                ExtraID = Guid.Parse("5FBDF069-889E-4F44-9984-55719389EE3C"),
                Name = "Tırtıklı Patates",
                Price = 10.00M,
                ImageID = Guid.Parse("B5C2ACF1-6B30-4E5D-8AF4-5B195EEEC91A"),
            },
           new Extra
           {
               ExtraID = Guid.Parse("941EA186-AD0E-407A-9694-99F5160B680F"),
               Name = "Soğan Halkası",
               Price = 20.00M,
               ImageID = Guid.Parse("F8158844-15D4-4014-BE20-745A147449B5"),
           },
           new Extra
           {
               ExtraID = Guid.Parse("20F69585-AC98-4213-8CFB-07D3B37927AA"),
               Name = "Çıtır Peynir",
               Price = 25.00M,
               ImageID = Guid.Parse("0F0E98FA-6B24-4C0E-9D2C-9AFA9220A610"),
           },
           new Extra
           {
               ExtraID = Guid.Parse("EA151FC1-5451-49AE-84EE-DD4315D021CA"),
               Name = "I-FEAT King Nuggets",
               Price = 35.00M,
               ImageID = Guid.Parse("88057A22-8DE7-4C7A-B712-C4053403AE60"),
           },
           new Extra
           {
               ExtraID = Guid.Parse("866D8813-A009-451A-A4A2-283509B60AE6"),
               Name = "Chicken Tenders",
               Price = 45.00M,
               ImageID = Guid.Parse("81FCBC63-54C7-4DDB-92AA-F677C1A144BB"),
           });
        }
    }
}

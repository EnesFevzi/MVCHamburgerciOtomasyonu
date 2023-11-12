using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.DataAccess.SeedData
{
    public class MenuSeedData : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasData(
            new Menu
            {
                MenuID = Guid.Parse("31DF718A-1182-48EF-A5ED-419DB6A217FA"),
                Name = "King Chicken",
                Price = 150.00M,
                CreatedBy = "Super Admın",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                ImageID = Guid.Parse("C213DFEC-2010-494C-A132-91FB6D8334CC"),

            },
            new Menu
            {
                MenuID = Guid.Parse("4454DA72-6C4C-4F57-B51A-76C50C8C8C05"),
                Name = "Double Whopper",
                Price = 160.00M,
                CreatedBy = "Super Admın",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                ImageID = Guid.Parse("09086832-F4C9-4B09-8DF4-055014D961C5"),
            },
             new Menu
             {
               
                 MenuID = Guid.Parse("B1EF787B-B80B-46E9-B827-51C9C05770A6"),
                 Name = "Big King",
                 Price = 170.00M,
                 CreatedBy = "Super Admın",
                 CreatedDate = DateTime.Now,
                 IsDeleted = false,
                 ImageID = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214"),
             },
              new Menu
              {
                
                  MenuID = Guid.Parse("5BE15229-6970-4E16-84E3-AFA6A0124977"),
                  Name = "Fish Royale",
                  Price = 180.00M,
                  CreatedBy = "Super Admın",
                  CreatedDate = DateTime.Now,
                  IsDeleted = false,
                  ImageID = Guid.Parse("109BAF81-2778-42E5-84CD-9B95EA88F1A7"),
              },
               new Menu
               {
                
                   MenuID = Guid.Parse("FD87ED69-9F92-4FF1-8D13-8435117A63E0"),
                   Name = "Tavuk Burger Menü",
                   Price = 190.00M,
                   CreatedBy = "Super Admın",
                   CreatedDate = DateTime.Now,
                   IsDeleted = false,
                   ImageID = Guid.Parse("9CBC9994-F594-4FFB-97B2-45D09F9F10F4"),
               },
                new Menu
                {
                 
                    MenuID = Guid.Parse("B68520C4-3C03-4DD1-B3E9-3024D9C53D55"),
                    Name = "Chicken Royale",
                    Price = 200.00M,
                    CreatedBy = "Super Admın",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    ImageID = Guid.Parse("0FC8F1CA-5366-4CB7-8492-5D17687CB648"),
                },
                 new Menu
                 {
                  
                     MenuID = Guid.Parse("66C8E428-7A09-4449-A605-64F9497AE2CE"),
                     Name = "Köfte Burger",
                     Price = 220.00M,
                     CreatedBy = "Super Admın",
                     CreatedDate = DateTime.Now,
                     IsDeleted = false,
                     ImageID = Guid.Parse("BA68B7C8-504C-4ABD-BB46-5ADD111B48BC"),
                 },
                 new Menu
                 {
                     MenuID = Guid.Parse("6C885842-C524-42EF-AF4D-DA3E70952E18"),
                     Name = "CheeseBurger Menü",
                     Price = 250.00M,
                     CreatedBy = "Super Admın",
                     CreatedDate = DateTime.Now,
                     IsDeleted = false,
                     ImageID = Guid.Parse("585CECBC-CF45-4198-910F-4FC0B0D07C2D"),
                 }

             );
        }
    }
}

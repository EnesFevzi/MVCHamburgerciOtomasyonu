using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCHamburgerciOtomasyonu.Entity.Entities;

namespace MVCHamburgerciOtomasyonu.DataAccess.SeedData
{
    public class ImageSeedData : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                ImageID = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214"),
                FileName = "menu-images/big-king-menu.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("585CECBC-CF45-4198-910F-4FC0B0D07C2D"),
                FileName = "menu-images/cheeseburger-menu.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("0FC8F1CA-5366-4CB7-8492-5D17687CB648"),
                FileName = "menu-images/chicken-royale-menu.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("C213DFEC-2010-494C-A132-91FB6D8334CC"),
                FileName = "menu-images/king-chicken-1.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            
            new Image
            {
                ImageID = Guid.Parse("09086832-F4C9-4B09-8DF4-055014D961C5"),
                FileName = "menu-images/double-whopper-jr.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("109BAF81-2778-42E5-84CD-9B95EA88F1A7"),
                FileName = "menu-images/fish-royale-menu.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("BA68B7C8-504C-4ABD-BB46-5ADD111B48BC"),
                FileName = "menu-images/kofteburger-menu.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("9CBC9994-F594-4FFB-97B2-45D09F9F10F4"),
                FileName = "menu-images/tavukburger-menu.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("88057A22-8DE7-4C7A-B712-C4053403AE60"),
                FileName = "extra-images/bk-king-nuggets-1.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("81FCBC63-54C7-4DDB-92AA-F677C1A144BB"),
                FileName = "extra-images/chicken-tenders.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("0F0E98FA-6B24-4C0E-9D2C-9AFA9220A610"),
                FileName = "extra-images/citir-peynir.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            }
            ,
            new Image
            {
                ImageID = Guid.Parse("F8158844-15D4-4014-BE20-745A147449B5"),
                FileName = "extra-images/sogan-halkasi.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("B5C2ACF1-6B30-4E5D-8AF4-5B195EEEC91A"),
                FileName = "extra-images/tirtikli-patates.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                ImageID = Guid.Parse("01673030-C382-45F8-84DC-A095BF6A7532"),
                FileName = "user-images/user.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            }
            ,
            new Image
            {
                ImageID = Guid.Parse("930ABCE6-6F8C-4144-9332-ED04A7F0C40A"),
                FileName = "user-images/superadmin.png",
                FileType = "image/png",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            }
            );
        }
    }
}

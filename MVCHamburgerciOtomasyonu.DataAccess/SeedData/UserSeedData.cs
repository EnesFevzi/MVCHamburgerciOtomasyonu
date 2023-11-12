using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCHamburgerciOtomasyonu.Entity.Entities;

namespace MVCHamburgerciOtomasyonu.DataAccess.SeedData
{
    public class UserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            
            var superadmin = new AppUser
            {
                Id = Guid.Parse("CB94223B-CCB8-4F2F-93D7-0DF96A7F065C"),
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "+905439999999",
                FirstName = "Super",
                LastName = "Admin",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageID = Guid.Parse("930ABCE6-6F8C-4144-9332-ED04A7F0C40A")
            };
            superadmin.PasswordHash = CreatePasswordHash(superadmin, "123456");

            var admin = new AppUser
            {
                Id = Guid.Parse("B207B056-26AC-4BE9-B6A5-07EB8C9E8D76"),
                UserName = "user@gmail.com",
                NormalizedUserName = "USER@GMAIL.COM",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                PhoneNumber = "+905439999988",
                FirstName = "User",
                LastName = "User",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageID = Guid.Parse("01673030-C382-45F8-84DC-A095BF6A7532")
            };
            admin.PasswordHash = CreatePasswordHash(admin, "123456");

            builder.HasData(superadmin, admin);

        }
        private string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}

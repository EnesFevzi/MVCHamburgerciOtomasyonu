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
    public class UserRoleSeedData : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("CB94223B-CCB8-4F2F-93D7-0DF96A7F065C"),
                RoleId = Guid.Parse("343F8370-28D4-4ADE-91DF-7965041B98F1"),
            },
            new AppUserRole
            {
                UserId = Guid.Parse("B207B056-26AC-4BE9-B6A5-07EB8C9E8D76"),
                RoleId = Guid.Parse("F0A0B477-42AA-47FD-9E01-A81DA466848D")
            });
        }
    }
}

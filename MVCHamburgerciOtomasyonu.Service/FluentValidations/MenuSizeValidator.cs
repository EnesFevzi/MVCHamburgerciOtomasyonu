using FluentValidation;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.FluentValidations
{
    public class MenuSizeValidator:AbstractValidator<MenuSize>
    {
        public MenuSizeValidator()
        {
            RuleFor(x => x.SizeName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("İsim");

            RuleFor(x => x.PriceModifier)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithName("Fiyat");
        }
    }
}

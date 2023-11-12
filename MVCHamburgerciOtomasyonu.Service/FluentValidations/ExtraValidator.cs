using FluentValidation;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Extras;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.FluentValidations
{
    public class ExtraValidator:AbstractValidator<Extra>
    {
        public ExtraValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("İsim");

            RuleFor(x => x.Price)
               .NotEmpty()
               .NotNull()
               .GreaterThan(0)
               .WithName("Para");
        }
    }
    public class ExtraAddValidator : AbstractValidator<ExtraAddDto>
    {
        public ExtraAddValidator()
        {

            RuleFor(x => x.Photo)
                .NotEmpty()
                .WithName("Resim dosyası");

        }
    }
}

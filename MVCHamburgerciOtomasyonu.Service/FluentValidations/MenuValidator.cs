using FluentValidation;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;

namespace MVCHamburgerciOtomasyonu.Service.FluentValidations
{
    public class MenuValidator : AbstractValidator<Menu>
    {
        public MenuValidator()
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
			   .WithName("Fiyat");
			   
        }
    }
	public class MenuAddValidator : AbstractValidator<MenuAddDto>
	{
		public MenuAddValidator()
		{

			RuleFor(x => x.Photo)
				.NotEmpty()
				.WithName("Resim dosyası");


			RuleFor(x => x.Price)
			   .NotEmpty()
			   .NotNull()
			   .GreaterThan(0)
			   .WithName("Fiyat");

		}
	}
}

﻿using FluentValidation;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.FluentValidations
{
	public class UserValidator : AbstractValidator<AppUser>
	{
		public UserValidator()
		{
			RuleFor(x => x.FirstName)
			 .NotEmpty()
			 .MinimumLength(3)
			 .MaximumLength(50)
			 .WithName("İsim");

			RuleFor(x => x.LastName)
			 .NotEmpty()
			 .MinimumLength(3)
			 .MaximumLength(50)
			 .WithName("Soyisim");

			RuleFor(x => x.PhoneNumber)
			 .NotEmpty()
			 .MinimumLength(11)
			 .WithName("Telefon numarası");

			RuleFor(x => x.ImageID)
			 .NotEmpty()
			 .WithName("Profil Fotoğrafı");
		}

		public class RegisterUserValidator : AbstractValidator<CreateUserDto>
		{
            public RegisterUserValidator()
            {
				RuleFor(x => x.FirstName)
			 .NotEmpty()
			 .MinimumLength(3)
			 .MaximumLength(50)
			 .WithName("İsim");

				RuleFor(x => x.LastName)
				 .NotEmpty()
				 .MinimumLength(3)
				 .MaximumLength(50)
				 .WithName("Soyisim");

				RuleFor(x => x.EMail)
				 .NotEmpty()
				 .MinimumLength(3)
				 .MaximumLength(100)
				 .WithName("Email");

				RuleFor(x => x.Password)
				 .NotEmpty()
				 .MinimumLength(3)
				 .MaximumLength(100)
				 .WithName("Parola");
				
			


			}

		}


		public class LoginUserValidator : AbstractValidator<UserLoginDto>
		{
			public LoginUserValidator()
			{
			

				RuleFor(x => x.Email)
				 .NotEmpty()
				 .MinimumLength(3)
				 .MaximumLength(100)
				 .WithName("Email");

				RuleFor(x => x.Password)
				 .NotEmpty()
				 .MinimumLength(3)
				 .MaximumLength(100)
				 .WithName("Parola");




			}

		}
	}

}

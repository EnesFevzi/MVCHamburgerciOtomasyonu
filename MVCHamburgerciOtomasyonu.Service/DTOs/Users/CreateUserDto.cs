using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Users
{
	public class CreateUserDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EMail { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}

using AutoMapper;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.AutoMapper.Users
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserAddDto>().ReverseMap();
            CreateMap<AppUser, UserUpdateDto>().ReverseMap();
            CreateMap<AppUser, UserProfileDto>().ReverseMap();
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
        }
	}
}

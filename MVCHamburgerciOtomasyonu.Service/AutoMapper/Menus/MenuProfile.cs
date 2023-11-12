using AutoMapper;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;

namespace MVCHamburgerciOtomasyonu.Service.AutoMapper.Menus
{
	public class MenuProfile : Profile
	{
		public MenuProfile()
		{
			CreateMap<MenuDto, Menu>().ReverseMap();
			CreateMap<MenuUpdateDto, Menu>().ReverseMap();
			CreateMap<MenuUpdateDto, MenuDto>().ReverseMap();
			CreateMap<MenuAddDto, Menu>().ReverseMap();
		}
	}
}

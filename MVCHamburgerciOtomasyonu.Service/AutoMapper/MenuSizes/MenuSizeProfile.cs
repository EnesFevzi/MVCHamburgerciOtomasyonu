using AutoMapper;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.MenuSizes;

namespace MVCHamburgerciOtomasyonu.Service.AutoMapper.MenuSizes
{
    public class MenuSizeProfile : Profile
	{
		public MenuSizeProfile()
		{
			CreateMap<MenuSizeDto, MenuSize>().ReverseMap();
			CreateMap<MenuSizeAddDto, MenuSize>().ReverseMap();
            CreateMap<MenuSizeUpdateDto, MenuSizeDto>().ReverseMap();
            CreateMap<MenuSizeAddDto, MenuSize>().ReverseMap();
        }
	}
}


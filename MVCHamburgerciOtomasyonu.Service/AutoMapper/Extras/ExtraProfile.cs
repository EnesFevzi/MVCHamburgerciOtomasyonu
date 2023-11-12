using AutoMapper;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Extras;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.AutoMapper.Extras
{
    public class ExtraProfile:Profile
    {
        public ExtraProfile()
        {
            CreateMap<Extra, ExtraDto>().ReverseMap();
            CreateMap<ExtraUpdateDto, Extra>().ReverseMap();
            CreateMap<ExtraUpdateDto, ExtraDto>().ReverseMap();
            CreateMap<ExtraAddDto, Extra>().ReverseMap();
        }
    }
}

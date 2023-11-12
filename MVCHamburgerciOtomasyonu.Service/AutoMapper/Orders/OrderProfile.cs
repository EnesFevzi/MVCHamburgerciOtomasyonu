using AutoMapper;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.Orders;

namespace MVCHamburgerciOtomasyonu.Service.AutoMapper.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>().ReverseMap();
            CreateMap<OrderUpdateDto, OrderDto>().ReverseMap();
            CreateMap<OrderAddDto, Order>().ReverseMap();
        }
    }
}

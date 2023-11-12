using AutoMapper;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.AutoMapper.Messages
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageDto,Message>().ReverseMap();
            CreateMap<SendMessageDto,Message>().ReverseMap();
            CreateMap<SendMessageDto,MessageDto>().ReverseMap();
        }
    }
}

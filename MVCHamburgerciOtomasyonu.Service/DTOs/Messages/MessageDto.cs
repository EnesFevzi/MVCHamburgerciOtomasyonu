using MVCHamburgerciOtomasyonu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.DTOs.Messages
{
    public class MessageDto
    {
        public Guid MessageID { get; set; }
        public string Subject { get; set; }
        public string MessageDetails { get; set; }
        public DateTime MessageDate { get; set; }
        public bool MessageStatus { get; set; }

        public AppUser SenderUser { get; set; }
        public AppUser ReceiverUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}

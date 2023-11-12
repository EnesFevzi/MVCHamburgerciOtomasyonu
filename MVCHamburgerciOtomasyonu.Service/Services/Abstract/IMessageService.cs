using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
    public interface IMessageService
    {
        Task SendMessageAsync(SendMessageDto sendMessageDto);
        Task<MessageDto> GetMessageDetails(Guid messageID);
        Task<List<MessageDto>> ReceiverMessageAsync();
        Task<List<MessageDto>> SenderMessageAsync();
        Task<string> SafeDeleteMenuAsync(Guid MenuID);
        Task<string> UndoDeleteMenuAsync(Guid MenuID);
    }
}

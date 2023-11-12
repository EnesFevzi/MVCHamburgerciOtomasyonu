using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MVCHamburgerciOtomasyonu.Core.Repositories.Abstract;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Messages;
using MVCHamburgerciOtomasyonu.Service.Extensions;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using System.Security.Claims;

namespace MVCHamburgerciOtomasyonu.Service.Services.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        private readonly ClaimsPrincipal _user;

        public MessageService(IRepository<Message> messageRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService, UserManager<AppUser> userManager)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<MessageDto> GetMessageDetails(Guid messageID)
        {
            var message = await _messageRepository.GetAsync(x => !x.IsDeleted && x.MessageID == messageID, x => x.ReceiverUser, x => x.SenderUser);
            var map = _mapper.Map<MessageDto>(message);
            return map;
        }

        public async Task<List<MessageDto>> ReceiverMessageAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var message = await _messageRepository.GetAllAsync(x => !x.IsDeleted && x.ReceiverUserId == userID, x => x.ReceiverUser, x => x.SenderUser);
            var map = _mapper.Map<List<MessageDto>>(message);
            return map;

        }

        public async Task<string> SafeDeleteMenuAsync(Guid MenuID)
        {
            var userEmail = _user.GetLoggedInEmail();
            var menu = await _messageRepository.GetByGuidAsync(MenuID);
            menu.IsDeleted = true;
            menu.DeletedDate = DateTime.Now;
            menu.DeletedBy = userEmail;
            await _messageRepository.UpdateAsync(menu);
            return menu.Subject;
        }

        public async Task<List<MessageDto>> SenderMessageAsync()
        {
            var userID = _user.GetLoggedInUserId();
            var message = await _messageRepository.GetAllAsync(x => !x.IsDeleted && x.SenderUserId == userID, x => x.ReceiverUser);
            var map = _mapper.Map<List<MessageDto>>(message);
            return map;
        }

        public async Task SendMessageAsync(SendMessageDto sendMessageDto)
        {
            var userID = _user.GetLoggedInUserId();
            var user = await _userService.GetAppUserByIdAsync(userID);
            var map = _mapper.Map<Message>(sendMessageDto);
            map.SenderUserId = userID;
            map.SenderUser = user;
            var receiverUser = await _userManager.FindByEmailAsync(sendMessageDto.ReceiverMail);
            map.ReceiverUserId = receiverUser.Id;
            map.ReceiverUser = receiverUser;
            await _messageRepository.AddAsync(map);

        }

        public async Task<string> UndoDeleteMenuAsync(Guid MenuID)
        {
            var menu = await _messageRepository.GetByGuidAsync(MenuID);
            menu.IsDeleted = false;
            menu.DeletedDate = null;
            menu.DeletedBy = null;
            await _messageRepository.UpdateAsync(menu);

            return menu.Subject;
        }
    }
}

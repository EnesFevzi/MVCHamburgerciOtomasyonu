using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCHamburgerciOtomasyonu.Entity.Entities;
using MVCHamburgerciOtomasyonu.Service.DTOs.Menus;
using MVCHamburgerciOtomasyonu.Service.DTOs.Messages;
using MVCHamburgerciOtomasyonu.Service.Services.Abstract;
using MVCHamburgerciOtomasyonu.Web.ResultMessages;
using NToastNotify;


namespace MVCHamburgerciOtomasyonu.Web.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IValidator<Message> _validator;
        private readonly IValidator<SendMessageDto> _validator2;

        public MessageController(IMessageService messageService, IMapper mapper, IToastNotification toastNotification, IValidator<Message> validator, IValidator<SendMessageDto> validator2)
        {
            _messageService = messageService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _validator = validator;
            _validator2 = validator2;
        }

        public async Task<IActionResult> InBox()
        {
            var message = await _messageService.ReceiverMessageAsync();
            return View(message);
        }
        public async Task<IActionResult> SendBox()
        {
            var message = await _messageService.SenderMessageAsync();
            return View(message);
        }
        [HttpGet]
        public IActionResult AdminSendMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminSendMessage(SendMessageDto sendMessageDto)
        {
            var map = _mapper.Map<Message>(sendMessageDto);
            var result = await _validator.ValidateAsync(map);
            var result2 = await _validator2.ValidateAsync(sendMessageDto);
            if (!result.IsValid || !result2.IsValid)
            {
                result.AddToModelState(this.ModelState);


            }
            else
            {
                await _messageService.SendMessageAsync(sendMessageDto);
                _toastNotification.AddSuccessToastMessage(Messages.Message.Send(sendMessageDto.Subject), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("SendBox", "Message");
            }

            return View();
        }
        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageDto sendMessageDto)
        {
            var map = _mapper.Map<Message>(sendMessageDto);
            var result = await _validator.ValidateAsync(map);
            var result2 = await _validator2.ValidateAsync(sendMessageDto);
            if (!result.IsValid || !result2.IsValid)
            {
                result.AddToModelState(this.ModelState);


            }
            else
            {
                await _messageService.SendMessageAsync(sendMessageDto);
                _toastNotification.AddSuccessToastMessage(Messages.Message.Send(sendMessageDto.Subject), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("SendBox", "Message");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AdminAnswerToMessage(Guid messageId)
        {
            var message = await _messageService.GetMessageDetails(messageId);
            var map = _mapper.Map<SendMessageDto>(message);
            map.ReceiverMail = message.SenderUser.Email;
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> AdminAnswerToMessage(SendMessageDto sendMessageDto)
        {
            var map = _mapper.Map<Message>(sendMessageDto);
            var result = await _validator.ValidateAsync(map);
            var result2 = await _validator2.ValidateAsync(sendMessageDto);
            if (!result.IsValid || !result2.IsValid)
            {
                result.AddToModelState(this.ModelState);


            }
            else
            {
                await _messageService.SendMessageAsync(sendMessageDto);
                _toastNotification.AddSuccessToastMessage(Messages.Message.Send(sendMessageDto.Subject), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("SendBox", "Message");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MessageDetails(Guid messageId)
        {
            var message = await _messageService.GetMessageDetails(messageId);
            var map = _mapper.Map<MessageDto>(message);
            return View(map);
        }
    }
}

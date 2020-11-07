using AutoMapper;
using Microsoft.AspNet.SignalR.Client;
using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Services;
using MobChat.Domain.Services;
using MobChat.Infra.DataAccess.Repositories.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Application.Services
{
    public class TextMessageAppService : ITextMessageAppService
    {
        public ObservableCollection<TextMessageViewModel> TextMessages { get; set; }
        private ITextMessageService textMessageService;
        private Mapper mapper;


        public TextMessageAppService(string devicePlatform)
        {
            textMessageService = new TextMessageService(new SQLiteMessagesRepository(devicePlatform));
            TextMessages = new ObservableCollection<TextMessageViewModel>();
            mapper = new Mapper(AutoMapper.AutoMapperConfig.RegisterMappings());
        }

        public Task<IEnumerable<TextMessageViewModel>> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TextMessageViewModel> GetAllMessagesByChatId(Guid chatId)
        {
            var messages = textMessageService.GetAllMessagesByChatId(chatId);
            IEnumerable<TextMessageViewModel> textMessagesViewModel =
                mapper.Map<IEnumerable<TextMessage>, IEnumerable<TextMessageViewModel>>(messages);

            foreach (TextMessageViewModel message in textMessagesViewModel)
            {
                TextMessages.Insert(0, message);
            }
            return textMessagesViewModel.OrderBy(item => item.MessageDate);
        }

        public IEnumerable<TextMessageViewModel> GetMessagesByContactId(Guid contactId)
        {
            var messages = textMessageService.GetMessagesByContactId(contactId);
            IEnumerable<TextMessageViewModel> textMessagesViewModel =
                mapper.Map<IEnumerable<TextMessage>, IEnumerable<TextMessageViewModel>>(messages);
            return textMessagesViewModel;
        }

        public IEnumerable<TextMessageViewModel> GetMessagesByUserId(Guid userId)
        {
            var messages = textMessageService.GetMessagesByUserId(userId);
            IEnumerable<TextMessageViewModel> textMessagesViewModel =
                mapper.Map<IEnumerable<TextMessage>, IEnumerable<TextMessageViewModel>>(messages);
            return textMessagesViewModel;
        }

        public async Task<bool> AddMessage(TextMessageViewModel message)
        {
            TextMessage textMessage = mapper.Map<TextMessage>(message);
            return await textMessageService.AddMessageAsync(textMessage);
        }

        public async Task<bool> AddOnlineMessage(TextMessageViewModel message)
        {
            TextMessages.Insert(0, message);
            return await AddMessage(message);
        }
    }
}

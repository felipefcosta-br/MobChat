using AutoMapper;
using Microsoft.Graph;
using MobChat.Application.EventHandlers;
using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Services;
using MobChat.Domain.Services;
using MobChat.Infra.DataAccess.Repositories.Chats;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat = MobChat.Domain.Entities.Chat;

namespace MobChat.Application.Services
{
    public class ChatAppService : IChatAppService
    {
        public ObservableCollection<ChatViewModel> Chats { get; set; }
        private IChatService chatService;
        private Mapper mapper;

        public event EventHandler<MessageEventArgs> OnReceivedMessageOnline;
        public event EventHandler<MessageEventArgs> OnReceivedMessageOffline;

        public ChatAppService()
        {
        }

        public ChatAppService(string devicePlatform)
        {
            SQLiteChatsRepository repository = new SQLiteChatsRepository(devicePlatform);
            chatService = new ChatService(repository);
            mapper = new Mapper(AutoMapper.AutoMapperConfig.RegisterMappings());
            Chats = new ObservableCollection<ChatViewModel>();

            chatService.OnReceivedChatMessageOnline += (sender, message) =>
            {
                TextMessageViewModel messageViewModel = mapper.Map<TextMessageViewModel>(message.Message);
                OnlineMessageReceived(messageViewModel);
            };
            chatService.OnReceivedChatMessageOffline += (sender, message) =>
            {
                TextMessageViewModel messageViewModel = mapper.Map<TextMessageViewModel>(message.Message);
                OfflineMessageReceived(messageViewModel);
            };
        }

        public Task<bool> CheckIfChatExists(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public async Task Connect(Guid userId, String token)
        {
            await chatService.ConnectAsync(userId, token);
        }

        public async Task<ChatViewModel> CreateChat(ChatViewModel chatViewModel)
        {
            Chat chat = mapper.Map<Chat>(chatViewModel);
            Chat newChat = await chatService.CreateChat(chat);
            ChatViewModel newChatViewModel = mapper.Map<ChatViewModel>(newChat);
            Chats.Insert(0, newChatViewModel);
            return newChatViewModel;
        }

        public async Task DisconnectUser(Guid userId)
        {
            await chatService.DisconnectUser(userId);
        }

        public async Task<IEnumerable<ChatViewModel>> GetAllChats()
        {
            var chats = await chatService.GetAllChats();
            IEnumerable<ChatViewModel> chatsViewModel = mapper.Map<IEnumerable<Chat>, IEnumerable<ChatViewModel>>(chats);
            //IEnumerable < ChatViewModel > OrderChats = chatsViewModel.OrderBy(chat => chat.)
            foreach (ChatViewModel chat in chatsViewModel)
            {
                Chats.Insert(0, chat);
                Console.WriteLine($"PhotoTeste - {chat.ContactPhoto}");
            }
            return chatsViewModel;
        }

        public async Task<ChatViewModel> GetChatAsync(Guid chatId)
        {
            var chat = await chatService.GetChatAsync(chatId);
            ChatViewModel chatViewModel = mapper.Map<ChatViewModel>(chat);
            return chatViewModel;
        }

        public async Task<ChatViewModel> GetChatByContactId(Guid contactId)
        {
            var chat = await chatService.GetChatByContactId(contactId);
            ChatViewModel chatViewModel = mapper.Map<ChatViewModel>(chat);
            return chatViewModel;
        }

        public async Task<ChatViewModel> GetChatByUserId(Guid userId)
        {
            var chat = await chatService.GetChatByUserId(userId);
            ChatViewModel chatViewModel = mapper.Map<ChatViewModel>(chat);
            return chatViewModel;
        }

        public async Task SendTextMessageAsync(TextMessageViewModel messageViewModel)
        {
            TextMessage message = mapper.Map<TextMessage>(messageViewModel);
            await chatService.SendTextMessageAsync(message);
        }

        public void OnlineMessageReceived(TextMessageViewModel messageViewModel)
        {
            OnReceivedMessageOnline?.Invoke(this, new MessageEventArgs(messageViewModel));
        } 
        
        public void OfflineMessageReceived(TextMessageViewModel messageViewModel)
        {
            OnReceivedMessageOffline?.Invoke(this, new MessageEventArgs(messageViewModel));
        }

        public void SetChatOnlineOffline(ChatViewModel chatViewModel)
        {
            Chat chat = mapper.Map<Chat>(chatViewModel);
            chatService.SetChatOnlineOffline(chat);
        }

        public void UpdateChat(ChatViewModel chatViewModel)
        {
            Chat chat = mapper.Map<Chat>(chatViewModel);
            chatService.UpdateChat(chat);
        }
    }
}

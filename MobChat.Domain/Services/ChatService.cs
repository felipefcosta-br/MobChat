using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Repositories;
using MobChat.Domain.Interfaces.Services;
using MobChat.Domain.Services.EventHandlers;
using MobChat.Infra.HttpService.Interfaces.Services;
using MobChat.Infra.HttpService.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository repository;
        private IChatHttpService chatHttpService;

        public event EventHandler<MessageEventArgs> OnReceivedChatMessageOnline;
        public event EventHandler<MessageEventArgs> OnReceivedChatMessageOffline;
        

        public ChatService(IChatRepository repository)
        {
            this.repository = repository;
            chatHttpService = new ChatHttpService();

            chatHttpService.OnReceivedMessage += (sender, message) =>
            {
                MessageReceived(message.User, message.Message);
            };
        }

        public async Task ConnectAsync(Guid userId, String token)
        {
            chatHttpService.InitConnection(token);
            await chatHttpService.ConnectAsync(userId);
        }

        public async Task<Chat> CreateChat(Chat chat)
        {
            chat.Id = Guid.NewGuid();
            await repository.CreateAsync(chat);
            bool result = await repository.SaveChangesAsync() > 0;
            repository.DetachEntity(chat);
            if (!result)
                return null;

            return chat;
        }

        public async Task DisconnectUser(Guid userId)
        {
            await chatHttpService.DisconnectAsync(userId);
        }

        public async Task<IEnumerable<Chat>> GetAllChats()
        {
            return await repository.ReadAllAsync();
        }

        public async Task<Chat> GetChatAsync(Guid chatId)
        {
            return await repository.ReadAsync(chatId);
        }

        public async Task<Chat> GetChatByContactId(Guid contactId)
        {
            return await repository.GetChatByContactId(contactId);
        }

        public async Task<Chat> GetChatByUserId(Guid userId)
        {
            return await repository.GetChatByUserId(userId);
        }

        private async void MessageReceived(string appContact, string message)
        {
            TextMessage textMessage = JsonConvert.DeserializeObject<TextMessage>(message);
            if (textMessage != null)
            {
                Chat chat = await repository.GetChatByContactId(textMessage.SenderId);
                if(chat != null)
                {
                    textMessage.ChatId = chat.Id;
                    if (chat.Online)
                    {
                        OnReceivedChatMessageOnline?.Invoke(this, new MessageEventArgs(textMessage));
                    }
                    else
                    {
                        OnReceivedChatMessageOffline?.Invoke(this, new MessageEventArgs(textMessage));

                    }

                }
                else
                {
                    Chat newChat = new Chat();
                    newChat.UserId = textMessage.ContactId;
                    newChat.ContactId = textMessage.UserId;                    
                    newChat.ContactName = textMessage.UserName;
                    newChat.ContactPhoto = textMessage.ContactPhoto;
                    newChat.Online = false;
                    Chat chatTemp = await CreateChat(newChat);

                    textMessage.ChatId = chatTemp.Id;
                    OnReceivedChatMessageOffline?.Invoke(this, new MessageEventArgs(textMessage));
                }
            }

        }

        public async Task SendTextMessageAsync(TextMessage message)
        {
            string serializedMessage = JsonConvert.SerializeObject(message);
            Guid userId = message.UserId;
            Guid contactId = message.ContactId;
            await chatHttpService.SendTextMessageAsync(userId, contactId, serializedMessage);
        }

        public void SetChatOnlineOffline(Chat chat)
        {
            if (!chat.Online)
            {
                chat.Online = true;
            }
            else
            {
                chat.Online = false;
            }
            UpdateChat(chat);
        }

        public void UpdateChat(Chat chat)
        {
            repository.UpdateNoTracked(chat);
            repository.SaveChangesAsync();
            repository.DetachEntity(chat);
            
        }
    }
}

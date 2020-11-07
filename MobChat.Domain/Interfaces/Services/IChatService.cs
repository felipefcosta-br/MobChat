using MobChat.Domain.Entities;
using MobChat.Domain.Services.EventHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Interfaces.Services
{
    public interface IChatService
    {
        Task ConnectAsync(Guid userId, String token);
        Task DisconnectUser(Guid userId);
        Task<Chat> CreateChat(Chat chat);
        Task<IEnumerable<Chat>> GetAllChats();
        Task<Chat> GetChatAsync(Guid chatId);
        Task<Chat> GetChatByUserId(Guid userId);
        Task<Chat> GetChatByContactId(Guid contactId);
        Task SendTextMessageAsync(TextMessage message);
        void SetChatOnlineOffline(Chat chat);
        void UpdateChat(Chat chat);
        event EventHandler<MessageEventArgs> OnReceivedChatMessageOnline;
        event EventHandler<MessageEventArgs> OnReceivedChatMessageOffline;
    }
}

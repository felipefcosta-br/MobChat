using MobChat.Application.EventHandlers;
using MobChat.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Application.Interfaces
{
    public interface IChatAppService
    {
        ObservableCollection<ChatViewModel> Chats { get; set; }
        Task Connect(Guid userId, String token);
        Task DisconnectUser(Guid userId);
        Task<ChatViewModel> CreateChat(ChatViewModel chatViewModel);
        Task<bool> CheckIfChatExists(Guid contactId);
        Task<IEnumerable<ChatViewModel>> GetAllChats();
        Task<ChatViewModel> GetChatAsync(Guid chatId);
        Task<ChatViewModel> GetChatByUserId(Guid userId);
        Task<ChatViewModel> GetChatByContactId(Guid contactId);
        Task SendTextMessageAsync(TextMessageViewModel messageViewModel);
        void SetChatOnlineOffline(ChatViewModel chatViewModel);
        void UpdateChat(ChatViewModel chatViewModel);
        event EventHandler<MessageEventArgs> OnReceivedMessageOnline;
        event EventHandler<MessageEventArgs> OnReceivedMessageOffline;
    }
}

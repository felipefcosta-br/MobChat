using MobChat.Application.EventHandlers;
using MobChat.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Application.Interfaces
{
    public interface ITextMessageAppService
    {
        ObservableCollection<TextMessageViewModel> TextMessages { get; set; }
        Task<bool> AddMessage(TextMessageViewModel message);
        Task<bool> AddOnlineMessage(TextMessageViewModel message);
        Task<IEnumerable<TextMessageViewModel>> GetAllMessages();
        IEnumerable<TextMessageViewModel> GetAllMessagesByChatId(Guid chatId);
        IEnumerable<TextMessageViewModel> GetMessagesByContactId(Guid contactId);
        IEnumerable<TextMessageViewModel> GetMessagesByUserId(Guid userId);
    }
}

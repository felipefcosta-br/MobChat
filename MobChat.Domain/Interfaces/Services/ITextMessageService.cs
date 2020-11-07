using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Interfaces.Services
{
    public interface ITextMessageService
    {
        Task<bool> AddMessageAsync(TextMessage message);
        Task<IEnumerable<TextMessage>> GetAllMessages();
        IEnumerable<TextMessage> GetAllMessagesByChatId(Guid chatId);
        IEnumerable<TextMessage> GetMessagesByContactId(Guid contactId);
        IEnumerable<TextMessage> GetMessagesByUserId(Guid userId);
        Task<TextMessage> GetMessageByUserIdAndContactId(Guid userId, Guid contactId);
    }
}

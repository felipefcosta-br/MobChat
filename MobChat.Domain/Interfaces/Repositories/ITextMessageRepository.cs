using MobChat.Common.Domain.Interface.Repositories;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Interfaces.Repositories
{
    public interface ITextMessageRepository : IRepository<Guid, TextMessage>
    {
        Task<TextMessage> GetMessageByUserIdAndContactId(Guid userId, Guid contactId);
        IEnumerable<TextMessage> GetMessagesByChatId(Guid chatId);
        IEnumerable<TextMessage> GetMessagesByContactId(Guid contactId);
        IEnumerable<TextMessage> GetMessagesByUserId(Guid userId);

    }
}

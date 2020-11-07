using MobChat.Common.Domain.Interface.Repositories;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Interfaces.Repositories
{
    public interface IChatRepository : IRepository<Guid, Chat>
    {
        Task<Chat> GetChatAsync(Guid chatId);
        Task<Chat> GetChatByContactId(Guid contactId);
        Task<Chat> GetChatByUserId(Guid userId);
    }
}

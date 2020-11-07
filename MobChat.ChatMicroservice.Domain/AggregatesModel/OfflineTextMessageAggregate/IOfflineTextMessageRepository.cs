using MobChat.Common.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate
{
    public interface IOfflineTextMessageRepository : IRepository<Guid, OfflineTextMessage>
    {
        Task DeleteUserOfflineTextMessages(Guid receiverId, Guid senderId);
        IEnumerable<OfflineTextMessage> GetOfflineTextMessages(Guid receiverId);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate
{
    public interface IOfflineTextMessageService
    {
        Task<bool> AddOfflineMessageAsync(Guid receiverId, Guid senderId, String message);
        IEnumerable<OfflineTextMessage> GetOfflineTextMessagesAsync(Guid receiverId);
        Task DeleteUserOfflineTextMessages(Guid receiverId, Guid senderId);
    }
}

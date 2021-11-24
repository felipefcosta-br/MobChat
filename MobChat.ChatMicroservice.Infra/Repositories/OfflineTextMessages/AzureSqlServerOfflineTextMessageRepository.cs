using MobChat.ChatMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using MobChat.ChatMicroservice.Infra.DataAccess;
using MobChat.Common.Infra.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Infra.Repositories.OfflineTextMessages
{
    public class AzureSqlServerOfflineTextMessageRepository :
        EntityFrameworkRepositoryBase<Guid, OfflineTextMessage>, IOfflineTextMessageRepository
    {
        public AzureSqlServerOfflineTextMessageRepository()
            : base(new ChatContext(""))
        {

        }
        public async Task DeleteUserOfflineTextMessages(Guid receiverId, Guid senderId)
        {
            IEnumerable<OfflineTextMessage> offTextMessages = dbContext.Set<OfflineTextMessage>().
                Where(message => message.ReceiverId == receiverId && message.SenderId == senderId).AsEnumerable();

            foreach (OfflineTextMessage message in offTextMessages)
            {
                dbContext.Set<OfflineTextMessage>().Remove(await ReadAsync(message.Id));
            }
        }

        public IEnumerable<OfflineTextMessage> GetOfflineTextMessages(Guid receiverId)
        {
            var result = dbContext.Set<OfflineTextMessage>().Where(message => message.ReceiverId == receiverId).AsEnumerable();
            return result;
        }
    }
}

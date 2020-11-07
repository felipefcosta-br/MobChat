using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatHubMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate
{
    public class OfflineTextMessageService : IOfflineTextMessageService
    {
        private readonly IOfflineTextMessageRepository repository;

        public OfflineTextMessageService(IOfflineTextMessageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddOfflineMessageAsync(Guid receiverId, Guid senderId, String message)
        {
            OfflineTextMessage offMessage = new OfflineTextMessage();
            offMessage.Id = Guid.NewGuid();
            offMessage.ReceiverId = receiverId;
            offMessage.SenderId = senderId;
            offMessage.Message = message;
            await repository.CreateAsync(offMessage);
            return await repository.SaveChangesAsync() > 0;
        }

        public async Task DeleteUserOfflineTextMessages(Guid receiverId, Guid senderId)
        {
            await repository.DeleteUserOfflineTextMessages(receiverId, senderId);
        }

        public IEnumerable<OfflineTextMessage> GetOfflineTextMessagesAsync(Guid receiverId)
        {
            return repository.GetOfflineTextMessages(receiverId);
        }
    }
}

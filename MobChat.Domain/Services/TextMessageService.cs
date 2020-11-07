using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Repositories;
using MobChat.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobChat.Domain.Services
{
    public class TextMessageService : ITextMessageService
    {
        private readonly ITextMessageRepository repository;

        public TextMessageService(ITextMessageRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> AddMessageAsync(TextMessage message)
        {
            message.Id = Guid.NewGuid();
            await repository.CreateAsync(message);
            return await repository.SaveChangesAsync() > 0;
        }

        public Task<IEnumerable<TextMessage>> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TextMessage> GetAllMessagesByChatId(Guid chatId)
        {
            return repository.GetMessagesByChatId(chatId);
        }

        public IEnumerable<TextMessage> GetMessagesByContactId(Guid contactId)
        {
            return repository.GetMessagesByContactId(contactId);
        }

        public IEnumerable<TextMessage> GetMessagesByUserId(Guid userId)
        {
            return repository.GetMessagesByUserId(userId);
        }

        public async Task<TextMessage> GetMessageByUserIdAndContactId(Guid userId, Guid contactId)
        {
            TextMessage textMessage = await repository.GetMessageByUserIdAndContactId(userId, contactId);
            return textMessage;
        }
    }
}

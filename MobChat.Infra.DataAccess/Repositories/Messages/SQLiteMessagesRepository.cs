using Microsoft.EntityFrameworkCore;
using MobChat.Common.Infra.DataAccess.Repositories;
using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Repositories;
using MobChat.Infra.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.DataAccess.Repositories.Messages
{
    public class SQLiteMessagesRepository : EntityFrameworkRepositoryBase<Guid, TextMessage>, ITextMessageRepository
    {
        public SQLiteMessagesRepository(string devicePlatform)
        {
            string dbPath = "Filename=";
            const string dbFileName = "mobchat.sqlite";

            switch (devicePlatform)
            {
                case "UWP":
                    dbPath += Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbFileName);
                    break;
                case "iOS":
                    dbPath += Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "data", dbFileName);
                    break;
                case "Android":
                    dbPath += Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), dbFileName);
                    break;
            }

            dbContext = new ChatContext(dbPath);
        }

        public IEnumerable<TextMessage> GetMessagesByChatId(Guid chatId)
        {
            return dbContext.Set<TextMessage>().Where(message => message.ChatId == chatId).AsEnumerable();
        }

        public async Task<TextMessage> GetMessageByUserIdAndContactId(Guid userId, Guid contactId)
        {
            TextMessage textMessage =
                await dbContext.Set<TextMessage>().FirstOrDefaultAsync(message => (message.UserId == userId && message.ContactId == contactId) ||
                                                                        (message.UserId == contactId && message.ContactId == userId));

            return textMessage;
        }

        public IEnumerable<TextMessage> GetMessagesByContactId(Guid contactId)
        {
            return dbContext.Set<TextMessage>().Where(message => message.ContactId == contactId).AsEnumerable();
        }

        public IEnumerable<TextMessage> GetMessagesByUserId(Guid userId)
        {
            return dbContext.Set<TextMessage>().Where(message => message.UserId == userId).AsEnumerable();
        }
    }
}

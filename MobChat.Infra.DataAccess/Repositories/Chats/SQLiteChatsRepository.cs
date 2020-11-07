using Microsoft.EntityFrameworkCore;
using MobChat.Common.Infra.DataAccess.Repositories;
using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Repositories;
using MobChat.Infra.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.DataAccess.Repositories.Chats
{
    public class SQLiteChatsRepository : EntityFrameworkRepositoryBase<Guid, Chat>, IChatRepository
    {
        public SQLiteChatsRepository(string devicePlatform)
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

        public async Task<Chat> GetChatAsync(Guid chatId)
        {
            return await dbContext.Set<Chat>().AsNoTracking().FirstOrDefaultAsync(chat => chat.Id == chatId);
        }

        public async Task<Chat> GetChatByContactId(Guid contactId)
        {
            return await dbContext.Set<Chat>().AsNoTracking().FirstOrDefaultAsync(chat => chat.ContactId == contactId);
        }

        
        public async Task<Chat> GetChatByUserId(Guid userId)
        {
            return await dbContext.Set<Chat>().AsNoTracking().FirstOrDefaultAsync(chat => chat.UserId == userId);
        }
    }
}

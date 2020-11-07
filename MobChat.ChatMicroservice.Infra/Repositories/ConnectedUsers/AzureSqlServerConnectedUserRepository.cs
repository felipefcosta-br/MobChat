using Microsoft.EntityFrameworkCore;
using MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatMicroservice.Infra.DataAccess;
using MobChat.Common.Infra.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Infra.Repositories.ConnectedUsers
{
    public class AzureSqlServerConnectedUserRepository : 
        EntityFrameworkRepositoryBase<Guid, ConnectedUser>, IConnectedUserRepository
    {
        public AzureSqlServerConnectedUserRepository()
            : base(new ChatContext("Server=tcp:mobchat-chatmicroservice-db-server.database.windows.net,1433;Initial Catalog=mobchat-chatmicroservice-db;Persist Security Info=False;User ID=felipefcrj;Password=01Xam-Project-BR-77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
        {

        }
        public async Task DeleteConnectedUserAsync(Guid userId)
        {
            ConnectedUser connectedUser = await dbContext.Set<ConnectedUser>().FirstOrDefaultAsync(user => user.UserId == userId);
            if (connectedUser != null)
                await DeleteAsync(connectedUser.Id);
        }

        public async Task<ConnectedUser> GetConnectedUserByUserIdAsync(Guid userId)
        {
            return await dbContext.Set<ConnectedUser>().AsNoTracking().FirstOrDefaultAsync(user => user.UserId == userId);
        }

        public async Task<bool> IsUserConnected(Guid userId)
        {
            ConnectedUser connectedUser = await dbContext.Set<ConnectedUser>().AsNoTracking().FirstOrDefaultAsync(user => user.UserId == userId);
            if (connectedUser == null)
                return false;

            return true;
        }
    }
}

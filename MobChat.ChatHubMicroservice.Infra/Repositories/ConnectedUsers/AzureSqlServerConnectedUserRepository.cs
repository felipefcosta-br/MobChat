using Microsoft.EntityFrameworkCore;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatHubMicroservice.Infra.DataAccess;
using MobChat.Common.Infra.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatHubMicroservice.Infra.Repositories.ConnectedUsers
{
    public class AzureSqlServerConnectedUserRepository: 
        EntityFrameworkRepositoryBase<Guid, ConnectedUser>, IConnectedUserRepository
    {
        public AzureSqlServerConnectedUserRepository()
            : base(new ChatHubContext("Server=tcp:mobchat-chathub-db-server.database.windows.net,1433;Initial Catalog=mobchat-chathubmicroservice-db;Persist Security Info=False;User ID=felipefcrj;Password=01Xam-Project-BR-77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
        {

        }
        public async Task DeleteConnectedUserAsync(Guid userId)
        {
            ConnectedUser connectedUser = await dbContext.Set<ConnectedUser>().FirstOrDefaultAsync(user => user.UserId == userId);
            if(connectedUser != null)
                await DeleteAsync(connectedUser.Id);
        }

        public async Task<ConnectedUser> GetConnectedUserByUserIdAsync(Guid userId)
        {
            return await dbContext.Set<ConnectedUser>().FirstOrDefaultAsync(user => user.UserId == userId);
        }

        public async Task<bool> IsUserConnected(Guid userId)
        {
            ConnectedUser connectedUser  = await dbContext.Set<ConnectedUser>().FirstOrDefaultAsync(user => user.UserId == userId);
            if (connectedUser == null)
                return false;
            
            return true;
        }
    }
}

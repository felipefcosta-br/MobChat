using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatHubMicroservice.Domain.AggregatesModel.ConnectedUserAggregate
{
    public class ConnectedUserService : IConnectedUserService
    {
        private readonly IConnectedUserRepository repository;

        
        public ConnectedUserService(IConnectedUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddConnectedUserAsync(Guid userId, string userContextName, string connectionId)
        {
            ConnectedUser user = new ConnectedUser();
            user.Id = Guid.NewGuid();
            user.UserId = userId;
            user.ContextUserId = userContextName;
            user.ConnectionId = connectionId;
            await repository.CreateAsync(user);
            return await repository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteConnectedUserAsync(Guid userId)
        {
            await repository.DeleteConnectedUserAsync(userId);
            return await repository.SaveChangesAsync() > 0;
        }

        public async Task<ConnectedUser> GetConnectedUserByUserIdAsync(Guid userId)
        {
            return await repository.GetConnectedUserByUserIdAsync(userId);
        }

        public async Task<bool> IsConnected(Guid userId)
        {
            return await repository.IsUserConnected(userId);
        }
    }
}

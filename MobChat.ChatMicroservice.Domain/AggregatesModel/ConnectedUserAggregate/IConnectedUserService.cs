using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate
{
    public interface IConnectedUserService
    {
        Task<bool> AddConnectedUserAsync(Guid userId, string userContextName, string connectionId);
        Task<bool> DeleteConnectedUserAsync(Guid userId);
        Task<IEnumerable<ConnectedUser>> GetAllConnectedUsersAsync();
        Task<ConnectedUser> GetConnectedUserByUserIdAsync(Guid userId);
        Task<bool> IsConnected(Guid userId);
        void UpdateConnectedUser(ConnectedUser connectedUser);
    }
}

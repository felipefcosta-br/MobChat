using MobChat.Common.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate
{
    public interface IConnectedUserRepository : IRepository<Guid, ConnectedUser>
    {
        Task DeleteConnectedUserAsync(Guid userId);
        Task<ConnectedUser> GetConnectedUserByUserIdAsync(Guid userId);
        Task<bool> IsUserConnected(Guid userId);
    }
}

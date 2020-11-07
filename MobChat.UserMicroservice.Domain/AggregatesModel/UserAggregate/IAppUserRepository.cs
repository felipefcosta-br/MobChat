using MobChat.Common.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate
{
    public interface IAppUserRepository : IRepository<Guid, AppUser>
    {
        Task<AppUser> FindByAccountIdAsync(Guid accountId);
        Task<AppUser> GetAppUserByUserName(String userName);
        IEnumerable<AppUser> SearchForUsers(string searchTxt);
    }
}

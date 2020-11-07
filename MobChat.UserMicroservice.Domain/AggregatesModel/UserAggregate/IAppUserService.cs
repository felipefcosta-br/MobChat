using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate
{
    public interface IAppUserService
    {
        Task<bool> AddUserAsync(AppUser user);
        Task<AppUser> GetUserAsync(Guid userId);
        Task<AppUser> GetUserByAccountIdAsync(Guid accountId);
        Task<AppUser> GetAppUserByUserNameAsync(String userName);
        IEnumerable<AppUser> GetAllUsers();
        IEnumerable<AppUser> SearchForUser(string searchTxt);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(AppUser user);
        Task<bool> DeleteUserAsync(Guid userId);
        
    }
}

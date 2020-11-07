using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository repository;
        
        public AppUserService(IAppUserRepository repository)
        {
            this.repository = repository;

        }
        public async Task<bool> AddUserAsync(AppUser user)
        {
            user.Id = Guid.NewGuid();
            await repository.CreateAsync(user);
            return await repository.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            await repository.DeleteAsync(userId);
            return await repository.SaveChangesAsync() > 0;
        }

        public IEnumerable<AppUser> GetAllUsers()
        {
            return repository.ReadAll();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await repository.ReadAllAsync();
        }

        public async Task<AppUser> GetAppUserByUserNameAsync(string userName)
        {
            return await repository.GetAppUserByUserName(userName);
        }

        public async Task<AppUser> GetUserAsync(Guid userId)
        {
            return await repository.ReadAsync(userId);
        }

        public async Task<AppUser> GetUserByAccountIdAsync(Guid accountId)
        {
            return await repository.FindByAccountIdAsync(accountId);
        }

        public IEnumerable<AppUser> SearchForUser(string searchTxt)
        {
            return repository.SearchForUsers(searchTxt);
        }

        public async Task<bool> UpdateUserAsync(AppUser user)
        {
            repository.Update(user);
            return await repository.SaveChangesAsync() > 0;

        }
    }
}

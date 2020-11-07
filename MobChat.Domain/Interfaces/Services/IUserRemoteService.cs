using MobChat.Domain.Entities;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Interfaces.Services
{
    public interface IUserRemoteService
    {
        Task<bool> AddUserAsync(AppUser appUser, Stream fileStream);
        Task DeleteUser(Guid id);
        Task<AppUser> GetUserAsync(Guid id);
        Task<AppUser> GetUserByAccountIdAsync(Guid accountId);
        Task UpdateUser();
        Task<string> UploadUserMedia(string container, string fileName, Stream fileStream, string contentType);
        Task<bool> IsUniqueUserName(string userName);
        Task<IEnumerable<AppUser>> SearchForUserAsync(string searchText);
    }
}

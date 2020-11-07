using MobChat.Application.Models.Dtos;
using MobChat.Application.Models.ViewModels;
using MobChat.Domain.Entities;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Application.Interfaces
{
    public interface IMobileUserAppService
    {
        Task<bool> AddUserAsync (AppUserViewModel userViewModel, Stream fileStream);
        Task UpdateUser ();
        Task<AppUserViewModel> GetUserAsync(Guid id);
        Task<AppUserViewModel> GetUserByAccountIdAsync(Guid accountId);
        Task<IEnumerable<AppUserViewModel>> GetUsersBySearchAsync(string searchText);
        Task DeleteUser(Guid id);
        Task<bool> IsUniqueUserName(string userName);
        
    }
}

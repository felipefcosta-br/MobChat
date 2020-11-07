using MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.HttpService.Interfaces.Services
{
    public interface IUserHttpService
    {
        Task<String> GetAppUserById(Guid userId);
        Task<String> GetAppUserByAccountId(Guid accountId);
        Task<String> GetUserByUserName(string userName);
        Task<bool> AddUserAsync(String serializedUser);
        Task<String> SearchForUser(String searchText);
    }
}

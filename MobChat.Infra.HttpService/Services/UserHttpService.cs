using MobChat.Infra.HttpService.Interfaces.Services;
using MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.HttpService.Services
{
    public class UserHttpService: IUserHttpService
    {
        public async Task<bool> AddUserAsync(string serializedUser)
        {
            var httpClient = new HttpClient();
            StringContent httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://mobchatusermicroserviceapi.azurewebsites.net/api/AppUsers", httpContent).Result;

            if (!result.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<String> GetAppUserById(Guid userId)
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync($"https://mobchatusermicroserviceapi.azurewebsites.net/api/AppUsers/{userId}").Result;

            String serializedResult = await result.Content.ReadAsStringAsync();
            return serializedResult;
        }

        public async Task<String> GetAppUserByAccountId(Guid accountId)
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync($"https://mobchatusermicroserviceapi.azurewebsites.net/api/AppUsers/account/{accountId}").Result;

            String serializedResult = await result.Content.ReadAsStringAsync();
            return serializedResult;
        }

        public async Task<String> GetUserByUserName(string userName)
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync($"https://mobchatusermicroserviceapi.azurewebsites.net/api/AppUsers/username/{userName}").Result;
            
            if (!result.IsSuccessStatusCode)
                return null;

            String serializedResult = await result.Content.ReadAsStringAsync();
            return serializedResult;
        }

        public async Task<string> SearchForUser(string searchText)
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync($"https://mobchatusermicroserviceapi.azurewebsites.net/api/AppUsers/search/user/{searchText}").Result;

            if (!result.IsSuccessStatusCode)
                return null;

            String serializedResult = await result.Content.ReadAsStringAsync();
            return serializedResult;
        }
    }
}

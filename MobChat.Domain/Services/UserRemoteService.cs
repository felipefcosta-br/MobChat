using Microsoft.Graph;
using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Services;
using MobChat.Infra.HttpService.Interfaces.Services;
using MobChat.Infra.HttpService.Services;
using MobChat.Infra.StorageService.Interfaces.Services;
using MobChat.Infra.StorageService.Services;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Image = System.Drawing.Image;

namespace MobChat.Domain.Services
{
    public class UserRemoteService: IUserRemoteService
    {
        private IUserHttpService httpService;
        private IBlobService blobService;

        public UserRemoteService()
        {
            httpService = new UserHttpService();
            blobService = new BlobService();
        }
        public async Task<bool> AddUserAsync(AppUser appUser, Stream fileStream)
        {
            string container = $"mobchatcontainer";
            string fileName = $"images{appUser.AccountId}.jpg";
            string photoUrl = UploadUserMedia(container, fileName, fileStream, "image/jpg").Result;


            //string thumbnailUrl = UploadUserMedia(container, thumbnailName, thumbnailStream, "image/jpg").Result;

            string userNameTemp = appUser.UserName;
            appUser.UserName = $"@{userNameTemp}";
            appUser.Photo = photoUrl;
            //appUser.Thumbnail = thumbnailUrl;
            appUser.Registration = DateTime.Now;

            string serializedUser = JsonConvert.SerializeObject(appUser);
            return await httpService.AddUserAsync(serializedUser);
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserAsync(Guid id)
        {
            String serializedAppUser = await httpService.GetAppUserById(id);
            var appUser = JsonConvert.DeserializeObject<AppUser>(serializedAppUser);
            return appUser;
        }

        public async Task<AppUser> GetUserByAccountIdAsync(Guid accountId)
        {
            String serializedAppUser = await httpService.GetAppUserByAccountId(accountId);
            var appUser = JsonConvert.DeserializeObject<AppUser>(serializedAppUser);
            return appUser;
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            String serializedAppUser = await httpService.GetUserByUserName(userName);           
            if (serializedAppUser == null)
                return true;

            return false;
        }

        public async Task<IEnumerable<AppUser>> SearchForUserAsync(string searchText)
        {
            String serializedAppUser = await httpService.SearchForUser(searchText);
            var result = JsonConvert.DeserializeObject<IEnumerable<AppUser>>(serializedAppUser);
            return result;
        }

        public Task UpdateUser()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadUserMedia(string container, string fileName, Stream fileStream, string contentType)
        {
            return blobService.UploadMediaFileAsync(container, fileName, fileStream, "image/jpg").Result;
        }
    }
}

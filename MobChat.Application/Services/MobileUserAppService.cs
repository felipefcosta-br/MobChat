using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Domain.Interfaces.Services;
using MobChat.Domain.Services;
using MobChat.Domain.Entities;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MobChat.Application.Models.Dtos;
using System.IO;

namespace MobChat.Application.Services
{
    public class MobileUserAppService : IMobileUserAppService
    {
        private IUserRemoteService userService;
        private Mapper mapper;

        public MobileUserAppService()
        {
            userService = new UserRemoteService();
            mapper = new Mapper(AutoMapper.AutoMapperConfig.RegisterMappings());
        }

        public async Task<bool> AddUserAsync(AppUserViewModel userViewModel, Stream fileStream)
        {
            AppUser appUser = mapper.Map<AppUser>(userViewModel);
            return await userService.AddUserAsync(appUser, fileStream);
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUserViewModel> GetUserAsync(Guid id)
        {
            var user = await userService.GetUserAsync(id);
            AppUserViewModel appUserViewModel = mapper.Map<AppUserViewModel>(user);
            return appUserViewModel;
        }

        public async Task<AppUserViewModel> GetUserByAccountIdAsync(Guid accountId)
        {
            var user = await userService.GetUserByAccountIdAsync(accountId);
            AppUserViewModel appUserViewModel = mapper.Map<AppUserViewModel>(user);
            return appUserViewModel;
        }

        public async Task<IEnumerable<AppUserViewModel>> GetUsersBySearchAsync(string searchText)
        {
            var IEunUsers = await userService.SearchForUserAsync(searchText);
            IEnumerable<AppUserViewModel> appUsersViewModel = mapper.Map< IEnumerable<AppUser> ,IEnumerable <AppUserViewModel>>(IEunUsers);
            return appUsersViewModel;
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await userService.IsUniqueUserName(userName);
        }

        public Task UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}

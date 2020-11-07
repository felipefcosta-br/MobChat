using AutoMapper;
using Microsoft.Identity.Client;
using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Domain.Interfaces.Services;
using MobChat.Domain.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MobChat.Application.Services
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        public ObservableCollection<AppUserViewModel> User { get; set; }
        private IAuthenticationService authService;
        private Mapper mapper;
        
        public AuthenticationAppService()
        {
            authService = new AuthenticationService();
            User = new ObservableCollection<AppUserViewModel>();
            mapper = new Mapper(AutoMapper.AutoMapperConfig.RegisterMappings());
        }
        public async Task<AppUserViewModel> IsUserAuthenticated()
        {
            var user = await authService.IsUserAuthenticated();
            AppUserViewModel userViewModel = mapper.Map<AppUserViewModel>(user);
            return userViewModel;
        }

        public async Task<AppUserViewModel> StartUserLoginService(Object UIParent)
        {
            var user = await authService.StartUserLoginService(UIParent);
            AppUserViewModel userViewModel = mapper.Map<AppUserViewModel>(user);
            return userViewModel;

        }

        public void StartUserLogoutService()
        {
            authService.StartUserLogoutService();
        }

        public async Task<Boolean> IsNewUser ()
        {
            return await authService.IsNewUser();
        }

        public Task<AuthenticationResult> GetUserAuthentication()
        {
            return authService.GetUserUserAuthentication();
        }
    }
}

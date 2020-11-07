using Microsoft.Identity.Client;
using MobChat.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Application.Interfaces
{
    public interface IAuthenticationAppService
    {
        Task<AuthenticationResult> GetUserAuthentication();
        Task<AppUserViewModel> IsUserAuthenticated();
        Task<AppUserViewModel> StartUserLoginService(Object UIParent);
        void StartUserLogoutService();
        Task<Boolean> IsNewUser();
    }
}

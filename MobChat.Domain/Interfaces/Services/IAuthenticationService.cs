using Microsoft.Identity.Client;
using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        object UIParent { get; set; }
        Task<AppUser> IsUserAuthenticated();
        Task<AuthenticationResult> GetUserUserAuthentication();
        Task<AppUser> StartUserLoginService(Object UIParent);
        void StartUserLogoutService();
        Task<Boolean> IsNewUser();
    }
}

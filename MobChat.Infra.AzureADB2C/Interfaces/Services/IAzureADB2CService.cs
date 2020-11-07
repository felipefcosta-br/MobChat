using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.AzureADB2C.Interfaces.Services
{
    public interface IAzureADB2CService
    {
        IPublicClientApplication AuthenticationClient { get; }
        object UIParent { get; set; }
        string ClientId { get; }
        string AuthoritySignin { get; }
        string AuthorityPasswordReset { get; }
        string AuthorityProfileEdition { get; }
        string IosKeychainSecurityGroup { get; }
        string[] Scopes { get; }
        Task<AuthenticationResult> CheckUserAuthentication();
        Task<AuthenticationResult> StartUserLoginService();
        Task<AuthenticationResult> OnForgotPassword();
        void StartUserLogoutService();
        //void GetUserAttributes();
    }
}

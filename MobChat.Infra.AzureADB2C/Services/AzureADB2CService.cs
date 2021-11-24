using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using MobChat.Infra.AzureADB2C.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.AzureADB2C.Services
{
    public class AzureADB2CService : IAzureADB2CService
    {
        static readonly string tenantName = "mobchat";
        static readonly string tenantId = "";
        static readonly string clientId = "";
        static readonly string policySignin = "";
        static readonly string policyPassword = "";
        static readonly string policyProfileEdition = "";
        static readonly string iosKeychainSecurityGroup = "";
        static readonly string[] scopes = { "" };
        static readonly string authorityBase = $"https://{tenantName}.b2clogin.com/tfp/{tenantId}/";
        static readonly string authoritySignInSignUp = $"{authorityBase}{policySignin}";
        private static IPublicClientApplication authenticationClient;
        private static InteractiveAuthenticationProvider authProvider;
        private static object uiParent = null;

        public IPublicClientApplication AuthenticationClient
        {
            get
            {
                return authenticationClient;
            }
        }
        public InteractiveAuthenticationProvider AuthProvider
        {
            get
            {
                return authProvider;
            }
        }
        public object UIParent
        {
            get
            {
                return uiParent;
            }
            set
            {
                uiParent = value;
            }
        }
        public string ClientId
        {
            get
            {
                return clientId;
            }
        }
        public string AuthoritySignin
        {
            get
            {
                return $"{authorityBase}{policySignin}";
            }
        }
        public string AuthorityPasswordReset
        {
            get
            {
                return $"{authorityBase}{policyPassword}";
            }
        }
        public string AuthorityProfileEdition
        {
            get
            {
                return $"{authorityBase}{policyProfileEdition}";
            }
        }
        public string IosKeychainSecurityGroup
        {
            get
            {
                return iosKeychainSecurityGroup;
            }
        }
        public string[] Scopes
        {
            get
            {
                return scopes;
            }
        }

        public AzureADB2CService()
        {
            authenticationClient = PublicClientApplicationBuilder.Create(ClientId)
                .WithIosKeychainSecurityGroup(IosKeychainSecurityGroup)
                .WithB2CAuthority(AuthoritySignin)
                .WithRedirectUri($"msal{ClientId}://auth")
                .Build();
        }
        public async Task<AuthenticationResult> CheckUserAuthentication()
        {
            try
            {
                IEnumerable<IAccount> accounts = await AuthenticationClient.GetAccountsAsync();
                AuthenticationResult result = AuthenticationClient
                     .AcquireTokenSilent(Scopes, accounts.FirstOrDefault())
                     .ExecuteAsync().Result;
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro - {e.Message}");
                return null;
            }

        }
        public async Task<AuthenticationResult> StartUserLoginService()
        {

            try
            {
                AuthenticationResult result = await AuthenticationClient
                                .AcquireTokenInteractive(Scopes)
                                .WithPrompt(Microsoft.Identity.Client.Prompt.SelectAccount)
                                .WithParentActivityOrWindow(UIParent)
                                .ExecuteAsync();
                return result;

            }
            catch (MsalClientException ex)
            {

                if (ex.Message != null && ex.Message.Contains("AADB2C90118"))
                {
                    return await OnForgotPassword();


                }
                else if (ex.ErrorCode != "authentication_canceled")
                {
                    return null;
                }
                else
                {
                    return null;
                }

            }
        }

        public async Task<AuthenticationResult> OnForgotPassword()
        {
            try
            {
                return await AuthenticationClient
                    .AcquireTokenInteractive(Scopes)
                    .WithPrompt(Microsoft.Identity.Client.Prompt.SelectAccount)
                    .WithParentActivityOrWindow(UIParent)
                    .WithB2CAuthority(AuthorityPasswordReset)
                    .ExecuteAsync();

            }
            catch (MsalException)
            {
                return null;
            }
        }

        public async void StartUserLogoutService()
        {
            IEnumerable<IAccount> accounts = await AuthenticationClient.GetAccountsAsync();
            while (accounts.Any())
            {
                await AuthenticationClient.RemoveAsync(accounts.First());
                accounts = await AuthenticationClient.GetAccountsAsync();
            }
        }
    }
}

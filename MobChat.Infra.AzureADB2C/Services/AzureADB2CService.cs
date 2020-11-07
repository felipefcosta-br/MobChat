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
        static readonly string tenantId = "mobchat.onmicrosoft.com";
        static readonly string clientId = "8d851549-9c94-4f28-9092-b076089985f9";
        static readonly string policySignin = "B2C_1_mobchat_signupandsignin";
        static readonly string policyPassword = "B2C_1_mobchat_passwordreset";
        static readonly string policyProfileEdition = "B2C_1_mobchat_profileediting";
        static readonly string iosKeychainSecurityGroup = "com.xamarin.mobchatmobile1915120669a.adb2cauthorization";
        static readonly string[] scopes = { "https://mobchat.onmicrosoft.com/8d851549-9c94-4f28-9092-b076089985f9/app.read" };
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

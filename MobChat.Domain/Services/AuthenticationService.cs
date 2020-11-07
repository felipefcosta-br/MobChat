using Microsoft.Identity.Client;
using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.Services;
using MobChat.Infra.AzureADB2C.Interfaces.Services;
using MobChat.Infra.AzureADB2C.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Domain.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private IAzureADB2CService authService;
        private static object uiParent = null;

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
        public AuthenticationService()
        {
            authService = new AzureADB2CService();
        }

        public async Task<AppUser> IsUserAuthenticated()
        {
            AuthenticationResult authResult = await authService.CheckUserAuthentication();

            if (authResult != null)
            {
                AppUser user = GetUserAttributesFromToken(authResult);
                return user;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<AppUser> StartUserLoginService(Object UIParent)
        {
            authService.UIParent = UIParent;
            AuthenticationResult authResult = await authService.StartUserLoginService();
            if (authResult != null)
            {
                AppUser user = GetUserAttributesFromToken(authResult);
                return user;
            }
            else
            {
                throw new Exception();
            }
        }

        public void StartUserLogoutService()
        {
            authService.StartUserLogoutService();
        }

        public async Task<Boolean> IsNewUser()
        {
            AuthenticationResult authResult = await authService.CheckUserAuthentication();

            if (authResult != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(authResult.IdToken);
                var json = token.Payload.SerializeToJson();
                var jsonObject = JObject.Parse(json.ToString());
                if (jsonObject["newUser"] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<AuthenticationResult> GetUserUserAuthentication()
        {
            return await authService.CheckUserAuthentication();
        }
        private AppUser GetUserAttributesFromToken(AuthenticationResult authResult)
        {

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authResult.IdToken);
            var json = token.Payload.SerializeToJson();
            var jsonObject = JObject.Parse(json.ToString());

            Console.WriteLine($"Teste - {jsonObject}");
            AppUser user = new AppUser();
            user.AccountId = Guid.Parse(jsonObject["oid"].ToString());
            user.Name = jsonObject["name"].ToString();
            user.LastName = jsonObject["family_name"].ToString();
            user.City = jsonObject["city"].ToString();
            user.State = jsonObject["state"].ToString();
            user.Country = jsonObject["country"].ToString();
            user.Email = jsonObject["emails"][0].ToString();

            return user;
        }

    }
}

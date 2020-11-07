using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
using MobChatApp.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobChatApp
{
    public partial class App : Application
    {
        public static IAuthenticationAppService AuthenticationService;
        public static IMobileUserAppService MobileUserService;
        public static IChatAppService ChatService;
        public static ITextMessageAppService textMessageAppService;
        public static object UIParent { get; set; } = null;
        public static AppUserViewModel AuthUser;
        public static bool IsConnected = false;
        public App()
        {                       
            AuthenticationService = new AuthenticationAppService();
            MobileUserService = new MobileUserAppService();
            ChatService = new ChatAppService(Device.RuntimePlatform);
            textMessageAppService = new TextMessageAppService(Device.RuntimePlatform);

            ChatService.OnReceivedMessageOffline += (sender, message) =>
            {
                AddOfflineTextMessage(message.TextMessage);
            };

            InitializeComponent();
            InitializeAppAuthentication();     
            
        }

        private async void InitializeAppAuthentication()
        {
            try
            {
                App.AuthUser = await App.AuthenticationService.IsUserAuthenticated();

                if (App.AuthUser.AccountId != null)
                {
                    AppUserViewModel appUser = await App.MobileUserService.GetUserByAccountIdAsync(App.AuthUser.AccountId);

                    if (App.AuthUser != null && appUser.UserName != null)
                    {
                        if(App.AuthUser.AccountId == appUser.AccountId)
                        {
                            App.AuthUser = appUser;
                            MainPage = new AppShell();
                            if(!IsConnected)
                                await ConnectToHub();
                            //await Shell.Current.GoToAsync("//privatechats");

                        }
                        else
                        {
                            MainPage = new LoginPage();
                        }                      

                    }
                    else
                    {
                        MainPage = new AddUserPage();
                        
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro de conexão{ex}");
                MainPage = new LoginPage();
            }

        }

        public static async Task ConnectToHub()
        {
            AuthenticationResult authResult = await App.AuthenticationService.GetUserAuthentication();
            try
            {
                await ChatService.Connect(App.AuthUser.Id, authResult.AccessToken);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }
            

        }

        private async void AddOfflineTextMessage(TextMessageViewModel textMessage)
        {
            AppUserViewModel contact = await MobileUserService.GetUserAsync(textMessage.UserId);
            //ChatViewModel chatViewModel = await ChatService.GetChatByContactId(contact.Id);

            TextMessageViewModel newTextMessage = new TextMessageViewModel();
            newTextMessage.ChatId = textMessage.ChatId;
            newTextMessage.SenderId = textMessage.SenderId;
            newTextMessage.UserId = App.AuthUser.Id;
            newTextMessage.UserName = $"{App.AuthUser.Name} {App.AuthUser.LastName}";
            newTextMessage.ContactId = contact.Id;
            newTextMessage.ContactName = $"{contact.Name} {contact.LastName}";
            newTextMessage.ContactPhoto = contact.Photo;
            newTextMessage.MessageDate = textMessage.MessageDate;
            newTextMessage.Message = textMessage.Message;
            newTextMessage.Status = "received";

            await textMessageAppService.AddMessage(newTextMessage);
            
        }

        protected override void OnStart()
        {
        }

        protected async override void OnSleep()
        {
            if (IsConnected)
                await ChatService.DisconnectUser(App.AuthUser.Id);
        }

        protected async override void OnResume()
        {
            if (!IsConnected && App.AuthUser != null)
            {
                await ConnectToHub();
            }
               

        }

    }
}

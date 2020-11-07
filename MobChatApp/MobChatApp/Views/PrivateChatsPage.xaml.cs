using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivateChatsPage : ContentPage
    {
        private IChatAppService chatService;
        public Boolean isShowingModal = false;
        public Boolean isInitChat = false;
        public ChatViewModel SelectedItem { get; set; }


        public PrivateChatsPage()
        {
            chatService = new ChatAppService(Device.RuntimePlatform);
            InitializeComponent();
            

        }

        private async void InitializeAppAuthentication()
        {
            try
            {
                if (App.AuthUser == null)
                    App.AuthUser = await App.AuthenticationService.IsUserAuthenticated();

                AppUserViewModel appUser = await App.MobileUserService.GetUserByAccountIdAsync(App.AuthUser.AccountId);
                if (App.AuthUser.AccountId == appUser.AccountId)
                    App.AuthUser = appUser;

                if (appUser != null && App.AuthUser.UserName != null)
                {
                    if (appUser.AccountId == App.AuthUser.AccountId)
                    {
                        App.AuthUser = appUser;
                        if (!isInitChat)
                        {
                            InitChats();
                            isInitChat = true;

                        }
                        if (!App.IsConnected)
                            await App.ConnectToHub();
                       
                        
                    }
                    else
                    {
                        App.AuthenticationService.StartUserLogoutService();
                        App.AuthUser = null;
                        await App.ChatService.DisconnectUser(App.AuthUser.Id);
                        await Shell.Current.GoToAsync("//login");
                    }

                }
                else
                {
                    if (!isShowingModal)
                    {
                        //await Shell.Current.GoToAsync("//addUserPage");
                        await Navigation.PushModalAsync(new AddUserPage());
                        isShowingModal = true;

                    }
                }
            }
            catch
            {



            }
        }

        private async void InitChats()
        {
            ChatsListView.ItemsSource = chatService.Chats;
            IEnumerable<ChatViewModel> chats = await chatService.GetAllChats();
        }

        private async void ChatsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ChatViewModel chatViewModel = e.Item as ChatViewModel;
            Console.WriteLine(chatViewModel.ContactName);

            AppUserViewModel appUser = await App.MobileUserService.GetUserAsync(chatViewModel.ContactId);
            var chatPage = new NavigationPage(new ChatPage());
            chatPage.BindingContext = appUser;
            await App.Current.MainPage.Navigation.PushModalAsync(chatPage);

        }

        protected override void OnAppearing()
        {
            isShowingModal = false;
            InitializeAppAuthentication();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
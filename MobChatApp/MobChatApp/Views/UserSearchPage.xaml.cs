using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSearchPage : ContentPage
    {
        private IMobileUserAppService userAppService;
        private IEnumerable<AppUserViewModel> appUsers;
        private AppUserViewModel userViewModel;
        public UserSearchPage()
        {
            InitializeComponent();
            userAppService = new MobileUserAppService();
            userViewModel = null;

        }

        private async void SearchUserBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && !string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                string searchText = e.NewTextValue.Trim();
                appUsers = await userAppService.GetUsersBySearchAsync(searchText);
                UserSearchResultsList.ItemsSource = appUsers.ToList();
            }
            else
            {
                UserSearchResultsList.ItemsSource = null;

            }

        }

        private void UserSearchResultsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            userViewModel = e.Item as AppUserViewModel;
            var searchedUserPage = new SearchedUserPage();
            searchedUserPage.BindingContext = userViewModel;
            PopupNavigation.Instance.PushAsync(searchedUserPage);
        }

        public async static Task GoToChatPage(AppUserViewModel appUser)
        {           
            var chatPage = new NavigationPage (new ChatPage());
            chatPage.BindingContext = appUser;
            await App.Current.MainPage.Navigation.PushModalAsync(chatPage);
        }
    }
}
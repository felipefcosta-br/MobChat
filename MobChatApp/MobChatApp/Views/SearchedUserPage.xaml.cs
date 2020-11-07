using MobChat.Application.Models.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedUserPage : PopupPage
    {
        private AppUserViewModel appUser;
        public SearchedUserPage()
        {
            InitializeComponent();
        }

        private async void GoToChatBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            var chatPage = new NavigationPage(new ChatPage());
            chatPage.BindingContext = appUser;
            await App.Current.MainPage.Navigation.PushModalAsync(chatPage);
        }

        private void AddContactBtn_Clicked(object sender, EventArgs e)
        {

        }

        protected override void OnBindingContextChanged()
        {
            appUser = ((AppUserViewModel)this.BindingContext);
            base.OnBindingContextChanged();
        }


    }
}
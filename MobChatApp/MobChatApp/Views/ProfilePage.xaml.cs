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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            
        }

        private async void BtnLogout_Clicked(object sender, EventArgs e)
        {
            App.AuthenticationService.StartUserLogoutService();
            App.AuthUser = null;
            await App.ChatService.DisconnectUser(App.AuthUser.Id);
            await Shell.Current.GoToAsync("//login");

        }
        protected override void OnAppearing()
        {
            ImgUserPhoto.Source = (App.AuthUser.Photo != null) ? App.AuthUser.Photo :" ";
            LblName.Text = $"{App.AuthUser.Name} {App.AuthUser.LastName}";
            LblUserName.Text = App.AuthUser.UserName;
            LblUserCity.Text = $"{App.AuthUser.City}";
            base.OnAppearing();
        }
    }
}
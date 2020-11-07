using MobChat.Application.Models.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        Button btnNewUser;
        public LoginPage()
        {
            InitializeComponent();
            btnNewUser = this.FindByName<Button>("BtnNewUser");
           
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.AuthUser =  await App.AuthenticationService.StartUserLoginService(App.UIParent);
                App.Current.MainPage = new AppShell();

            }
            catch
            {
        
            }
            
        }

    }
}
using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
using MobChatApp.Helpers.Validators;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddUserPage : ContentPage
    {
        private static MediaFile MediaFile;
        //private static MediaFile Thumbnail;


        public AddUserPage()
        {
            InitializeComponent();
            MediaFile = null;
            BindingContext = new AddUserPageValidation();
            BtnAddUser.CommandParameter = null;
        }

        private async void AddUserPhoto_Tapped(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet(null, "Cancelar", null, "Tirar foto", "Escolher uma imagem da galeria");
            switch (action)
            {
                case "Tirar foto":
                    TakeUserPhoto();
                    break;
                case "Escolher uma imagem da galeria":
                    ChooseUserPhoto();
                    break;
            }
        }
        private async void TakeUserPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sem câmera", "Nenhuma câmera encontrada.", "Ok");
                return;
            }
            MediaFile = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    AllowCropping = true,
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 60,
                    SaveToAlbum = true,
                    Directory = "MobChatAlbum",
                }
            );

            if (MediaFile == null)
                return;       
            
            UserImagePreview.Source = ImageSource.FromStream(() => MediaFile.GetStream());
            BtnAddUser.CommandParameter = MediaFile;
        }

        private async void ChooseUserPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Galeria não encontrada", "Nenhuma galeria foi encontrada.", "Ok");
                return;
            }
            MediaFile = await CrossMedia.Current.PickPhotoAsync(
                new PickMediaOptions 
                {
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 60
                }
            );

            if (MediaFile == null)
                return;

            UserImagePreview.Source = ImageSource.FromStream(() => MediaFile.GetStream());
            BtnAddUser.CommandParameter = MediaFile;

        }

        private void BtnAddUser_Clicked(object sender, EventArgs e)
        {
            AddUserPageValidation AddUserPageValidator;
            AddUserPageValidator = new AddUserPageValidation();
            AddUserPageValidator.AddUserCommand.Execute(null);
            
        }
    }

    
}
using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
using MobChatApp.Helpers.Validators.Rules;
using MobChatApp.Views;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobChatApp.Helpers.Validators
{
    public class AddUserPageValidation : INotifyPropertyChanged
    {
        private static IMobileUserAppService UserAppService;
        public ValidatableObject<string> UserName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> MobileNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> ImgUser { get; set; } = new ValidatableObject<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        public AddUserPageValidation()
        {
            AddValidationRules();
        }
        public void AddValidationRules()
        {
            UserAppService = new MobileUserAppService();

            UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Digite um nome de usuário" });
            UserName.Validations.Add(new IsOnlyLetterAndNumberRule<string> { ValidationMessage = "O Nome de usuário deve conter apenas letras e números"});
            UserName.Validations.Add(new IsLenghtValidRule<string> { ValidationMessage = "O nome de usuário deve ter entre 3 e 12 caracteres", MinimunLenght = 2, MaximunLenght = 12 });
            UserName.Validations.Add(new IsUniqueUser<string> { ValidationMessage = "Esse nome de usuário não está disponível" });

            MobileNumber.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Digite o número do seu telefone celular" });
            MobileNumber.Validations.Add(new IsOnlyNumberRule<string> { ValidationMessage = "Digite apenas números" });
            MobileNumber.Validations.Add(new IsLenghtValidRule<string> { ValidationMessage = "O seu celular deve ter entre 7 e 12 números", MinimunLenght = 6, MaximunLenght = 12 });
        }

        public bool AreFieldsValid()
        {
            bool isUserNameValid = UserName.Validate();
            bool isMobileNumberValid = MobileNumber.Validate();

            return isUserNameValid && isMobileNumberValid;
        }

        public ICommand AddUserCommand => new Command(async (parameter) =>
        {
            MediaFile media = null;
            if (parameter != null) 
            {
                media = parameter as MediaFile;
                
            }
            if (AreFieldsValid())
            {
                AppUserViewModel userViewModel = App.AuthUser;
                userViewModel.UserName = UserName.Value;
                userViewModel.MobileNumber = MobileNumber.Value;
                bool result = UserAppService.AddUserAsync(userViewModel, media.GetStream()).Result;

                if (result)
                {
                    Console.WriteLine("Ok");
                    App.Current.MainPage = new AppShell();
                }
                else
                {
                    Console.WriteLine("Erro");
                }
            }
        });

        
    }
}

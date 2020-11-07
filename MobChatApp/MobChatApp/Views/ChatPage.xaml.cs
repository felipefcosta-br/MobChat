using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using MobChat.Application.Services;
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
    public partial class ChatPage : ContentPage
    {
        private IChatAppService chatService;
        private ITextMessageAppService textMessageService;
        private AppUserViewModel chatContact;
        private ChatViewModel chat;
        private bool isNewChat = false;

        public ChatPage()
        {
            InitializeComponent();
            SendMessageBtn.IsEnabled = false;
            chatService = new ChatAppService(Device.RuntimePlatform);
            textMessageService = new TextMessageAppService(Device.RuntimePlatform);

            App.ChatService.OnReceivedMessageOnline += (sender, message) =>
            {
                AddReceivedTextMessage(message.TextMessage);
                MessagesCollectionView.ScrollTo(0);
            };
        }

        private async void BackButtonNavBar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SendMessageBtn_Clicked(object sender, EventArgs e)
        {
            if (!isNewChat)
            {
                await SendMessage(chat.Id);

            }
            else
            {
                AddChatAndSendMessage();
            }


        }
        private async void InitChatPage(Guid receiverId)
        {
            chat = await chatService.GetChatByContactId(receiverId);
            
            MessagesCollectionView.ItemsSource = textMessageService.TextMessages;
            

            if (chat != null)
            {
                if (!chat.Online)
                {
                    chatService.SetChatOnlineOffline(chat);
                    chat.Online = true;
                }
                    

                IEnumerable<TextMessageViewModel> messages = textMessageService.GetAllMessagesByChatId(chat.Id);
                isNewChat = false;

            }
            else
            {
                chat = new ChatViewModel();
                chat.UserId = App.AuthUser.Id;
                chat.ContactName = $"{chatContact.Name} {chatContact.LastName}";
                chat.ContactId = chatContact.Id;
                chat.ContactPhoto = chatContact.Photo;
                chat.Online = true;
                isNewChat = true;
            }

        }

        private async Task SendMessage(Guid chatId)
        {
            TextMessageViewModel message = new TextMessageViewModel();
            message.ChatId = chatId;
            message.SenderId = App.AuthUser.Id;
            message.UserId = App.AuthUser.Id;
            message.UserName = $"{App.AuthUser.Name} {App.AuthUser.LastName}";
            message.ContactId = chatContact.Id;
            message.ContactName = $"{chatContact.Name} {chatContact.LastName}";
            message.ContactPhoto = chatContact.Photo;
            message.MessageDate = DateTime.Now;
            message.Message = UserMessageEntry.Text;
            message.Status = "sent";

            await textMessageService.AddOnlineMessage(message);
            await App.ChatService.SendTextMessageAsync(message);
            MessagesCollectionView.ScrollTo(0);
            UserMessageEntry.Text = "";

        }

        private async void AddChatAndSendMessage()
        {
            ChatViewModel appChat = await chatService.GetChatByContactId(chatContact.Id);
            if (appChat == null)
            {
                chat = new ChatViewModel();
                chat.UserId = App.AuthUser.Id;
                chat.ContactName = chatContact.Name;
                chat.ContactId = chatContact.Id;
                chat.ContactPhoto = chatContact.Photo;
                chat.Online = true;
                isNewChat = true;
                appChat = await chatService.CreateChat(chat);
                if (appChat == null)
                    return;
            }
            else
            {
                IEnumerable<TextMessageViewModel> messages = textMessageService.GetAllMessagesByChatId(appChat.Id);
            }

            chat = appChat;
            isNewChat = false;
            await SendMessage(appChat.Id);
        }

        private void AddReceivedTextMessage(TextMessageViewModel textMessage)
        {
            TextMessageViewModel newTextMessage = new TextMessageViewModel();
            newTextMessage.ChatId = textMessage.ChatId;
            newTextMessage.SenderId = textMessage.SenderId;
            newTextMessage.UserId = textMessage.ContactId;
            newTextMessage.UserName = textMessage.ContactName;
            newTextMessage.ContactId = textMessage.UserId;
            newTextMessage.ContactName = textMessage.UserName;
            newTextMessage.ContactPhoto = chatContact.Photo;
            newTextMessage.MessageDate = textMessage.MessageDate;
            newTextMessage.Message = textMessage.Message;
            newTextMessage.Status = "received";

            textMessageService.AddOnlineMessage(newTextMessage);

        }

        protected override void OnBindingContextChanged()
        {
            chatContact = ((AppUserViewModel)this.BindingContext);
            InitChatPage(chatContact.Id);
            base.OnBindingContextChanged();
        }

        protected override void OnDisappearing()
        {            
            if(chat != null && chat.Online)
            {
                Console.WriteLine($"Chatteste - {chat}");
                chatService.SetChatOnlineOffline(chat);
            }
               

            base.OnDisappearing();
        }

        private void UserMessageEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && !string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                SendMessageBtn.IsEnabled = true;
            }
            else
            {
                SendMessageBtn.IsEnabled = false;

            }
        }
    }
}
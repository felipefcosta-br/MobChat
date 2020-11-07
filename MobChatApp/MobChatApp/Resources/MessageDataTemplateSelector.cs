using MobChat.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobChatApp.Resources
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserMessageTemplate { get; set; }
        public DataTemplate ContactMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((TextMessageViewModel)item).SenderId == App.AuthUser.Id ? UserMessageTemplate : ContactMessageTemplate;
        }
    }
}

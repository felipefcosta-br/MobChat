using MobChat.Application.Interfaces;
using MobChat.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Application.EventHandlers
{
    public class MessageEventArgs : IMessageEventArgs
    {
        public TextMessageViewModel TextMessage { get; }

        public MessageEventArgs(TextMessageViewModel textMessage)
        {
            TextMessage = textMessage;
        }
    }
}

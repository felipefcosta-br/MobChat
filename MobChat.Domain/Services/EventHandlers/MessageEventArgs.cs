using MobChat.Domain.Entities;
using MobChat.Domain.Interfaces.EventHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Domain.Services.EventHandlers
{
    public class MessageEventArgs : IMessageEventArgs
    {
        public TextMessage Message { get; }

        public MessageEventArgs(TextMessage message)
        {
            Message = message;
        }
    }
}

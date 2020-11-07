using MobChat.Infra.HttpService.Interfaces.EventHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Infra.HttpService.EventHandlers
{
    public class MessageEventArgs : IMessageEventArgs
    {
        public string Message { get; }
        public string User { get; }

        public MessageEventArgs(string message, string user)
        {
            Message = message;
            User = user;
        }
    }
}

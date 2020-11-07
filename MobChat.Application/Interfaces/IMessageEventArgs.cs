using MobChat.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Application.Interfaces
{
    public interface IMessageEventArgs
    {
        TextMessageViewModel TextMessage { get; }
    }
}

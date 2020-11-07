using MobChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Domain.Interfaces.EventHandlers
{
    public interface IMessageEventArgs
    {
        TextMessage Message { get; }
    }
}

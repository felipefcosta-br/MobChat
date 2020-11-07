using System;
using System.Collections.Generic;
using System.Text;

namespace MobChat.Infra.HttpService.Interfaces.EventHandlers
{
    public interface IMessageEventArgs
    {
        string Message { get; }
        string User { get; }
    }
}

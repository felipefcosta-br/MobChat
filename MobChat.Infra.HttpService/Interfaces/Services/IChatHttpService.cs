using MobChat.Infra.HttpService.EventHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.HttpService.Interfaces.Services
{
    public interface IChatHttpService
    {
        void InitConnection(String AccessToken);
        Task ConnectAsync(Guid userId);
        Task DisconnectAsync(Guid userId);
        Task RegisterUser(Guid userId);
        Task RemoveUser(Guid userId);
        Task SendTextMessageAsync(Guid userId, Guid contactId, string serializedMessage);
        event EventHandler<MessageEventArgs> OnReceivedMessage;
    }
}

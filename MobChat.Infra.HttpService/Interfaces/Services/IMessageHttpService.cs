using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobChat.Infra.HttpService.Interfaces.Services
{
    public interface IMessageHttpService
    {
        Task ConnectToHub();
        Task ConnectUser(Guid userId);
        Task DisconnectUser(Guid userId);
        Task<bool> SendTextMessage(Guid userId, Guid contactId, String serializedMessage);
    }
}
